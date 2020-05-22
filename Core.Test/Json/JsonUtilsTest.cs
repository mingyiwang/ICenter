using System;
using Core.Collection;
using Core.IO;
using Core.Json;
using Newtonsoft.Json.Converters;
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
            Checks.IsEqual("1",result);
            Checks.IsEqual(TestCase1, JsonUtils.Deserialize<int>(result));
        }

        [Test]
        public void TestBool()
        {
            var result = JsonUtils.Serialize(TestCase2);
            Checks.IsEqual(TestCase2.ToString(), result);
            Checks.IsEqual(TestCase2, JsonUtils.Deserialize<bool>(result));
        }

        [Test]
        public void TestString()
        {
            var result = JsonUtils.Serialize(TestCase3);
            Checks.IsEqual(TestCase3, result);
            Checks.IsEqual(TestCase3, JsonUtils.Deserialize<string>(result));
        }

        [Test]
        public void TestDecimal()
        {
            
        }

        [Test]
        public void TestDateTime()
        {
            var dateTime = DateTime.Now;
            var result   = JsonUtils.Serialize(dateTime, new JavaScriptDateTimeConverter());
            Checks.IsEqual(dateTime, JsonUtils.Deserialize<DateTime>(result, new JavaScriptDateTimeConverter()));
            Console.WriteLine(result);
        }

        [Test]
        public void TestArray()
        {
           
        }

        [Test]
        public void TestCollection()
        {

        }

        [Test]
        public void TestObject()
        {

        }


        [Test]
        public void TestMap()
        {
            var map = new HashMap<TestKey, TestValue>();
            map.Add(new TestKey(), new TestValue());
            map.Add(new TestKey(), new TestValue());

            var json = JsonUtils.Serialize(map);
            Console.WriteLine(json);
        }

        class TestKey
        {
            private string KeyValue = "KeyValue";
        }

        class TestValue
        {
            private string ValueValue = "ValueValue";
        }
    }

}
