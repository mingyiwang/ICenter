using System;
using System.IO;

namespace Core.IO
{

    public sealed class Resources
    {
         
        public static byte[] GetBytes(Type type, string resourceName)
        {
            using (var s = GetStream(type, resourceName))
            {
                return Streams.GetBytes(s);
            }
        }

        public static string GetString(Type type, string resourceName)
        {
            using (var s = GetStream(type, resourceName))
            {
                return Streams.GetString(s);
            }
        }

        private static Stream GetStream(Type type, string resourceName)
        {
            Checks.NotNullOrEmpty(resourceName);
            Checks.NotNull(type, "Type can not be null.");
            return type.Assembly.GetManifestResourceStream(type, resourceName);
        }

    }
}