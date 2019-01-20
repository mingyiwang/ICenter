using System;
using System.IO;

namespace Core
{
    /// <summary>
    /// Exception throw helper class used for throw exceptions
    /// </summary>
    public sealed class Throws
    {

        public static FileNotFoundException FileNotFound(string message)
        {
            return new FileNotFoundException(message);
        }

        public static InvalidOperationException InvalidOperation(string message)
        {
            return new InvalidOperationException(message);

        }

    }

}