using System;
using System.IO;

namespace Core.IO
{

    public sealed class Resources
    {
         
        public static byte[] GetBytes(Type type, string resource)
        {
            using (var s = GetStream(type, resource))
            {
                return Streams.GetBytes(s);
            }
        }

        public static string GetString(Type type, string resource)
        {
            using (var s = GetStream(type, resource))
            {
                return Streams.GetString(s);
            }
        }

        private static Stream GetStream(Type type, string resource)
        {
            Checks.NotNullOrEmpty(resource);
            Checks.NotNull(type, "Type can not be null.");

            return type.Assembly.GetManifestResourceStream(type, resource);
        }

    }
}