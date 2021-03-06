﻿using System;
using System.Linq;

namespace Common.Collection
{

    public sealed class Arrays
    {
         
        public static byte[]     EmptyBytesArray  => Empty<byte>();
        public static string[]   EmptyStringArray  => Empty<string>();
        public static int[]       EmptyIntArray       => Empty<int>();

        public static bool IsEmpty<T>(T[] array)
        {
            return array == null || array.Length == 0;
        }

        public static T[] Empty<T>()
        {
            return Empty<T>(0);
        }

        public static T[] Empty<T>(int length)
        {
            return new T[length];
        }

        public static T[] Make<T>(int length)
        {
            return Make(length, default(T));
        }

        public static T[] Make<T>(int length, T defautValue)
        {
            var array = new T[length];
            for(var i = 0; i < length; i++)
            {
                array[i] = defautValue;
            }
            return array;
        }

        public static void Reverse<T>(ref T[] array)
        {
            Checks.NotNull(array);
            array = array.Reverse().ToArray();
        }

        public static T[] Reverse<T>(T[] array)
        {
            Checks.NotNull(array);
            var copy = Make<T>(array.Length);
            var j = 0;

            for (var i = array.Length - 1; i >= 0; i--)
            {
                copy[j] = array[i];
                j++;
            }

            return copy;
        }

        public static T[] Resize<T>(ref T[] source, int newLength)
        {
            return CopyOf(source, 0, newLength);
        }

        public static T[] CopyOf<T>(T[] source)
        {
            return CopyOf(source, 0, source.Length);
        }

        public static T[] CopyOf<T>(T[] source, int startIndex, int length)
        {
            if(startIndex > length)
            {
                throw new IndexOutOfRangeException();
            }

            var originalLength = source.Length;
            if(startIndex < 0 || startIndex > originalLength)
            {
                throw new IndexOutOfRangeException();
            }

            var resultLength = length - startIndex;
            var target = new T[resultLength];
            Array.Copy(source, startIndex, target, 0, Math.Min(resultLength, (originalLength - startIndex)));
            return target;
        }

        public static void CopyTo<T>(T[] source, ref T[] target)
        {
            CopyTo(source, 0, source.Length, ref target, 0);
        }

        public static void CopyTo<T>(T[] source, int startIndex, ref T[] target)
        {
            CopyTo(source, startIndex, source.Length, ref target, 0);
        }

        public static void CopyTo<T>(T[] source, int startIndex, int length, ref T[] target, int targetStartIndex)
        {
            if(startIndex > source.Length || length > (source.Length - startIndex))
            {
                throw new IndexOutOfRangeException();
            }

            if(targetStartIndex > target.Length)
            {
                throw new IndexOutOfRangeException();
            }

            var availableLength = target.Length - targetStartIndex;
            if(length <= availableLength)
            {
                Array.Copy(source, startIndex, target, targetStartIndex, length);
            }
            else
            {
                target = Resize(ref target, (target.Length + (length - availableLength)));
                Array.Copy(source, startIndex, target, targetStartIndex, length);
            }
        }

        public static T[] DeepCopyOf<T>(T[] source, int startIndex, int length)
        {
            return source;
        }

        public static T[] DeepCopyOf<T>(T[] source, int startIndex, int length, Func<T, T> copyFunc)
        {
            return source;
        }

        public static bool Equals<T>(T[] array1, T[] array2)
        {
            if (array1 == null || array2 == null)
            {
                return false;
            }

            if (ReferenceEquals(array1, array2))
            {
                return true;
            }

            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (var i = 0; i < array1.Length; i++)
            {
                if (array1[i] == null || array2[i] == null)
                {
                    return false;
                }

                if(!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }

            return true;
        }


    }
}