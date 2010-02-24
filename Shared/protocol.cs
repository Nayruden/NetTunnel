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
        public static readonly byte[] MAGIC_NUM = { 0xDE, 0xAD, 0xCA, 0xFE };
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
        PingRequestMessage,
        PingMessage,
    }

    [DataContract]
    [ProtoInclude(1,typeof(RegistrationMessage))]
    [ProtoInclude(11,typeof(ChatMessage))]
    [ProtoInclude(12,typeof(UserMessage))]
    [ProtoInclude(13,typeof(PingRequestMessage))]
    [ProtoInclude(14,typeof(PingMessage))]
    public abstract class Message
    {
        [DataMember(Order=2)]
        public MessageType type;
    }

    [DataContract]
    public class PingMessage : Message
    {
        public PingMessage()
        {
            this.type = MessageType.PingMessage;
        }
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
        public DateTimeOffset time;

        [DataMember(Order = 4)]
        public ulong userid;

        public ChatMessage(ulong userid, string message)
        {
            this.message = message;
            this.userid = userid;
            this.time = DateTimeOffset.Now;
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
    public class PingRequestMessage : Message
    {
        [DataMember(Order = 2)]
        public ulong userid;

        [DataMember(Order = 3)]
        public IPAddress ip;

        [DataMember(Order = 4)]
        public ulong to_userid;

        /// <summary>
        /// Remote port, the receiver of this message should ping this port.
        /// </summary>
        [DataMember(Order = 5)]
        public ushort remoteport;

        /// <summary>
        /// Local port, the receiver of this message should ping from this port.
        /// </summary>
        [DataMember(Order = 6)]
        public ushort localport;

        public PingRequestMessage( ulong userid, ulong to_userid, ushort remoteport, ushort localport )
        {
            this.type = MessageType.PingRequestMessage;
            this.userid = userid;
            this.to_userid = to_userid;
            this.remoteport = remoteport;
            this.localport = localport;
        }

        public PingRequestMessage() { }
    }
}