using System;
using System.Threading;
using Core.Concurrent;

namespace Core
{
    public sealed class Lazys
    {

        public static Lazy<T> Of<T>(Func<T> creator)
        {
            return new Lazy<T>(creator);
        }

    }


}