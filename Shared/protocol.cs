using System;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using ProtoBuf;

namespace NetTunnel
{
    public class Protocol
    {
        public static readonly ushort PORT = 4141;
        public static readonly byte[] MAGIC_NUM = { 0xDE, 0xAD, 0xCA, 0xFE, 0xBE, 0xEF, 0xBA, 0xBE };
        public static readonly int MAX_MESSAGE_SIZE = 1024;
    }

    public enum MessageState
    {
        Created,
        Modified,
        Deleted,
    }

    public enum MessageType
    {
        Unknown, // This is the default if left unspecified
        ChatMessage,
        UserMessage,
        RegistrationMessage,
        TunnelRequestMessage,
        TryTunnelMessage,
        OpenPortMessage,
    }

    [DataContract]
    [ProtoInclude(1,typeof(RegistrationMessage))]
    [ProtoInclude(11,typeof(ChatMessage))]
    [ProtoInclude(12,typeof(UserMessage))]
    [ProtoInclude(13,typeof(TunnelRequestMessage))]
    [ProtoInclude(14, typeof(TryTunnelMessage))]
    [ProtoInclude(15, typeof(OpenPortMessage))]
    public abstract class Message
    {
        [DataMember(Order=2)]
        public MessageType type;
    }

    [DataContract]
    public class RegistrationMessage : Message
    {
        [DataMember(Order = 2)]
        public ulong userid;

        public RegistrationMessage(ulong userid)
        {
            this.userid = userid;
            this.type = MessageType.RegistrationMessage;
        }

        public RegistrationMessage() { }
    }

    [DataContract]
    public class ChatMessage : Message
    {
        [DataMember(Order = 2)]
        public string message;

        [DataMember(Order = 3)]
        public DateTime time;

        [DataMember(Order = 4)]
        public ulong userid;

        public ChatMessage(ulong userid, string message)
        {
            this.message = message;
            this.userid = userid;
            this.time = DateTime.Now;
            this.type = MessageType.ChatMessage;
        }

        public ChatMessage() { }
    }

    [DataContract]
    public class UserMessage : Message
    {
        [DataMember(Order = 2)]
        public MessageState state;

        [DataMember(Order = 3)]
        public string nick;

        [DataMember(Order = 4)]
        public ulong userid;

        [DataMember(Order = 5)]
        public List<Service> services = new List<Service>();

        public UserMessage( string nick, ulong userid )
        {
            type = MessageType.UserMessage;
            this.nick = nick;
            this.userid = userid;
        }

        public UserMessage() { }
    }

    [DataContract]
    public class TunnelRequestMessage : Message
    {
        [DataMember(Order = 2)]
        public ulong userid;

        public TunnelRequestMessage(ulong userid)
        {
            type = MessageType.TunnelRequestMessage;
            this.userid = userid;
        }

        public TunnelRequestMessage() { }
    }

    [DataContract]
    public class OpenPortMessage : Message
    {
        [DataMember(Order = 2)]
        public ushort port;

        [DataMember(Order = 3)]
        public ulong userid;

        [DataMember(Order = 4)]
        public ulong request_num;

        public static ulong last_request = 0;

        public OpenPortMessage(ushort port, ulong userid, ulong request_num)
        {
            this.port = port;
            this.userid = userid;
            this.request_num = request_num;
            type = MessageType.OpenPortMessage;
        }

        public OpenPortMessage()
        {
            type = MessageType.OpenPortMessage;
        }
    }

    [DataContract]
    public class TryTunnelMessage : Message
    {
        [DataMember(Order = 2)]
        public ulong userid;

        [DataMember(Order = 3)]
        public IPAddress endpoint_address; // PORT: Would want to store both internal and external

        [DataMember(Order = 4)]
        public ushort endpoint_port;

        [DataMember(Order = 5)]
        public ushort origin_port;

        public IPEndPoint endpoint { get { return new IPEndPoint( endpoint_address, endpoint_port ); } }
        public IPEndPoint origin { get { return new IPEndPoint(IPAddress.Any, origin_port); } }

        public TryTunnelMessage(ulong userid, IPEndPoint endpoint, IPEndPoint origin)
        {
            type = MessageType.TryTunnelMessage;
            this.userid = userid;
            this.endpoint_port = (ushort)endpoint.Port;
            this.endpoint_address = endpoint.Address;
            this.origin_port = (ushort)origin.Port;
        }

        public TryTunnelMessage() { }
    }
}