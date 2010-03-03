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

    public class Utilities
    {
        public static Int16 reverseEndian(Int16 n)
        {
            return (Int16)(n >> 8 | n << 8);
        }

        public static UInt16 reverseEndian(UInt16 n)
        {
            return (UInt16)(n >> 8 | n << 8);
        }

        public static Int32 reverseEndian(Int32 n)
        {
            return n >> 24 | (n & 0x00FF0000) >> 8 | (n & 0x0000FF00) << 8 | n << 24;
        }

        public static UInt32 reverseEndian(UInt32 n)
        {
            return n >> 24 | (n & 0x00FF0000) >> 8 | (n & 0x0000FF00) << 8 | n << 24;
        }

        public static Int64 reverseEndian(Int64 n)
        {
            return n >> 56 | (n & 0x00FF000000000000) >> 40 | (n & 0x0000FF0000000000) >> 24 | (n & 0x000000FF00000000) >> 8 |
                (n & 0x00000000FF000000) << 8 | (n & 0x0000000000FF0000) << 24 | (n & 0x000000000000FF00) << 40 | n << 56;
        }

        public static UInt64 reverseEndian(UInt64 n)
        {
            return n >> 56 | (n & 0x00FF000000000000) >> 40 | (n & 0x0000FF0000000000) >> 24 | (n & 0x000000FF00000000) >> 8 |
                (n & 0x00000000FF000000) << 8 | (n & 0x0000000000FF0000) << 24 | (n & 0x000000000000FF00) << 40 | n << 56;
        }
    }
}
