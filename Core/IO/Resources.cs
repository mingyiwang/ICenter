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

        public static string GetString(string resourcePath)
        {
            using (var s = GetStream(resourcePath))
            {
                return Streams.GetString(s);
            }
        }

        /// <summary>
        /// Retrieve the stream based on the resource name e.g. Resource.txt,
        /// resource should be located in the same location as type specified.
        /// </summary>
        private static Stream GetStream(Type type, string resourceName)
        {
            Checks.IsNotNullOrEmpty(resourceName);
            Checks.IsNotNull(type, "Type can not be null.");
            
            return type.Assembly
                       .GetManifestResourceStream(type, resourceName);
        }

        /// <summary>
        /// Retrieve the stream based on the full resource path e.g A.B.C.Resource.txt.
        /// </summary>
        /// Todo: Change for loop to string splitter solution in case there are hundreds of assemblies.
        private static Stream GetStream(string resourcePath)
        {
            Checks.IsNotNullOrEmpty(resourcePath);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!resourcePath.Contains(assembly.GetName().Name))
                {
                     continue;
                }
                
                var stream = assembly.GetManifestResourceStream(resourcePath);
                if (stream != null)
                {
                    return stream;
                }
            }

            throw new FileNotFoundException("Can not find resource on {resourcePath}.");
        }

    }
}