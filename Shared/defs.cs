using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetTunnel
{
    [Flags]
    public enum Protocols
    {
        NONE = 0, // Special case
        TCP = 1,
        UDP = 2,
        BOTH = TCP | UDP
    }

    [DataContract]
    public class PortRange
    {
        [DataMember(Order = 1)]
        public ushort start;

        [DataMember(Order = 2)]
        public ushort end;

        [DataMember(Order = 3)]
        public Protocols protocols;

        public PortRange(ushort port, Protocols protocols)
        {
            start = end = port;
            this.protocols = protocols;
        }

        public PortRange(ushort start, ushort end, Protocols protocols)
        {
            this.start = start;
            this.end = end;
            this.protocols = protocols;
        }

        public PortRange() { }

        public bool parseRange(string text, out ushort start, out ushort end)
        {
            if (!Regex.Match(text, @"^\d+-?\d*$").Success)
            {
                start = end = 0;
                return false;
            }

            var pieces = text.Split('-');
            // Get first num
            if (!ushort.TryParse(pieces[0], out start))
            {
                end = 0;
                return false;
            }
            if (pieces.Length > 1)
            {
                // Get second num
                if (!ushort.TryParse(pieces[1], out end))
                    return false;
            }
            else
                end = start; // Not a range

            if (end < start)
                return false;

            return true;
        }

        public bool parseRange(string text)
        {
            return parseRange(text, out this.start, out this.end);
        }
    }

    [DataContract]
    public class Service : IComparable<Service>
    {
        [DataMember(Order = 1)]
        public string service_name;

        [DataMember(Order = 2)]
        public bool enabled = true;

        [DataMember(Order = 3)]
        public PortRange[] port_ranges = null;

        public Service(string service_name)
        {
            this.service_name = service_name;
        }

        public Service() { }

        public override string ToString()
        {
            return service_name;
        }

        public int CompareTo(Service service)
        {
            return service_name.CompareTo(service.service_name);
        }
    }

    // Just a small collection of known services to help in various places in the program
    public class KnownServices
    {
        public static readonly Service[] services;

        static KnownServices()
        {
            var ventrilo = new Service("Ventrilo");
            ventrilo.port_ranges = new[] 
        {
            new PortRange( 3784, Protocols.TCP )
        };

            var srcds = new Service("Srcds");
            srcds.port_ranges = new[] 
        {
            new PortRange( 27005, Protocols.UDP ),
            new PortRange( 27015, Protocols.BOTH ),
            new PortRange( 27020, Protocols.UDP )
        };

            var cities = new Service("Cities Online");
            cities.port_ranges = new[] 
        {
            new PortRange( 7176, Protocols.TCP )
        };

            var apache = new Service("Apache");
            apache.port_ranges = new[] 
        {
            new PortRange( 80, Protocols.TCP )
        };

            services = new[] { apache, cities, srcds, ventrilo };
        }

        public static Service get(string service_name)
        {
            return services.FirstOrDefault(service => service.service_name == service_name);
        }
    }

    /// <summary>
    /// Services we are currently sharing with the world.
    /// </summary>
    public class SharedServices // TODO: Is this needed anymore?
    {
        public static readonly List<Service> services = new List<Service>();

        static SharedServices()
        {
            // TODO, persist and stuff
            services.AddRange(new[] { KnownServices.get("Apache"), KnownServices.get("Ventrilo") });
        }
    }

}
