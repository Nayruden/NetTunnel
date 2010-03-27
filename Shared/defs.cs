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
        /// <summary>
        /// 'NONE' needs to be defined for the serializer
        /// </summary>
        NONE = 0,
        TCP = 1,
        UDP = 2,
        BOTH = TCP | UDP
    }

    [DataContract]
    public class PortRange
    {
        [DataMember(Order = 1)]
        public ushort start { get; set; }

        [DataMember(Order = 2)]
        public ushort end { get; set; }

        [DataMember(Order = 3)]
        public Protocols protocols { get; set; }

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

        /// <summary>
        /// Parses a string into a port range. Recognizes inputs like "80" and "100-120".
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="start">The starting port resulting from the text parse.</param>
        /// <param name="end">The ending port resulting from the text parse.</param>
        /// <returns>A bool stating whether or not it was successfully parsed.</returns>
        public static bool parseRange(string text, out ushort start, out ushort end)
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

        /// <summary>
        /// See <see cref="Parse(string, out ushort, out ushort)"/> for details on this function, which acts on this
        /// instance instead of returning values like <see cref="Parse(string, out ushort, out ushort)"/>.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A bool stating whether or not it was successfully parsed.</returns>
        public bool parseRange(string text)
        {
            ushort start, end;
            var result = parseRange(text, out start, out end);
            if (!result)
                return result;
            else
            {
                this.start = start;
                this.end = end;
                return result;
            }
        }
    }

    /// <summary>
    /// Keeps track of services offered. IE, srcds with several ports on various protocols.
    /// </summary>
    [DataContract]
    public class Service : IComparable<Service>
    {
        /// <summary>
        /// The name of the service. IE, 'apache'.
        /// </summary>
        [DataMember(Order = 1)]
        public string service_name { get; set; }

        /// <summary>
        /// Whether or not the service is enabled for the client.
        /// </summary>
        [DataMember(Order = 2)]
        public bool enabled { get; set; }

        /// <summary>
        /// The ports this service uses.
        /// </summary>
        [DataMember(Order = 3)]
        public IList<PortRange> port_ranges { get; set; }

        public Service(string service_name)
            : this()
        {
            this.service_name = service_name;
        }

        private Service()
        {
            enabled = true;
            port_ranges = new List<PortRange>();
        }

        public override string ToString()
        {
            return service_name;
        }

        public int CompareTo(Service service)
        {
            return service_name.CompareTo(service.service_name);
        }
    }

    /// <summary>
    /// Just a small collection of known services to help in various places in the program
    /// </summary>
    public class KnownServices
    {
        public static readonly Service[] services;

        static KnownServices()
        {
            var ventrilo = new Service("Ventrilo");
            ventrilo.port_ranges.Add(new PortRange(3784, Protocols.TCP));

            var srcds = new Service("Srcds");
            srcds.port_ranges.Add(new PortRange(27005, Protocols.UDP));
            srcds.port_ranges.Add(new PortRange(27015, Protocols.BOTH));
            srcds.port_ranges.Add(new PortRange(27020, Protocols.UDP));

            var cities = new Service("Cities Online");
            cities.port_ranges.Add( new PortRange( 7176, Protocols.TCP ) );

            var apache = new Service("Apache");
            apache.port_ranges.Add( new PortRange( 80, Protocols.TCP ) );

            services = new[] { apache, cities, srcds, ventrilo };
        }

        public static Service get(string service_name)
        {
            // Future: Make this into a hashmap if it grows large
            return services.FirstOrDefault(service => service.service_name == service_name);
        }
    }

    /// <summary>
    /// Services we are currently sharing with the world. TODO: REMOVE THIS.
    /// </summary>
    public class SharedServices
    {
        public static readonly List<Service> services = new List<Service>();

        static SharedServices()
        {
            // TODO, persist and stuff
            services.AddRange(new[] { KnownServices.get("Apache"), KnownServices.get("Ventrilo") });
        }
    }
}
