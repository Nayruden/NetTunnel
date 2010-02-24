using System.Diagnostics;
using System;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using ProtoBuf;

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
}
