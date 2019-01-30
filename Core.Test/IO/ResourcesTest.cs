using System;
using System.IO;
using System.Linq;
using Core.Collection;
using Core.IO;
using NUnit.Framework;

namespace Core.Test.IO
{

    public class ResourcesTest
    {
        private const string FullCharSet = "abcdefghijklmnopqrstuvwxyz";

        [Test]
        public void TestText()
        {
            var text = Resources.GetString("Core.Test.IO.Charset.txt");
            
        }

        [Test]
        public void TestLoadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            assemblies.ForEach(a =>
            {
                Console.WriteLine(a.GetName().Name);

            });
        }

    }
}