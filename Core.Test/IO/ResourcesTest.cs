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
            var text = Resources.GetString(typeof(ResourcesTest), "Resources-FullCharset.txt");
            Console.WriteLine(FullCharSet.Length);
            Console.WriteLine(text.Length);
            Console.WriteLine(text.First());
            Console.WriteLine(text.Last());
            Checks.IsEqual(text, FullCharSet);

        }

        [Test]
        public void TestLoadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            assemblies.ForEach(a =>
            {
                Console.WriteLine(a.FullName);
            });
        }

    }
}