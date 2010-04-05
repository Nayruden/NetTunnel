using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace NetTunnel
{
    /// <summary>
    /// Stores everything we need to know about a tunnel as well as handles tunnel logic.
    /// </summary>
    public class Tunnel // TODO: Add events on connected and failed.
    {
        private readonly ulong _remote_userid;
        private readonly bool _is_server;
        private readonly Service _service;
        private readonly ushort _server_port;
        private readonly Protocols _protocols;

        public enum TunnelState
        {
            /// <summary>
            /// We just created this tunnel and we haven't allocated a socket yet.
            /// </summary>
            Initial,
            /// <summary>
            /// We've created the socket and told the server about our port, but we haven't heard back from the server about the remote port to connect to.
            /// </summary>
            InformedServer,
            /// <summary>
            /// We're attempting to connect to the remote endpoint and haven't heard anything from the remote.
            /// </summary>
            Connecting,
            /// <summary>
            /// We got a ping message from the remote endpoint, but we're not sure if our messages are reaching the remote endpoint yet. (haven't heard a pong)
            /// </summary>
            Halfway,
            /// <summary>
            /// Fully connected.
            /// </summary>
            Connected
        }

        public Tunnel(bool is_server, ulong remote_userid, Service service, Protocols protocols, ushort server_port)
        {
            _remote_userid = remote_userid;
            _is_server = is_server;
            _service = service;
            _server_port = server_port;
            _protocols = protocols;
            state = TunnelState.Initial;
        }

        /// <summary>
        /// The userid the other end of this tunnel is connected to.
        /// </summary>
        public ulong remote_userid { get { return _remote_userid; } }

        /// <summary>
        /// The state of the tunnel.
        /// </summary>
        public TunnelState state { get; private set; }

        /// <summary>
        /// Whether or not the local client is the one serving data on this tunnel.
        /// </summary>
        public bool is_server { get { return _is_server; } }

        /// <summary>
        /// The port for the server-side tunnel to connect to on the local host. This is only used on the server.
        /// </summary>
        public ushort server_port
        {
            get
            {
                if (_is_server)
                    return _server_port;
                throw new NotSupportedException("server_port is invalid on a client tunnel");
            }
        }

        /// <summary>
        /// The local UDP client for the tunnel (connects between two local endpoints). Can be null if not used.
        /// </summary>
        public UdpClient udp_connection { get; private set; }

        /// <summary>
        /// The local TCP client for the tunnel (connects between two local endpoints). Can be null if not used.
        /// </summary>
        public TcpClient tcp_connection { get; private set; }

        /// <summary>
        /// The service associated with this tunnel.
        /// </summary>
        public Service service { get { return _service; } }

        /// <summary>
        /// The protocols this tunnel is allowed to use.
        /// </summary>
        public Protocols protocols { get { return _protocols; } }

        /// <summary>
        /// The tunnel connection (to the remote host).
        /// </summary>
        public UdpClient tunnel_connection { get; private set; }

        public class TunnelManager
        {
            private readonly List<Tunnel> _tunnels_to_add; // Must be accessed by lock
            private readonly List<ulong> _disconnected_userids; // Must be accessed by lock
            private readonly List<Tunnel> _tunnels;
            private readonly CancellationToken _cancel_token;
            private readonly IPEndPoint _server_endpoint;

            public TunnelManager(CancellationToken cancel_token, IPEndPoint server_endpoint)
            {
                _tunnels = new List<Tunnel>();
                _disconnected_userids = new List<ulong>();
                _tunnels_to_add = new List<Tunnel>();
                _cancel_token = cancel_token;
                _server_endpoint = server_endpoint;
            }

            /// <summary>
            /// Add a new tunnel to the manager for the manager to take care of.
            /// </summary>
            public void AddTunnel(Tunnel tunnel)
            {
                lock (_tunnels_to_add)
                    _tunnels_to_add.Add(tunnel);
            }

            /// <summary>
            /// Call with the userid of a disconnected user when they disconnect.
            /// </summary>
            public void InformUseridDisconnected(ulong userid)
            {
                lock (_disconnected_userids)
                    _disconnected_userids.Add(userid);
            }

            /// <summary>
            /// Where the tunnel manager logic runs, only returns shortly after a cancel is requested.
            /// </summary>
            public void Run()
            {
                while (!_cancel_token.IsCancellationRequested)
                {
                    RemoveTunnelsOfDisconnectedUsers();
                    AddWaitingTunnels();

                    if (_tunnels.Count == 0)
                    {
                        Thread.Sleep(100); // Sleep 0.1 seconds and hope there's something waiting for us next time
                        continue;
                    }

                    // Future: Maybe make it so this only happens once a minute or something...
                    // Remove anything with a disabled service
                    _tunnels.RemoveAll(t => !t.service.enabled); // TODO: Volatile read or something of service.enabled?

                    SetupTunnels();

                    // Future: This could be cached
                    var socket_to_tunnel = new Dictionary<Socket, Tunnel>();
                    var sockets_check_read = _tunnels.SelectMany(t => // Note that there are no tunnels in initial state at this point
                    {
                        var sockets_to_add = new List<Socket>(){ 
                            t.tunnel_connection.Client, // This can't be null
                            t.udp_connection != null ? t.udp_connection.Client : null, 
                            t.tcp_connection != null ? t.tcp_connection.Client : null };
                        sockets_to_add.RemoveAll(s => s == null); // Remove nulls

                        // Insert what's left into our dictionary
                        sockets_to_add.ForEach(s => socket_to_tunnel[s] = t);

                        return sockets_to_add;
                    }).ToList();

                    var sockets_check_error = new List<Socket>(sockets_check_read);

                    Socket.Select(sockets_check_read, null, sockets_check_error, 100000); // Wait a max of 0.1 seconds

                    sockets_check_read.ForEach(s =>
                    {
                        var tunnel = socket_to_tunnel[s];

                        var data = new byte[s.Available];
                        var ip_endpoint = new IPEndPoint(IPAddress.Any, 0);
                        var endpoint = (EndPoint)ip_endpoint;
                        s.ReceiveFrom(data, ref endpoint);

                        HandleData(data, tunnel, ip_endpoint);
                        return;
                    });
                }
            }

            private void RemoveTunnelsOfDisconnectedUsers()
            {
                lock (_disconnected_userids)
                {
                    if (_disconnected_userids.Count > 0)
                    {
                        _tunnels.RemoveAll(t => _disconnected_userids.Contains(t.remote_userid));
                        _disconnected_userids.Clear();
                    }
                }
            }

            private void SetupTunnels()
            {
                var to_setup = _tunnels.Where(t => t.state == TunnelState.Initial);
                to_setup.ToList().ForEach(t =>
                {
                    t.tunnel_connection = new UdpClient();
                    throw new NotImplementedException("Need to inform server here");
                    t.state = TunnelState.InformedServer;
                });
            }

            private void HandleData(byte[] data, Tunnel tunnel, IPEndPoint ip_endpoint)
            {
                if (ip_endpoint.Address.Equals(_server_endpoint.Address))
                {
                    if (tunnel.state != TunnelState.InformedServer)
                    {
                        Trace.TraceError("Received unexpected message from server on a tunnel socket");
                        return; // Continue
                    }

                    throw new NotImplementedException("Read data from server, open connection to remote");
                }
                else if (tunnel.tunnel_connection != null && ip_endpoint.Address.Equals(tunnel.tunnel_connection.Client.RemoteEndPoint))
                {
                    if (tunnel.state < TunnelState.Connecting)
                    {
                        Trace.TraceError("Received message from remote endpoint before expected");
                        return; // Continue
                    }
                    else if (tunnel.state == TunnelState.Connecting || tunnel.state == TunnelState.Halfway)
                    {
                        throw new NotImplementedException("See if it's a ping or a pong and act accordingly");
                        // State to change to when we receive ping/pong and we're connecting/halfway:
                        //          Connecting      Halfway
                        // Ping     Halfway         Halfway
                        // Pong     Connected       Connected
                    }
                }
                else
                {
                    // Assume that it's coming from the local host
                    // TODO: Figure out how to verify it's from the local host
                    throw new NotImplementedException("Forward data");
                }
            }

            private void AddWaitingTunnels()
            {
                lock (_tunnels_to_add)
                {
                    if (_tunnels_to_add.Count > 0)
                    {
                        _tunnels.AddRange(_tunnels_to_add);
                        _tunnels_to_add.Clear();
                    }
                }
            }
        }
    }
}
