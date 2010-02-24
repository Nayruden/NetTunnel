using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Linq;
using ProtoBuf;

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
                inputLine = Console.ReadLine();
                if (inputLine != null) // Mono doesn't appear to block on readline and can return null
                {
                    inputLine.Trim();
                    inputRead.Set();
                    readyForInput.WaitOne();
                }
                else
                {
                    Thread.Sleep(100);
                }
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

            var socketToClient = new Dictionary<Socket, ClientDetails>();

            byte[] buffer = new byte[Protocol.MAX_MESSAGE_SIZE];

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
                          //var t = new TestMessage();
                          //t.str = "Zoot-a-nation";
                          //int length;
                          //var serialized = Utilities.writeMessage(t, out length);
                          //broadcast(serialized, length);
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
                        var tcp_client = listener.AcceptTcpClient();

                        DebugMessage.msg("Client with IP {0} connected".F(tcp_client.Client.RemoteEndPoint.ToString()));

                        var client_details = new ClientDetails();
                        client_details.nick = "Unnamed";
                        client_details.tcp_client = tcp_client;                        

                        sockets.Add(tcp_client.Client);                        
                        socketToClient.Add(tcp_client.Client, client_details);

                        Message m = new RegistrationMessage( client_details.userid );
                        int length;
                        Serializer.SerializeWithLengthPrefix(tcp_client.GetStream(), m, PrefixStyle.Base128);
                        
                        // Tell them about other connected clients
                        foreach (ClientDetails client in clients)
                        {
                            m = new UserMessage( client.state.nick, client.state.userid );
                            ((UserMessage)m).state = MessageState.Created;
                            ((UserMessage)m).services = client.state.services;
                            serialized = Utilities.writeMessage(m, out length);
                            stream.Write(BitConverter.GetBytes(length), 0, sizeof(int));
                            stream.Write(serialized, 0, length);
                        }

                        stream.Flush();
                        clients.Add(client_details);
                    }
                    else
                    {
                        var client_details = socketToClient[socket];

                        if (socket.Available == 0)
                        {
                            DebugMessage.msg("Client with IP {0} disconnected".F(client_details.tcp_client.Client.RemoteEndPoint.ToString()));
                            clients.Remove(client_details);
                            sockets.Remove(socket);
                            socketToClient.Remove(socket);

                            var user_message = new UserMessage(client_details.nick, client_details.userid);
                            user_message.state = MessageState.Deleted;
                            int length;
                            var msg = Utilities.writeMessage(user_message, out length);
                            broadcast(msg, length);
                            continue;
                        }

                        var stream = client_details.tcp_client.GetStream();
                        byte[] len = new byte[sizeof(int)];
                        stream.Read(len, 0, len.Length);
                        int size = stream.Read(buffer, 0, buffer.Length);
                        Debug.Assert(size > 0, "Sanity error!");
                        var m = Utilities.readMessage(buffer, size);
                        // TODO, add validation they are who they say they are

                        switch (m.type)
                        {
                            case MessageType.UserMessage:
                                var user_message = (UserMessage)m;
                                Console.WriteLine("Got usermessage from {0}, nick of {1}", user_message.userid, user_message.nick);
                                client_details.nick = user_message.nick;
                                client_details.state = user_message;
                                broadcast(buffer, size);
                                break;

                            case MessageType.ChatMessage:
                                var chat_message = (ChatMessage)m;
                                Console.WriteLine("Got chat message [{0}] {1}: {2}", chat_message.time.DateTime.ToShortTimeString(), client_details.nick, chat_message.message);
                                broadcast(buffer, size);
                                break;

                            case MessageType.PingRequestMessage:
                                var request_message = (PingRequestMessage)m;
                                var recipient = (ClientDetails)clients.ToArray().Single(r => ((ClientDetails)r).userid == request_message.to_userid);
                                request_message.ip = ((IPEndPoint)socket.RemoteEndPoint).Address;
                                Console.WriteLine("Got ping request from {0} ({1}) to {2} ({3}), r{4} l{5}", client_details.userid, client_details.nick, recipient.userid, recipient.nick, request_message.remoteport, request_message.localport);

                                int length;
                                var serialized = Utilities.writeMessage(request_message, out length);
                                var stream2 = recipient.tcp_client.GetStream();
                                stream2.Write(BitConverter.GetBytes(length), 0, sizeof(int));
                                stream2.Write(serialized, 0, length);

                                var request_message2 = new PingRequestMessage(request_message.to_userid, request_message.userid, request_message.localport, request_message.remoteport);
                                request_message2.ip = ((IPEndPoint)recipient.tcp_client.Client.RemoteEndPoint).Address;
                                serialized = Utilities.writeMessage(request_message2, out length);
                                stream2 = client_details.tcp_client.GetStream();
                                stream2.Write(BitConverter.GetBytes(length), 0, sizeof(int));
                                stream2.Write(serialized, 0, length);
                                break;

                            default:
                                Console.WriteLine("Got unknown usermessage of type {0}", m.type);
                                break;
                        }
                    }
                }
            }
        }

        static void broadcast(byte[] msg, int length)
        {
            foreach (ClientDetails client_details in clients)
            {
                var stream = client_details.tcp_client.GetStream();
                stream.Write(BitConverter.GetBytes(length), 0, sizeof(int));
                stream.Write(msg, 0, length);
            }
        }
    }
}
