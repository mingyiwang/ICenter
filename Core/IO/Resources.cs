using System;
using System.IO;
using System.Reflection;

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

        public static string GetString(string resourcePath)
        {
            using (var s = GetStream(resourcePath))
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

        private static Stream GetStream(string resourcePath)
        {
            Checks.NotNullOrEmpty(resourcePath);
            // Find assembly.
            Assembly assembly;
            /*Checks.NotNull(assembly, "{resourcePath} doesn't exist.");

            return assembly.GetManifestResourceStream(resourcePath);*/
            var chars = resourcePath.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                
            }
            return null;
        }

    }
}