using System.Diagnostics;
using System;
using System.Runtime.Serialization;
using System.IO;

public static class StringExtensions
{
    public static string F(this string s, object arg0)
    {
        return string.Format(s, arg0);
    }
    public static string F(this string s, object arg0, object arg1)
    {
        return string.Format(s, arg0, arg1);
    }
    public static string F(this string s, object arg0, object arg1, object arg2)
    {
        return string.Format(s, arg0, arg1, arg2);
    }
    public static string F(this string s, params object[] args)
    {
        return string.Format(s, args);
    }
}

namespace NetTunnel
{
    public class DebugMessage
    {
        [Conditional("DEBUG")]
        public static void msg(string s)
        {
            Console.WriteLine(s);
        }
    }

    public class Utilities
    {
        private static DataContractSerializer ds = new DataContractSerializer(typeof(Message));

        public static Message readMessage( byte[] buffer, int buffer_length )
        {            
            Message m;
            using (var memory_stream = new MemoryStream(buffer, 0, buffer_length))
                m = (Message)ds.ReadObject(memory_stream);

            return m;
        }

        public static byte[] writeMessage(Message message, out int length)
        {
            var memory_stream = new MemoryStream();
            ds.WriteObject(memory_stream, message);

            length = (int)memory_stream.Length;
            return memory_stream.GetBuffer();
        }
    }
}
