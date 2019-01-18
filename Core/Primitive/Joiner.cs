﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Primitive
{
    public sealed class Joiner
    {

        private readonly string _glue;

        private Joiner(string glue)
        {
            _glue = glue;
        }

        public static Joiner On(char glue)
        {
            return new Joiner(char.ToString(glue));
        }

        public static Joiner On(string glue)
        {
            return new Joiner(Strings.Of(glue));
        }

        public static Joiner On<T>(T glue)
        {
            return new Joiner(Strings.Of(glue));
        }

        public string Join<T>(IEnumerable<T> enumerable, Func<T, string> generator = null)
        {
            if (enumerable == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            using (var enumerator = enumerable.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    var item = enumerator.Current;
                    builder.Append(generator == null ? Strings.Of<T>(item) : generator(item));
                }

                while (enumerator.MoveNext())
                {
                    var item = enumerator.Current;
                    builder.Append(_glue);
                    builder.Append(generator == null ? Strings.Of<T>(item) : generator(item));
                }

                return builder.ToString();
            }

        }  

    }

}