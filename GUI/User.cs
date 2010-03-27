using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetTunnel
{
    public class User
    {
        private readonly ulong _userid;
        private readonly List<Service> _services = new List<Service>();

        /// <summary>
        /// The remote_userid is used to identify the user across the network.
        /// </summary>
        public ulong userid { get { return _userid; } }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string nick { get; set; }

        /// <summary>
        /// Services this user is offering.
        /// </summary>
        public IList<Service> services { get { return _services; } }
    }
}
