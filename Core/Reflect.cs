using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Core.Concurrent;

namespace Core
{
    public sealed class Reflect
    {

        /// <summary>
        /// This method used to find assembly with specified name.
        /// </summary>
        /// <param name="name">The assembly name e.g A.B.C.file.txt</param>
        /// <returns>Assembly or null if more then one assembly with the same found.</returns>
        public static Assembly GetAssembly(string name)
        {
            Checks.NotNullOrEmpty(name, "Assembly name can not be empty.");
            
            // 1. split by .
            // 2. check every possible combiations
            // 3. returns the first found one.

            var names = name.Split('.');
            foreach (var temp in names)
            {
                
            }

            using (var reader = new StringReader(name))
            {
                int c = reader.Read();
                if (c == '.')
                {
                    
                }
            }

          

            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .AsParallel()
                            .Single(a => name.Equals(a.GetName().Name));

        }

    }
}