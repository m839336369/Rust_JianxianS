using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace JianxianS.Core
{
    public static class StringHelper
    {
        public enum CapitalizeCase
        {
            First,
            All
        }

        public static bool IsEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static string Args(this string str, object arg0)
        {
            return string.Format(str, arg0);
        }

        public static string Args(this string str, object arg0, object arg1)
        {
            return string.Format(str, arg0, arg1);
        }

        public static string Args(this string str, object arg0, object arg1, object arg2)
        {
            return string.Format(str, arg0, arg1, arg2);
        }

        public static string Args(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static uint ToUInt32(this string value)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(2);
            }
            uint result;
            try
            {
                result = uint.Parse(value, NumberStyles.HexNumber);
            }
            catch (Exception)
            {
                result = 0u;
            }
            return result;
        }
        public static T ToEnum<T>(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                input = "0";
            }
            return (T)((object)Enum.Parse(typeof(T), input, true));
        }

        public static bool ToBool(this string input)
        {
            input = input.Trim().ToUpper();
            return input == "ENABLED" || input == "ENABLE" || input == "TRUE" || input == "YES" || input == "ON" || input == "Y" || input == "1";
        }

        public static List<int> Int32List(this string[] value)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < value.Length; i++)
            {
                int item;
                if (int.TryParse(value[i], out item) && !list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<uint> UInt32List(this string[] value)
        {
            List<uint> list = new List<uint>();
            for (int i = 0; i < value.Length; i++)
            {
                uint item;
                if (uint.TryParse(value[i], out item) && !list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<long> Int64List(this string[] value)
        {
            List<long> list = new List<long>();
            for (int i = 0; i < value.Length; i++)
            {
                long item;
                if (long.TryParse(value[i], out item) && !list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<ulong> UInt64List(this string[] value)
        {
            List<ulong> list = new List<ulong>();
            for (int i = 0; i < value.Length; i++)
            {
                ulong item;
                if (ulong.TryParse(value[i], out item) && !list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}