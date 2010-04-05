using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Shared;
using System.Diagnostics;
using ProtoBuf;
using NetTunnel.Properties;

namespace NetTunnel
{
    class ServerCommunicationHandler
    {
        private readonly Tunnel.TunnelManager _tunnel_manager;
        private readonly UserManager _user_manager;
        private readonly TcpClient _server_connection;
        private readonly IPEndPoint _server_endpoint;
        private readonly CancellationToken _cancel_token;
        private readonly MainWindow _main_window;
        private readonly List<Message> _messages; // Lock when accessing, holds outgoing messages

        public ServerCommunicationHandler(MainWindow main_window, CancellationToken cancel_token, Tunnel.TunnelManager tunnel_manager, UserManager user_manager, IPEndPoint server_endpoint)
        {
            _main_window = main_window;
            _cancel_token = cancel_token;
            _tunnel_manager = tunnel_manager;
            _server_endpoint = server_endpoint;
            _server_connection = new TcpClient();
            _messages = new List<Message>();
            _user_manager = user_manager;
        }

        public void QueueMessage(Message message)
        {
            lock (_messages)
                _messages.Add(message);
        }

        public void Run()
        {
            try
            {
                Trace.TraceInformation("Attempting to connect to {0}...", _server_endpoint.ToString());
                _server_connection.Connect(_server_endpoint);
                Trace.TraceInformation("Connection successful!");
            }
            catch (SocketException e)
            {
                Trace.TraceError("Connection timed out, shutting down");
                _main_window.Invoke((Action<string>)_main_window.Error, "Could not connect to server at " + _server_endpoint.ToString());
                return; // Don't continue
            }

            using (_server_connection)
            using (var stream = _server_connection.GetStream())
            {
                while (!_cancel_token.IsCancellationRequested)
                {
                    lock (_messages)
                    {
                        _messages.ForEach(m => Serializer.SerializeWithLengthPrefix(stream, m, PrefixStyle.Base128));
                        _messages.Clear();
                    }

                    if (_server_connection.Available > 0)
                    {
                        var message = Serializer.DeserializeWithLengthPrefix<Message>(stream, PrefixStyle.Base128);

                        switch (message.type)
                        {
                            case MessageType.Registration:
                                HandleRegistration((RegistrationMessage)message);
                                break;

                            case MessageType.Unknown:
                                Trace.TraceWarning("Got an unknown message type");
                                break;

                            default:
                                Trace.TraceError("Got a warning that I have no idea what to do with (is {0})", message.type);
                                break;
                        }
                    }
                }
            }
        }

        private void HandleRegistration(RegistrationMessage registrationMessage)
        {
            Trace.TraceInformation("Got our registration from the server, we've been assigned a userid of {0}", registrationMessage.userid);
            var user = new User(registrationMessage.userid);
            user.nick = Settings.Default.Nick;
            _user_manager.local_user = user;
        }
    }
}
