using System.Text;
using Core.IO;
using NUnit.Framework;

namespace Core.Test.IO
{

    public class StreamsTest
    {
        private const string Text = "Text";
        private const string EmptyText = "";
        private const string FullCharSet = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=_+[]\\{};':\",.<>";

        [Test]
        public void TestText()
        {
            var stream = Streams.Of(Encoding.UTF8.GetBytes(Text));
            Checks.Equals<string>(Text,Streams.GetString(stream));
        }

        [Test]
        public void TestEmptyText()
        {
            var stream = Streams.Of(Encoding.UTF8.GetBytes(EmptyText));
            Checks.Equals<string>(EmptyText, Streams.GetString(stream));
        }

        [Test]
        public void TestFullCharSet()
        {
            var stream = Streams.Of(Encoding.UTF8.GetBytes(FullCharSet));
            Checks.Equals<string>(FullCharSet, Streams.GetString(stream));
        }

    }
}
