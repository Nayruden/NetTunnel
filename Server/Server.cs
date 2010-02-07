using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

namespace NetTunnel
{
    class Server
    {
        private static ArrayList sockets = new ArrayList();
        private static ArrayList clients = new ArrayList();

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
            // Create thread to read from input
            var input_thread = new Thread(readInput);
            input_thread.Start();

            Console.WriteLine("Listening on port {0}...", Protocol.PORT);

            TcpListener listener = new TcpListener(IPAddress.Any, Protocol.PORT);
            listener.Start();

            var socketToClient = new Dictionary<Socket, TcpClient>();

            byte[] buffer = new byte[64];

            while (!exit)
            {
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
                        case "test":
                            var t = new TestMessage();
                            t.str = "Zoot-a-nation";
                            var ds = new DataContractSerializer(typeof(Message));
                            var memory_stream = new MemoryStream();
                            ds.WriteObject(memory_stream, t);
                            broadcast(memory_stream.GetBuffer(), (int)memory_stream.Length);
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

                ArrayList listenList = new ArrayList();
                listenList.Add(listener.Server);
                listenList.AddRange(sockets);
                Socket.Select(listenList, null, null, 100000); // Wait 100 ms

                foreach (Socket socket in listenList)
                {
                    if (socket == listener.Server) // It's our listener
                    {
                        var client = listener.AcceptTcpClient();
                        Console.WriteLine("Client connected");
                        sockets.Add(client.Client);
                        clients.Add(client);
                        socketToClient.Add(client.Client, client);
                    }
                    else
                    {
                        var client = socketToClient[socket];
                        // Do we have to use using?
                        var stream = client.GetStream();
                        int size = stream.Read(buffer, 0, buffer.Length);
                        if (size > 0) // Data
                        {
                            broadcast(buffer, size);
                        }
                        else // They disconnected
                        {
                            Console.WriteLine("Client disconnected");
                            clients.Remove(client);
                            sockets.Remove(socket);
                            socketToClient.Remove(socket);
                        }
                    }
                }
            }
        }

        static void broadcast(byte[] msg, int length)
        {
            foreach (TcpClient client in clients)
            {
                client.GetStream().Write(msg, 0, length);
            }
        }
    }
}
