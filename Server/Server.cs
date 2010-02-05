using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace NetTunnel
{
    class Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Listening on port {0}...", Protocol.PORT);

            TcpListener listener = new TcpListener(IPAddress.Any, Protocol.PORT);
            listener.Start();

            using (TcpClient c = listener.AcceptTcpClient())
            using (NetworkStream n = c.GetStream())
            {
                byte[] buffer = new byte[1000];
                int size = n.Read(buffer, 0, 1000);
                n.Write(buffer, 0, size);
            }
        }
    }
}
