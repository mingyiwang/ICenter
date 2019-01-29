using System;
using System.Collections.Generic;
using Core.Collection;
using Core.Primitive;

namespace Core
{

    public sealed class Checks
    {

        public static void Is(object obj, Type expected)
        {
            NotNull(expected);
            NotNull(obj);
            if (obj.GetType() != expected)
            {
                Fail<ArgumentException>($"Expected type {expected.Name} but was {obj.GetType().Name}.");
            }
        }

        public static void NotNull(object obj)
        {
            NotNull(obj, "Expected not null but was null.");
        }

        public static void NotNull(object obj, string message)
        {
            NotNull<NullReferenceException>(obj, message);
        }

        public static void NotNull<T>(object obj, string message) where T : Exception
        {
            if (obj == null)
            {
                Fail<T>(message);
            }
        }

        public static void NotNullOrEmpty(string value)
        {

            NotNull(value, "Expected not null but was null.");
            if (value.Trim().Length == 0)
            {
                Fail<ArgumentException>("Expected not empty but was empty.");
            }
        }

        public static void Null(object obj)
        {
            Null(obj, "Expected null but was not null.");
        }

        public static void Null(object obj, string message)
        {
            Null<ArgumentException>(obj, message);
        }

        private static void Null<T>(object obj, string message) where T : Exception
        {
            if (obj != null)
            {
                Fail<T>(message);
            }
        }

        public static void NotNullOrEmpty<TC>(ICollection<TC> collection)
        {
            NotNullOrEmpty<ArgumentException, TC>(collection, "Expected not empty but was empty.");
        }

        public static void NotNullOrEmpty<TC>(ICollection<TC> collection, string message)
        {
            NotNullOrEmpty<ArgumentException, TC>(collection, message);
        }

        private static void NotNullOrEmpty<TE, TC>(ICollection<TC> collection, string message) where TE : Exception
        {
            if (collection == null || collection.Count == 0)
            {
                Fail<TE>(message);
            }
        }

        public static void IsEqual(int actual, int expected)
        {
            if (actual != expected)
            {
                Fail<ArgumentException>($"Expected {expected} but was {actual}.");
            }
        }

        public static void IsEqual(string actual, string expected)
        {
            if (!expected.Equals(actual))
            {
                 Fail<ArgumentException>($"Expected {expected} but was {actual}.");
            }
        }

        public static void IsEqual<T>(T[] actual, T[] expected)
        {
            if (!Arrays.IsEqual(actual, expected))
            {
                Fail<ArgumentException>($"Array is not equal.");
            }
        }

        public static void IsEqual<T>(T actual, T expected)
        {
            IsEqual<ArgumentException>(actual, expected, $"Expected {expected} but was {actual}.");
        }

        public static void IsEqual<T>(T actual, T expected, string message)
        {
            IsEqual<ArgumentException>(actual, expected, message);
        }

        private static void IsEqual<T>(object actual, object expected, string message) where T : Exception
        {

            if (!expected.Equals(actual))
            {
                 Fail<T>(message);
            }
        }

        public static void IsNotEqual(int actual, int expected, string message)
        {
            IsNotEqual<ArgumentException>(actual, expected, message);
        }

        public static void IsNotEqual(object actual, object expected)
        {
            IsNotEqual<ArgumentException>(actual, expected, "Value must not be equal.");
        }

        public static void IsNotEqual<T>(object actual, object expected,  string message) where T : Exception
        {
            if (ReferenceEquals(expected, actual) || expected.Equals(actual))
            {
                Fail<T>(message);
            }
        }

        public static void IsFalse(bool value)
        {
            IsFalse(value, "Expected false but was true.");
        }

        public static void IsFalse(bool value, string message)
        {
            IsFalse<ArgumentException>(value, message);
        }

        public static void IsFalse<T>(bool? value, string message) where T : Exception
        {
            if (value != false)
            {
                Fail<T>(message);
            }
        }

        public static void IsTrue(bool value)
        {
            IsTrue(value, "Expected true but was false.");
        }

        public static void IsTrue(bool value, string message)
        {
            IsTrue<ArgumentException>(value, message);
        }

        public static void IsTrue<T>(bool value, string message) where T : Exception
        {
            if (value != true)
            {
                Fail<T>(message);
            }
        }

        public static void InRange(int min, int max, int actual, string message)
        {
            if (actual < min || actual > max)
            {
                Fail<OverflowException>(message);
            }
        }

        public static void InRange(int actual, Range<int> range, string message)
        {
            if (actual < range.GetStart() || actual > range.GetStart())
            {
                Fail<OverflowException>(message);
            }
        }

        public static void LessThan<T>(int actual, int comparator,  string message) where T : Exception
        {
            if (actual >= comparator)
            {
                Fail<T>(message);
            }
        }

        public static void LessThanOrEqual(int actual, int comparator, string message)
        {
            if (actual > comparator)
            {
                Fail<ArgumentException>(message);
            }
        }

        public static void GreaterThan(int actual, int comparator, string message)
        {
            if (actual <= comparator)
            {
                Fail<ArgumentException>(message);
            }
        }

        public static void GreaterThanOrEqual<T>(int actual, int comparator, string message) where T : Exception
        {
            if (actual < comparator)
            {
                Fail<T>(message);
            }
        }

        private static void Fail<T>(string message) where T : Exception
        {
            var type = typeof(T);

            var constructor = type.GetConstructor(new[]{
                typeof(string)
            });

            if (constructor == null)
            {
                throw new ArgumentException("Exception[" + type.FullName + "] must have a constructor of a single string parameter.");
            }

            throw (T)constructor.Invoke(new object[] {
                   message
            });
        }
    }

}

