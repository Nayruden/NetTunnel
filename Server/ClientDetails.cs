using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace NetTunnel
{
    class ClientDetails
    {
        public string nick;
        public TcpClient tcp_client;
        public readonly ulong userid;
        public UserMessage state;

        public static ulong last_id = 0;

        public ClientDetails()
        {
            userid = ++last_id;
        }
    }
}
