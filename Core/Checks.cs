using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Core.Collection;
using Core.Primitive;

namespace Core
{

    public sealed class Checks
    {

        public static void Is(object obj, Type expected)
        {
            IsNotNull(expected);
            IsNotNull(obj);
            if (obj.GetType() != expected)
            {
                FileAndThrow<ArgumentException>($"Expected type {expected.Name} but was {obj.GetType().Name}.");
            }
        }

        public static void IsNotNull(object obj)
        {
            IsNotNull(obj, "Expected not null but was null.");
        }

        public static void IsNotNull(object obj, string message)
        {
            IsNotNull<NullReferenceException>(obj, message);
        }

        public static void IsNotNull<T>(object obj, string message) where T : Exception
        {
            if (obj == null)
            {
                FileAndThrow<T>(message);
            }
        }

        public static void IsNotNullOrEmpty(string value)
        {
            IsNotNull(value, "Expected not null but was null.");
            if (value.Trim().Length == 0)
            {
                FileAndThrow<ArgumentException>("Expected not empty but was empty.");
            }
        }

        public static void IsNotNullOrEmpty(string value, string message)
        {
            IsNotNull(value, "Expected not null but was null.");
            if (value.Trim().Length == 0)
            {
                FileAndThrow<ArgumentException>(message);
            }
        }

        public static void IsNull(object obj)
        {
            IsNull(obj, "Expected null but was not null.");
        }

        public static void IsNull(object obj, string message)
        {
            IsNull<ArgumentException>(obj, message);
        }

        private static void IsNull<T>(object obj, string message) where T : Exception
        {
            if (obj != null)
            {
                FileAndThrow<T>(message);
            }
        }

        public static void IsNotNullOrEmpty<TC>(ICollection<TC> collection)
        {
            IsNotNullOrEmpty<ArgumentException, TC>(collection, "Expected not empty but was empty.");
        }

        public static void IsNotNullOrEmpty<TC>(ICollection<TC> collection, string message)
        {
            IsNotNullOrEmpty<ArgumentException, TC>(collection, message);
        }

        private static void IsNotNullOrEmpty<TE, TC>(ICollection<TC> collection, string message) where TE : Exception
        {
            if (collection == null || collection.Count == 0)
            {
                FileAndThrow<TE>(message);
            }
        }

        public static void IsEqual(int actual, int expected)
        {
            if (actual != expected)
            {
                FileAndThrow<ArgumentException>($"Expected {expected} but was {actual}.");
            }
        }

        public static void IsEqual(string actual, string expected)
        {
            if (!expected.Equals(actual))
            {
                 FileAndThrow<ArgumentException>($"Expected {expected} but was {actual}.");
            }
        }

        public static void IsEqual<T>(T[] actual, T[] expected)
        {
            if (!Arrays.IsEqual(actual, expected))
            {
                FileAndThrow<ArgumentException>($"Array is not equal.");
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
                 FileAndThrow<T>(message);
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
                FileAndThrow<T>(message);
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
                FileAndThrow<T>(message);
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
                FileAndThrow<T>(message);
            }
        }

        public static void IsInRange(int min, int max, int actual, string message)
        {
            if (actual < min || actual > max)
            {
                FileAndThrow<OverflowException>(message);
            }
        }

        public static void IsInRange(int actual, Range<int> range, string message)
        {
            if (actual < range.GetStart() || actual > range.GetStart())
            {
                FileAndThrow<OverflowException>(message);
            }
        }

        public static void IsLessThan<T>(int actual, int comparator,  string message) where T : Exception
        {
            if (actual >= comparator)
            {
                FileAndThrow<T>(message);
            }
        }

        public static void IsLessThanOrEqual(int actual, int comparator, string message)
        {
            if (actual > comparator)
            {
                FileAndThrow<ArgumentException>(message);
            }
        }

        public static void IsGreaterThan(int actual, int comparator, string message)
        {
            if (actual <= comparator)
            {
                FileAndThrow<ArgumentException>(message);
            }
        }

        public static void IsGreaterThanOrEqual<T>(int actual, int comparator, string message) where T : Exception
        {
            if (actual < comparator)
            {
                FileAndThrow<T>(message);
            }
        }

        private static void FileAndThrow<T>(string message) where T : Exception
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(new[]{
                typeof(string)
            });

            if (constructor == null)
            {
                throw new ArgumentException("Exception[" + type.FullName + "] must have a constructor of a single string parameter.");
            }

            throw LinqCreateException<T>(message, constructor)();
        }

        private static Func<T> LinqCreateException<T>(string message, ConstructorInfo info) where T : Exception
        {
            return Expression.Lambda<Func<T>>(Expression.New(info, Expression.Constant(message))).Compile();
        }

        
    }

}

