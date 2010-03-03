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

namespace NetTunnel
{
    class Tunnel
    {
        public static List<Tunnel> tunnels = new List<Tunnel>();
        private static List<Tunnel> add_tunnels = new List<Tunnel>();
        private static List<LivePcapDevice> devices;
        private static Dictionary<LivePcapDevice, string> deviceMACs = new Dictionary<LivePcapDevice,string>();
        private static Dictionary<LivePcapDevice, PhysicalAddress> destMACs = new Dictionary<LivePcapDevice, PhysicalAddress>();

        static Tunnel()
        {
            // Retrieve the device list
            devices = LivePcapDeviceList.Instance.ToList();

            // If no devices were found print an error
            if (devices.Count < 1)
            {
                Console.WriteLine("No devices were found on this machine");
                return;
            }

            // Print out the available network devices 
            List<LivePcapDevice> devices_copy = new List<LivePcapDevice>();
            devices_copy.AddRange(devices);
            foreach (var dev in devices_copy)
            {
                if (dev.Interface.Name.Contains("usb") || dev.Interface.Name.Contains("any") || dev.Interface.Name.Contains("lo"))
                {
                    devices.Remove(dev);
                    continue; // HACK: Linux bug workaround
                }
                dev.Open();
                dev.OnPacketArrival += new PacketArrivalEventHandler(receive);              

                var bytes = dev.Interface.MacAddress.GetAddressBytes();
                StringBuilder sb = new StringBuilder();
                foreach (var b in bytes)
                {
                    sb.Append(b.ToString("X2")).Append(":");
                }
                sb.Remove(sb.Length - 1, 1);
                deviceMACs[ dev ] = sb.ToString();

                // Get dest MAC
                var p = new Process();
                // Redir output
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "arp";
                p.StartInfo.Arguments = "-a";
                p.Start();
                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                var lines = output.Split('\n');
                foreach (var line in lines)
                {
                    if (Regex.IsMatch(line, @"\D" + Regex.Escape(dev.Interface.GatewayAddress.ToString()) + @"\D"))
                    {
                        var m = Regex.Match(line, @"[\da-f]{1,2}[:-][\da-f]{1,2}[:-][\da-f]{1,2}[:-][\da-f]{1,2}[:-][\da-f]{1,2}[:-][\da-f]{1,2}", RegexOptions.IgnoreCase).Value;
                        var seperated = m.Split(':', '-');
                        var mac_bytes = seperated.Select(s => byte.Parse(s,System.Globalization.NumberStyles.HexNumber));
                        destMACs[dev] = new PhysicalAddress(mac_bytes.ToArray());
                        break;
                    }
                }
            }
        }

        public static void buildAndApplyFilter()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var tunnel in tunnels)
            {
                sb.AppendFormat("dst port {0} or ", tunnel.local_port);
            }

            sb.Remove(sb.Length - 4, 4); // remove last or

