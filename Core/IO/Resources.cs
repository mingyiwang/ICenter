using System;
using System.IO;

namespace Core.IO
{

    public sealed class Resources
    {
         
        public static byte[] GetBytes(Type type, string resourceName)
        {
            return Streams.GetBytes(GetStream(type, resourceName));
        }

        public static string GetString(Type type, string resourceName)
        {
            return Streams.GetString(GetStream(type, resourceName));
        }

        private static Stream GetStream(Type type, string resourceName)
        {
            Checks.NotNull(type, "Type can not be null.");
            Checks.NotNullOrEmpty("Resource name can not be null or empty.");
            return type.Assembly.GetManifestResourceStream(type, resourceName);
        }

    }
}