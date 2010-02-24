using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

namespace NetTunnel
{
    class Client
    {
        private static ASCIIEncoding encoding = new ASCIIEncoding();

        private static AutoResetEvent inputRead = new AutoResetEvent(false);
        private static AutoResetEvent readyForInput = new AutoResetEvent(false);

        private static AutoResetEvent streamRead = new AutoResetEvent(false);
        private static AutoResetEvent readyForStream = new AutoResetEvent(false);

        private static volatile string inputLine;
        private static volatile byte[] buffer = new byte[Protocol.MAX_MESSAGE_SIZE];
        private static volatile Message read_message;
        private static volatile NetworkStream stream;
        public static volatile bool exit = false;

        static void readInput()
        {
            try
            {
                while (!exit)
                {
                    inputLine = Console.ReadLine().Trim();
                    inputRead.Set();
                    readyForInput.WaitOne();
                }
            }
            catch (ThreadInterruptedException exception)
            {
                // Don't need to do anything
            }
        }

        static void readStream()
        {
            using (var client = new TcpClient("server.ulyssesmod.net", Protocol.PORT))
            //using (var client = new TcpClient("192.168.0.110", Protocol.PORT))
            using (stream = client.GetStream())            
            {
                try
                {               
                    while (!exit)
                    {
                        read_message = Serializer.DeserializeWithLengthPrefix<Message>(stream, PrefixStyle.Base128);

                        streamRead.Set();
                        readyForStream.WaitOne();
                    }
                }
                catch (IOException exception) // This is thrown when we get rid of the stream object so this thread can exit
                    // Also thrown when server leaves
                {
                    exit = true;
                }
            }

        }

        static void Main(string[] args)
        {
            // Create thread to read from input
            var input_thread = new Thread(readInput);
            input_thread.Start();

            // Create thread to read from stream
            var stream_thread = new Thread(readStream);
            stream_thread.Start();

            var ping_thread = new Thread(Tunnel.run);
            ping_thread.Start();

            while (!exit)
            {
                if (streamRead.WaitOne(100)) // Wait 100 ms for stream to be read
                {
                    if (read_message != null) // Data available
                    {
                        var m = read_message;

                        switch (m.type)
                        {
                            case MessageType.RegistrationMessage:
                                var registration_message = (RegistrationMessage)m;

                                // Send the initial information
                                UserDetails.local_user.nick = "Unnamed";
                                UserDetails.local_user.userid = registration_message.userid;
                                var service = new Service("vent");
                                service.port_ranges = new PortRange[1];
                                service.port_ranges[0] = new PortRange(4149, Protocols.BOTH);
                                UserDetails.local_user.services.Add(service);
                                var message = UserDetails.local_user.toUserMessage(MessageState.Created);
                                Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Base128);
                                break;

                            case MessageType.UserMessage:
                                var user_message = (UserMessage)m;
                                UserDetails user;

                                if (user_message.userid != UserDetails.local_user.userid && user_message.state == MessageState.Created)
                                {
                                    user = new UserDetails();
                                    user.nick = user_message.nick;
                                    user.userid = user_message.userid;
                                    user.services = user_message.services;
                                    UserDetails.add(user);
                                }
                                else
                                    user = UserDetails.getByUserid(user_message.userid);

                                user.nick = user_message.nick;
                                user.services = user_message.services;
                                Console.WriteLine("Got usermessage from {0} state of {1}, nick of {2}", user.userid, user_message.state, user_message.nick);

                                foreach (Service s in user_message.services)
                                {
                                    Console.WriteLine("Service {0} {1}", s.service_name, s.port_ranges[0].start);
                                }

                                if (user_message.state == MessageState.Deleted)
                                    UserDetails.remove(user_message.userid);

                                break;

                            case MessageType.ChatMessage:
                                var chat_message = (ChatMessage)m;
                                Console.WriteLine("[{0}] {1}: {2}", chat_message.time.DateTime.ToShortTimeString(), UserDetails.getByUserid(chat_message.userid).nick, chat_message.message);
                                break;

                            case MessageType.PingRequestMessage:
                                var request_message = (PingRequestMessage)m;
                                var sender = UserDetails.getByUserid(request_message.userid);
                                Console.WriteLine("{0} ({1}) [{4}] is requesting we connect to them from {2} to {3}", sender.userid, sender.nick, request_message.localport, request_message.remoteport, request_message.ip);
                                var tunnel = new Tunnel(request_message.localport, request_message.ip, request_message.remoteport);
                                Tunnel.addTunnel(tunnel);
                                break;

                            default:
                                Console.WriteLine("Got unknown usermessage of type {0}", m.type);
                                break;
                        }
                    }
                    else // Disconnect
                    {
                        Console.WriteLine("Server left");
                        exit = true;
                    }

                    readyForStream.Set();
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

                    Message message;

                    switch (op)
                    {
                        case "c":
                            message = new ChatMessage(UserDetails.local_user.userid, inputLine);
                            Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Base128);
                            break;

                        case "n":
                            UserDetails.local_user.nick = inputLine;
                            message = UserDetails.local_user.toUserMessage();
                            Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Base128);
                            break;

                        case "s":
                            var words = inputLine.Split(' ');
                            var service = new Service(words[0]);
                            service.port_ranges = new PortRange[1];
                            service.port_ranges[0] = new PortRange(ushort.Parse(words[1]), Protocols.BOTH);
                            UserDetails.local_user.services.Add(service);

                            message = UserDetails.local_user.toUserMessage();
                            Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Base128);
                            break;

                        case "t":
                            var peices = inputLine.Split(' ');
                            message = new PingRequestMessage(UserDetails.local_user.userid,ulong.Parse(peices[0]), ushort.Parse(peices[1]), ushort.Parse(peices[2]));
                            Serializer.SerializeWithLengthPrefix(stream, message, PrefixStyle.Base128);;
                            break;

                        case "exit":
                        case "quit":
                            exit = true;                                
                            break;

                        default:
                            Console.WriteLine("Unrecognized input");
                            break;
                    }

                    if (!exit)
                        readyForInput.Set();
                }
            }

            stream.Dispose(); // So the stream thread can stop blocking!
            input_thread.Interrupt();            
        }

    }
}
