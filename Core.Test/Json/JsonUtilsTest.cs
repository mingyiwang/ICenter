using System;
using Core.Collection;
using Core.IO;
using Core.Json;
using Core.Primitive;
using NUnit.Framework;

namespace Core.Test.Json
{
    public class JsonUtilsTest
    {
        private const string TestCase3 = "TestCase";
        private const int    TestCase1 = 1;
        private const bool   TestCase2 = true;

        [Test]
        public void TestInt()
        {
            var result = JsonUtils.Serialize(TestCase1);
            Checks.Equals("1",result);
            Checks.Equals(TestCase1, JsonUtils.Deserialize<int>(result));
        }

        [Test]
        public void TestBool()
        {
            var result = JsonUtils.Serialize(TestCase2);
            Checks.Equals(TestCase2.ToString(), result);
            Checks.Equals(TestCase2, JsonUtils.Deserialize<bool>(result));
        }

        [Test]
        public void TestString()
        {
            var result = JsonUtils.Serialize(TestCase3);
            Checks.Equals(TestCase3, result);
            Checks.Equals(TestCase3, JsonUtils.Deserialize<string>(result));
        }

        [Test]
        public void TestDecimal()
        {
            var result = Resources.GetString("TextFile1", typeof(JsonUtils));
            Console.WriteLine(result);
        }

        [Test]
        public void TestDateTime()
        {
            var dateTime = DateTime.Now;
            var result   = JsonUtils.Serialize(dateTime);
            Checks.Equals(dateTime, JsonUtils.Deserialize<DateTime>(result));
        }

        [Test]
        public void TestArray()
        {
            var array  = Arrays.Make<int>(10);
            var result = JsonUtils.Serialize(array);
            var deserialize = JsonUtils.Deserialize<int[]>(result);
            Checks.Equals(array, deserialize);
        }

        [Test]
        public void TestCollection()
        {

        }

        [Test]
        public void TestObject()
        {

        }

        
    }

}
