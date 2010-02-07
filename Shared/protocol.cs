using System;
using System.Runtime.Serialization;

namespace NetTunnel
{
    [DataContract, KnownType(typeof(TestMessage))]
    public abstract class Message
    {
        public abstract void executeLogic();
    }

    [DataContract]
    public class TestMessage : Message
    {
        [DataMember]
        public string str = "Not SET! BWA HA HA HA!";

        public override void executeLogic()
        {
            Console.WriteLine(str);
        }
    }
}