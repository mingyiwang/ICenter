using System;
using System.Text;
using Common;
using NUnit.Framework;

namespace EL.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private const string Charsets = "123456789/*-+0<>/";
        private const int Upper = 27;

        [Test]
        public void TestMethod1()
        {
            
            for(int j=0; j<30; j++){
                var builder = new StringBuilder();
                var random = new Random();
                for (int i = 0; i < Upper; i++)
                {
                    builder.Append(Charsets.Substring(random.Next(0, Charsets.Length), 1));
                }
                builder.AppendLine();
                for (int i = 0; i < Upper; i++)
                {
                    builder.Append(Charsets.Substring(random.Next(0, Charsets.Length), 1));
                }
                builder.AppendLine();
                for (int i = 0; i < Upper; i++)
                {
                    builder.Append(Charsets.Substring(random.Next(0, Charsets.Length), 1));
                }
                builder.AppendLine();
                for (int i = 0; i < Upper; i++)
                {
                    builder.Append(Charsets.Substring(random.Next(0, Charsets.Length), 1));
                }
                builder.AppendLine();
                builder.AppendLine();
                Console.WriteLine(builder.ToString());
            }
           
          
        }

        
    }
}
