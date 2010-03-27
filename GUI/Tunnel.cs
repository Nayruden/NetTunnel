using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace NetTunnel
{
    /// <summary>
    /// Stores everything we need to know about a tunnel as well as handles tunnel logic.
    /// </summary>
    public class Tunnel
    {
        private readonly ulong _remote_userid;
        private readonly bool _is_server;

        public enum TunnelState
        {
            /// <summary>
            /// We're attempting to connect to the remote endpoint and haven't heard anything from the remote.
            /// </summary>
            Connecting,
            /// <summary>
            /// We got a message from the remote endpoint, but we're not sure if our messages are reaching the remote endpoint yet.
            /// </summary>
            Halfway,
            /// <summary>
            /// Fully connected.
            /// </summary>
            Connected
        }

        public Tunnel(bool is_server, ulong remote_userid)
        {
            _remote_userid = remote_userid;
            _is_server = is_server;
        }

        /// <summary>
        /// The userid the other end of this tunnel is connected to.
        /// </summary>
        public ulong remote_userid { get { return _remote_userid; } }

        public TunnelState state { get; private set; }

        /// <summary>
        /// Whether or not the local client is the one serving data on this tunnel.
        /// </summary>
        public bool is_server { get { return _is_server; } }

        /// <summary>
        /// The local UDP clients for the tunnel.
        /// </summary>
        public IList<UdpClient> udp_connections { get; private set; }

        /// <summary>
        /// The local TCP clients for the tunnel.
        /// </summary>
        public IList<TcpClient> tcp_connections { get; private set; }

        /// <summary>
        /// The service associated with this tunnel
        /// </summary>
        public Service service
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// The port for the server-side tunnel to connect to on the local host. This is only used on the server.
        /// </summary>
        public ushort server_port
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// The protocols this tunnel is allowed to use.
        /// </summary>
        public Protocols protocols
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        

        /// <summary>
        /// The tunnel connection (to the remote host).
        /// </summary>
        public UdpClient tunnel_client
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
