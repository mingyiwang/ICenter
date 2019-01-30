using System;
using System.Text;
using Core.IO;

namespace Core.Primitive
{

    public sealed class Strings
    {

        public static readonly string Empty = string.Empty;

        public static string Of(string value)
        {
            return Of(value, string.Empty);
        }

        public static string Of(string value, string defaultIfNull)
        {
            return value ?? defaultIfNull;
        }

        public static string Of<T>(T obj)
        {
            return Of(obj, string.Empty);
        }

        public static string Of<T>(T obj, string defaultIfNull)
        {
            return obj == null ? defaultIfNull : obj.ToString();
        }

        public static string GetSubstring(string input, int startIndex, int endIndex)
        {
            Checks.IsNotNullOrEmpty(input);
            Checks.IsGreaterThanOrEqual<IndexOutOfRangeException>(startIndex, 0, $"Start index[{startIndex}] can not be negative.");
            Checks.IsGreaterThanOrEqual<IndexOutOfRangeException>(endIndex,   0, $"End index[{endIndex}] can not be negative.");
            Checks.IsGreaterThanOrEqual<IndexOutOfRangeException>(endIndex,   startIndex, $"End index[{endIndex}] must be greater than start index[{startIndex}].");
            var length = endIndex - startIndex + 1;
            return input.Substring(startIndex, length);
        }

        public static bool TryGetSubstring(string input, ref string result, int startIndex, int endIndex)
        {
            result = Empty;
            try
            {
                result = GetSubstring(input, startIndex, endIndex);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetRightInnerSubstring(string input, int startIndex, int endIndex)
        {
            var end = endIndex - 1;
            return GetSubstring(input, startIndex, end);
        }

        public static string GetLeftInnerSubstring(string input, int startIndex, int endIndex)
        {
            var start = startIndex - 1;
            return GetSubstring(input, start, endIndex);
        }

        public static string GetInnerSubstring(string input, int startIndex, int endIndex)
        {
            var start = startIndex + 1;
            var end = endIndex - 1;
            return GetSubstring(input, start, end);
        }

        public static bool TryGetInnerSubstring(string input, ref string result, int startIndex, int endIndex)
        {
            result = Empty;
            try
            {
                result = GetInnerSubstring(input, startIndex, endIndex);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static byte[] GetBytes(string content)
        {
            return Encoding.UTF8.GetBytes(content);
        }

    }

}
