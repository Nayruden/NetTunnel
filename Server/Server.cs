using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;

namespace NetTunnel
{
    class Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Listening on port {0}...", Protocol.PORT);

            TcpListener listener = new TcpListener(IPAddress.Any, Protocol.PORT);
            listener.Start();

            ArrayList listenList = new ArrayList();
            listenList.Add(listener.Server);

            while (true)
            {
                ArrayList selectedListenList = new ArrayList();
                selectedListenList.AddRange(listenList);
                Socket.Select(selectedListenList, null, null, 100000);

                foreach ( Socket socket in selectedListenList )
                {
                    if (socket == listener.Server) // It's our listener
                    {
                        TcpClient c = listener.AcceptTcpClient();
                        listenList.Add(c.Client);
                    }
                }
            }
        }

        static void accept(object clientObject)
        {
            using (TcpClient c = (TcpClient)clientObject)
            using (NetworkStream n = c.GetStream())
            {
                while (true)
                {
                    byte[] buffer = new byte[64];
                    int size = n.Read(buffer, 0, buffer.Length);
                    n.Write(buffer, 0, size);
                }
            }
        }
    }
}
