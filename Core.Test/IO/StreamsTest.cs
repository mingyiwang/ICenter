﻿using System;
using System.IO;
using System.Text;
using Core.IO;
using NUnit.Framework;

namespace Core.Test.IO
{

    public class StreamsTest
    {
        private const string Text = "Text";
        private const string EmptyText   = "";
        private const string FullCharSet = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=_+[]\\{};':\",.<>";

        [Test]
        public void TestReadNormalText()
        {
            var stream = Streams.Of(Encoding.UTF8.GetBytes(Text));
            using (stream)
            {
                Checks.Equals<string>(Text, Streams.GetString(stream));
            }
            
        }

        [Test]
        public void TestReadEmptyText()
        {
            var stream = Streams.Of(Encoding.UTF8.GetBytes(EmptyText));
            using (stream)
            {
                Checks.Equals<string>(EmptyText,
                    Streams.GetString(stream)
                );
            }
            
        }
        
        [Test]
        public void TestReadFullCharset()
        {
            var stream = Streams.Of(Encoding.UTF8.GetBytes(FullCharSet));
            using (stream)
            {
                Checks.Equals<string>(FullCharSet,
                    Streams.GetString(stream)
                );
            }
            
        }

       
        [Test]
        public void TestReadEmptyTextWithBufferSize()
        {
            var s = Streams.Of(EmptyText);
            using (s)
            {
                for (int i = 1; i <= 1024; i++)
                {
                    Checks.Equals<string>(EmptyText,
                        Encoding.UTF8.GetString(Streams.GetBytes(s, i))
                    );
                }
            }
            
        }

        [Test]
        public void TestReadFullCharsetWithBufferSize()
        {
            var s = Streams.Of(FullCharSet);
            using (s)
            {
                for (int i = 1; i <= 1024; i++)
                {
                    Checks.Equals<string>(FullCharSet,
                        Encoding.UTF8.GetString(Streams.GetBytes(s, i))
                    );
                }
            }
            
        }

    }
}
