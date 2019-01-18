using Common;
using Common.Collection;

namespace Core.Primitive
{

    public sealed class Strings
    {

        public static string Of(string value)
        {
            return Of(value, string.Empty);
        }

        /// <summary>
        /// Get value if null then returns defaultIfNull
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultIfNull"></param>
        /// <returns></returns>
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

        public static string Substring(string input, int startIndex, int endIndex)
        {
            Checks.NotNullOrEmpty(input);
            Checks.GreaterThanOrEqual<IndexOutOfRangeException>(startIndex, endIndex, $"End index[{endIndex}] must be greater than start index[{startIndex}].");
            return input.Substring(startIndex, endIndex - startIndex + 1);
        }

        public static string Between(string input, int startIndex, int endIndex)
        {
            // Check bounds
            Checks.NotNullOrEmpty(input);
            Checks.LessThan<IndexOutOfRangeException>(input.Length, startIndex, $"Index[{startIndex}] is out of range.");
            Checks.LessThan<IndexOutOfRangeException>(input.Length, endIndex, $"Index[{endIndex}] is out of range.");
            Checks.GreaterThanOrEqual<IndexOutOfRangeException>(Numbers.Zero, startIndex,$"Index[{startIndex}] can not be negative.");
            Checks.GreaterThanOrEqual<IndexOutOfRangeException>(Numbers.Zero, endIndex,$"Index[{endIndex}] can not be negative.");
            Checks.GreaterThanOrEqual<IndexOutOfRangeException>(startIndex,   endIndex, $"Last index[{endIndex}] must be greater than or equal to index[{startIndex}].");

            var start  = ++startIndex;
            var length = endIndex - start;

            return length <= Numbers.Zero 
                 ? string.Empty 
                 : input.Substring(start, length)
                 ;
            
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
