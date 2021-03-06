﻿using System;
using Common.Collection;

namespace Common.Primitive
{

    public sealed class Strings
    {

        public static string Of(string value, string defaultIfNull)
        {
            return value ?? defaultIfNull;
        }

        public static string Of(string value)
        {
            return Of(value, string.Empty);
        }

        public static string Of<T>(T obj, string defaultIfNull)
        {
            return obj == null ? defaultIfNull : obj.ToString();
        }

        public static string Of<T>(T obj)
        {
            return Of(obj, string.Empty);
        }

        public static string Substring(string input, int startIndex, int endIndex)
        {
            // Check range
            Checks.IsTrue<IndexOutOfRangeException>(endIndex >= startIndex, $"End Index[{endIndex}] must be greater than Start Index[{startIndex}].");
            return input.Substring(startIndex, endIndex - startIndex + 1);
        }

        public static string Between(string input, int startIndex, int endIndex)
        {
            // Check bounds
            Checks.IsTrue<IndexOutOfRangeException>(startIndex >=0 , $"Index[{startIndex}] can not be negative.");
            Checks.IsTrue<IndexOutOfRangeException>(endIndex >= 0,   $"Index[{endIndex}] can not be negative.");
            Checks.LessThan<IndexOutOfRangeException>(input.Length, startIndex, $"Index[{startIndex}] is out of range.");
            Checks.LessThan<IndexOutOfRangeException>(input.Length, endIndex,   $"Index[{endIndex}] is out of range.");
            Checks.IsTrue<IndexOutOfRangeException>(endIndex >= startIndex,       $"End Index[{endIndex}] must be greater than Or Equal Start Index[{startIndex}].");

            var length = endIndex - startIndex - 1;
            if (length == 0 || length == -1)
            {
                return string.Empty;
            }

            return input.Substring(startIndex + 1, length);
        }

        public static string[] Split(string input, params char[] characters)
        {
            return Split(input, StringSplitOptions.RemoveEmptyEntries, characters);
        }

        public static string[] Split(string input, StringSplitOptions options, params char[] characters)
        {
            Checks.NotNull(characters, "Seperator can not be null.");
            return string.IsNullOrEmpty(input) ? Arrays.Empty<string>() : input.Split(characters, options);
        }

    }

}
