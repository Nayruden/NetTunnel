using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Shared
{
    public enum MessageType
    {
        Unknown, // Default if left unspecified
        Registration,
    }

    [DataContract]
    [ProtoInclude(11, typeof(RegistrationMessage))]
    public class Message
    {
        [DataMember(Order = 1)]
        public MessageType type;
    }

    public class RegistrationMessage : Message
    {
        [DataMember(Order = 1 )]
        public ulong userid { get; private set; }

        public RegistrationMessage( ulong userid )
        {
            type = MessageType.Registration;
            this.userid = userid;
        }
    }
}
