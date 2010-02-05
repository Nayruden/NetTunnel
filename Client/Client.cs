using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace NetTunnel
{
    class Client
    {
        private static ASCIIEncoding encoding = new ASCIIEncoding();

        private static AutoResetEvent inputRead = new AutoResetEvent(false);
        private static AutoResetEvent readyForInput = new AutoResetEvent(false);
        private static volatile string inputLine;
        private static volatile bool exit = false;

        static void readInput()
        {
            while (!exit)
            {                
                inputLine = Console.ReadLine().Trim();
                inputRead.Set();
                readyForInput.WaitOne();
            }
        }

        static void Main(string[] args)
        {
            // Server setup (remove later on)
            var server_thread = new Thread(Server.NotMain);
            server_thread.Start();
            Thread.Sleep(500);
            // End server setup

            // Create thread to read from input
            var input_thread = new Thread(readInput);
            input_thread.Start();

            using (var client = new TcpClient( "localhost", Protocol.PORT ) )
            using (var n = client.GetStream())
            {                
                var buffer = new byte[64];

                while (!exit)
                {
                    if (n.DataAvailable)
                    {
                        n.Read(buffer, 0, buffer.Length);
                        Console.WriteLine(encoding.GetString(buffer));
                    }

                    if (inputRead.WaitOne(100)) // Wait 100 ms for input to be read
                    {
                        // There's something waiting for us, process it...
                        string op;
                        int space_index = inputLine.IndexOf(' ');
                        if (space_index >= 0) // Was found
                        {
                            op = inputLine.Substring(0, space_index);
                            inputLine = inputLine.Substring(space_index + 1);
                        }
                        else
                        {
                            op = inputLine; // Just a straight copy
                            inputLine = "";
                        }

                        switch (op)
                        {
                            case "c":
                                n.Write(encoding.GetBytes(inputLine), 0, inputLine.Length);
                                n.Flush();
                                break;

                            case "exit":
                            case "quit":
                                exit = true;                                
                                break;

                            default:
                                Console.WriteLine("Unrecognized input");
                                break;
                        }

                        readyForInput.Set();
                    }
                }

                server_thread.Abort();
            }
        }

    }

    class Server
    {
        public static void NotMain()
        {
            Console.WriteLine("Listening on port {0}...", Protocol.PORT);

            TcpListener listener = new TcpListener(IPAddress.Any, Protocol.PORT);
            listener.Start();

            using (TcpClient c = listener.AcceptTcpClient())
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