            foreach (var dev in devices)
            {
                dev.SetFilter("ether dst {0} and ({1})".F(deviceMACs[dev], sb));
                if (!dev.Started)
                    dev.StartCapture();
            }
        }

        private ushort local_port;
        private ushort remote_port;
        private IPAddress ip;

        public Tunnel(ushort local_port, IPAddress ip, ushort remote_port)
        {
            this.local_port = local_port;
            this.remote_port = remote_port;
            this.ip = ip;
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
                        buildAndApplyFilter();
                    }
                }

                foreach (var tunnel in tunnels)
                {
                    var ping = new PingMessage();
                    //int length;
                    //var serialized = Utilities.writeMessage(ping, out length);

                    var tcp_packet = new TcpPacket(tunnel.local_port, tunnel.remote_port);
                    tcp_packet.PayloadData = Protocol.MAGIC_NUM;

                    var udp_packet = new UdpPacket(tunnel.local_port, tunnel.remote_port);
                    udp_packet.PayloadData = Protocol.MAGIC_NUM;

                    foreach (var dev in devices)
                    {
                        if (!destMACs.ContainsKey(dev)) continue;
                        IPAddress ip_source = null;
                        foreach (var addy in dev.Addresses)
                        {
                            if (addy.Addr.ipAddress == null) continue;
                            if (addy.Addr.ipAddress.AddressFamily == AddressFamily.InterNetwork)
                            {
                                ip_source = addy.Addr.ipAddress;
                                break;
                            }
                        }
                        var ip_dest = tunnel.ip;
                        var ip_packet = new IPv4Packet(ip_source, ip_dest);
                        ip_packet.TimeToLive = 15;

                        var ethernetSourceHwAddress = System.Net.NetworkInformation.PhysicalAddress.Parse( deviceMACs[ dev ].Replace( ":", "-" ) );
                        var ethernetDestinationHwAddress = System.Net.NetworkInformation.PhysicalAddress.Parse("FF-FF-FF-FF-FF-FF");
                        // NOTE: using EthernetPacketType.None to illustrate that the ethernet
                        //       protocol type is updated based on the packet payload that is
                        //       assigned to that particular ethernet packet
                        var ethernetPacket = new EthernetPacket(ethernetSourceHwAddress,
                                                                ethernetDestinationHwAddress,
                                                                EthernetPacketType.None);

                        // Now stitch all of the packets together
                        //ip_packet.PayloadPacket = tcp_packet;
                        //ethernetPacket.PayloadPacket = ip_packet;

                        //dev.SendPacket(ethernetPacket.Bytes);

                        var packet = new List<byte>();
                        var ethernet = new List<byte>();
                        var ip = new List<byte>();
                        var udp = new List<byte>();

                        // Ethernet
                        ethernet.AddRange(destMACs[ dev ].GetAddressBytes()); // Destination MAC (TODO!)
                        ethernet.AddRange(dev.Interface.MacAddress.GetAddressBytes()); // Source MAC
                        ethernet.AddRange(new byte[] { 0x08, 0x00 }); // Ether Type (08 = IP)

                        // IP
                        ip.Add(0x45); // IPv4 in first nibble and standard size header (5 words * 4 bytes in a word == 20 bytes in header) in second nibble
                        ip.Add(0x00); // No DS
                        ip.AddRange(new byte[] { 0x00, 0x24 }); // 36 bytes in length (20 byte header + 16 byte payload)
                        ip.AddRange(new byte[] { 0x00, 0x00 }); // No ID
                        ip.AddRange(new byte[] { 0x00, 0x00 }); // No flags and no offset
                        ip.Add(0x1F); // 31 hops TTL
                        ip.Add(0x11); // UDP protocol
                        //ip.AddRange(new byte[] { 0x00, 0x00 }); // 0 for now
                        ip.AddRange(ip_source.GetAddressBytes()); // Source address
                        ip.AddRange(ip_dest.GetAddressBytes()); // Destination address

                        // Calc IP checksum
                        ip.InsertRange(10, BitConverter.GetBytes(Utilities.reverseEndian(checksum_ip(ip))));                      

                        // UDP
                        udp.AddRange(BitConverter.GetBytes(Utilities.reverseEndian(tunnel.local_port))); // Source port
                        udp.AddRange(BitConverter.GetBytes(Utilities.reverseEndian(tunnel.remote_port))); // Destination port
                        udp.AddRange(new byte[] { 0x00, 0x10 }); // Length (8 for header, 8 for data)
                        udp.AddRange(BitConverter.GetBytes((ushort)0)); // Checksum
                        udp.AddRange(Protocol.MAGIC_NUM);

                        packet.AddRange(ethernet);
                        packet.AddRange(ip);
                        packet.AddRange(udp);
                        dev.SendPacket(packet.ToArray());

                        //Console.WriteLine( ethernetPacket.ToString() );
                        //[EthernetPacket: 00235422B1B9 -> FFFFFFFFFFFF proto=IpV4 (0x800) l=14][IPv4Packe
                        //t: 192.168.0.145 -> 71.219.103.119 HeaderLength=5 Protocol=UDP TimeToLive=15][UD
                        //PPacket: 4149 -> 4149 l=2,-2]
                    }                    
                }

                Thread.Sleep(1000);
            }
        }

        public static ushort checksum_ip(List<byte> bytes)
        {
            Int32 sum = 0;

            for (int i = 0; i < bytes.Count; i += 2)
            {
                sum += (bytes[i] << 8) | bytes[i + 1];
                if ((sum & 0x80000000) != 0)
                    sum = (sum & 0xFFFF) + (sum >> 8);
            }

            while (sum >> 16 != 0)
                sum = (sum & 0xFFFF) + (sum >> 16);

            return (ushort)~sum;
        }

        public static void receive(object sender, CaptureEventArgs e)
        {
            if (e.Packet is SharpPcap.Packets.TCPPacket)
            {
                var packet = (SharpPcap.Packets.TCPPacket)e.Packet;
                Console.WriteLine("Recieved TCP packet from {0}:{1}", packet.SourceAddress, packet.SourcePort);
            }
            else
            {
                var packet = (SharpPcap.Packets.UDPPacket)e.Packet;
                Console.WriteLine("Recieved UDP packet from {0}:{1}", packet.SourceAddress, packet.SourcePort);
            }
        }
    }
}
