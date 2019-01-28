using System;
using Core.Collection;
using Core.IO;
using NUnit.Framework;

namespace Core.Test.IO
{

    public class ResourcesTest
    {
        private const string FullCharSet = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=_+[]\\{};':\",.<>";

        [Test]
        public void TestText()
        {
            var text = Resources.GetString(typeof(ResourcesTest), "Resources-Fullcharset.txt");
            Checks.Equals(FullCharSet, text);
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