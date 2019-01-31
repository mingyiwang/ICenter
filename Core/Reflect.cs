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
        /// This method used to find assembly with specified assembly name.
        /// </summary>
        /// <param name="name">The assembly name</param>
        /// <returns>Assembly or null if more then one assembly with the same found.</returns>
        public static Assembly GetAssembly(string name)
        {
            Checks.IsNotNullOrEmpty(name, "Assembly name can not be empty.");
            
            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .AsParallel()
                            .Single(a => name.Equals(a.GetName().Name));

        }

    }
}