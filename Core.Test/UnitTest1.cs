using System;
using System.IO;
using System.Text;
using Core.IO;
using Core.Json;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine(Streams.GetString(new MemoryStream(Encoding.UTF8.GetBytes("test"))));
        }

        void change1(string input)
        {
            input = "1234";
        }

        void change(ref string input)
        {
            input = "1234";
        }
    }

    
}