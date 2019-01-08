using Common.Collection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Common.Primitive;

namespace Tests
{
    public class CollectionsTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var lists = new List<string>();
            lists.Add("1");
            lists.Add("2");
            lists.Add("3");
            lists.Add("4");
            lists.Add("5");

            var resutls  = Collections.JoinAsString<string>(lists);
            Console.WriteLine(resutls);

            var emurator = lists.GetEnumerator();
            emurator.MoveNext();
            Console.WriteLine(emurator.Current);

            Console.WriteLine(DoubleConverter.ToExactString(555));

        }
    }
}