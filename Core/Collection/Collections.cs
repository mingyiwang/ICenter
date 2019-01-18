using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Aliyun.Acs.Cdn.Model.V20141111;
using Core.Primitive;

namespace Core.Collection
{

    public sealed class Collections
    {

        public static string JoinAsString<T>(IEnumerable<T> collection)
        {
            var builder = new StringBuilder();
            using (var enumerator = collection.GetEnumerator())
            {
                enumerator.MoveNext();
                do
                {
                    builder.Append(Strings.Of(enumerator.Current));
                }
                while (enumerator.MoveNext());
                return builder.ToString();
            }
        
        }

        public static string JoinAsString<T>(IEnumerable<T> collection, Func<T, string> convert)
        {
            var builder = new StringBuilder();
            using (var enumerator = collection.GetEnumerator())
            {
                enumerator.MoveNext();
                do
                {
                    builder.Append(Strings.Of(convert(enumerator.Current)));
                }
                while (enumerator.MoveNext());
                return builder.ToString();
            }

        }

        public static string Join<T>(char character, IEnumerable<T> collection)
        {
            return Join<char, T>(character, collection);
        }

        public static string Join<T>(char character, IEnumerable<T> collection, Func<T, string> convert)
        {
            return Join<char, T>(character, collection, convert);
        }

        public static string Join<TJ, T>(TJ character, IEnumerable<T> collection)
        {
            var joiner = Strings.Of(character);
            var builder = new StringBuilder();

            var enumerator = collection.GetEnumerator();
            if(enumerator.MoveNext())
            {
                builder.Append(Strings.Of(enumerator.Current));
            }

            while(enumerator.MoveNext())
            {
                builder.Append(joiner);
                builder.Append(Strings.Of(enumerator.Current));
            }

            return builder.ToString();
        }

        public static string Join<TJ, T>(TJ character, IEnumerable<T> collection, Func<T, string> convert)
        {
            var joiner = Strings.Of(character);
            var builder = new StringBuilder();

            var enumerator = collection.GetEnumerator();
            if(enumerator.MoveNext())
            {
                builder.Append(Strings.Of(convert(enumerator.Current)));
            }

            while(enumerator.MoveNext())
            {
                builder.Append(joiner);
                builder.Append(Strings.Of(convert(enumerator.Current)));
            }

            return builder.ToString();
        }

        public static IReadOnlyCollection<TModel> AsReadOnly<TModel>(IList<TModel> collection)
        {
            return new ReadOnlyCollection<TModel>(collection);
        }

        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue> enumrable)
        {
            return new ReadOnlyDictionary<TKey, TValue>(enumrable);
        }

        public static bool Equals<TKey, TValue>(IDictionary<TKey, TValue> dic1, IDictionary<TKey, TValue> dic2)
        {
            if(ReferenceEquals(dic1, dic2))
            {
                return true;
            }

            var dicCount1  = dic1.Count;
            var dicCount2  = dic2.Count;
            if (dicCount1 != dicCount2)
            {
                return false;
            }

            if (dicCount1 == 0)
            {
                return true;
            }

            if (dicCount2 >= 10000)
            {
                return !(dic1.Keys.AsParallel().Any(key => (!dic2.ContainsKey(key) || !dic2[key].Equals(dic1[key]))));
            }

            return !(dic1.Keys.Any(key => (!dic2.ContainsKey(key) || !dic2[key].Equals(dic1[key]))));

        }

        public static void InsertFirst<T>(List<T> collections, T obj)
        {
              collections.Insert(0, obj);
        }

        public static void InsertLast<T>(List<T> collections, T obj)
        {
            var size = collections.Count;
            if (size == 0)
            {
                collections.Add(obj);
            }
            else
            {
                collections.Insert(collections.Count, obj);
            }
        }

    }
}