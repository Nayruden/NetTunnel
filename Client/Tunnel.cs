using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using SharpPcap;
using PacketDotNet;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.IO;

namespace NetTunnel
{
    class Tunnel
    {
        public static List<Tunnel> tunnels = new List<Tunnel>();
        private static List<Tunnel> add_tunnels = new List<Tunnel>();
        public static Dictionary<ushort, UdpClient> port_to_client = new Dictionary<ushort, UdpClient>();

        private UdpClient client;
        private TcpListener listener;
        private TcpClient tcp_client;
        private volatile bool tcp_connected = false;
        private bool apache_connected = false;
        private bool established = false;
        private IPEndPoint endpoint;

        public Tunnel(IPEndPoint origin, IPEndPoint endpoint)
        {
            client = port_to_client[(ushort)origin.Port];
            this.endpoint = endpoint;
            listener = new TcpListener(IPAddress.Any, origin.Port);
            listener.Start();
            listener.BeginAcceptSocket(new AsyncCallback(AcceptTCPClient), this);
            //client.Connect(endpoint);
        }

        public static void AcceptTCPClient(IAsyncResult ar)
        {
            Tunnel t = (Tunnel)ar.AsyncState;
            t.tcp_client = t.listener.EndAcceptTcpClient(ar);
            Console.WriteLine("TCP connection");
            t.tcp_connected = true;

        }

        public static void addTunnel( Tunnel tunnel )
        {
            lock (add_tunnels)
                add_tunnels.Add(tunnel);
        }

        public static void run()
        {
            while (!Client.exit)
            {
                lock (add_tunnels)
                {
                    if (add_tunnels.Count > 0)
                    {
                        tunnels.AddRange(add_tunnels);
                        add_tunnels.Clear();
                    }
                }               

                foreach (var tunnel in tunnels)
                {
                    if (!tunnel.established)
                        tunnel.client.Send(Protocol.MAGIC_NUM, Protocol.MAGIC_NUM.Length, tunnel.endpoint);

                    if (tunnel.client.Available > 0)
                    {                        
                        IPEndPoint from = new IPEndPoint(IPAddress.Any, 0);
                        var b = tunnel.client.Receive(ref from);                       
                        if (from.Address.ToString() == tunnel.endpoint.Address.ToString())
                        {
                            if (Utilities.ArraysEqual(b, Protocol.MAGIC_NUM))
                            {
                                Console.WriteLine("Data! From {0}. Marking tunnel as established.", from);
                                tunnel.established = true;
                            }
                            else if (tunnel.tcp_connected)
                            {
                                tunnel.tcp_client.GetStream().Write(b, 0, b.Length);
                            }
                            else
                            {
                                if (true)
                                {
                                    Console.WriteLine("Ignored data");
                                }
                                else
                                {
                                    if (!tunnel.apache_connected)
                                    {
                                        tunnel.apache_connected = true;
                                        tunnel.tcp_client = new TcpClient();
                                        tunnel.tcp_client.Connect("localhost", 80);
                                        Console.WriteLine("Connected to Apache");
                                    }
                                    tunnel.tcp_client.GetStream().Write(b, 0, b.Length);
                                }
                            }
                        }
                        else
                        {
                            tunnel.client.Send(b, b.Length, tunnel.endpoint);
                        }
                    }

                    if (tunnel.tcp_client != null && tunnel.tcp_client.Available > 0)
                    {
                        var buffer = new byte[10240];
                        var b = tunnel.tcp_client.GetStream().Read(buffer, 0, buffer.Length);
                        tunnel.client.Send(buffer, b, tunnel.endpoint);
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
