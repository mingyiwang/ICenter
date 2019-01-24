using System;
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
            var test = "123";
            change1(test);
            Console.WriteLine(test);
            change(ref test);
            Console.WriteLine(test);
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