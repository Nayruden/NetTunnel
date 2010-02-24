using System;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;

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
    [KnownType(typeof(RegistrationMessage))]
    [KnownType(typeof(ChatMessage))]
    [KnownType(typeof(UserMessage))]
    [KnownType(typeof(PingRequestMessage))]
    [KnownType(typeof(PingMessage))]
    public abstract class Message
    {
        [DataMember]
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
        [DataMember]
        public ulong userid;

        public RegistrationMessage(ulong userid)
        {
            this.userid = userid;
            this.type = MessageType.RegistrationMessage;
        }
    }

    [DataContract]
    public class ChatMessage : Message
    {
        [DataMember]
        public string message;

        [DataMember]
        public DateTimeOffset time;

        [DataMember]
        public ulong userid;

        public ChatMessage(ulong userid, string message)
        {
            this.message = message;
            this.userid = userid;
            this.time = DateTimeOffset.Now;
            this.type = MessageType.ChatMessage;
        }
    }

    [DataContract]
    public class UserMessage : Message
    {
        [DataMember]
        public MessageState state;

        [DataMember]
        public string nick;

        [DataMember]
        public ulong userid;

        [DataMember]
        public List<Service> services = new List<Service>();

        public UserMessage( string nick, ulong userid )
        {
            type = MessageType.UserMessage;
            this.nick = nick;
            this.userid = userid;
        }
    }

    [DataContract]
    public class PingRequestMessage : Message
    {
        [DataMember]
        public ulong userid;

        [DataMember]
        public IPAddress ip;

        [DataMember]
        public ulong to_userid;

        /// <summary>
        /// Remote port, the receiver of this message should ping this port.
        /// </summary>
        [DataMember]
        public ushort remoteport;

        /// <summary>
        /// Local port, the receiver of this message should ping from this port.
        /// </summary>
        [DataMember]
        public ushort localport;

        public PingRequestMessage( ulong userid, ulong to_userid, ushort remoteport, ushort localport )
        {
            this.type = MessageType.PingRequestMessage;
            this.userid = userid;
            this.to_userid = to_userid;
            this.remoteport = remoteport;
            this.localport = localport;
        }
    }
}