using System;

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