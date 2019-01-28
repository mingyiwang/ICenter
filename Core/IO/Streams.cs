using System;
using System.IO;
using System.Text;
using Core.Collection;
using Core.Primitive;

namespace Core.IO
{
    public sealed class Streams
    {

        private const int BufferSize = 1024 * 32; // 32K

        public static MemoryStream Memory => new MemoryStream();

        public static MemoryStream MemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static string GetString(Stream stream)
        {
            return Encoding.UTF8.GetString(GetBytes(stream, 1));
        }

        public static string GetString(Stream stream, Encoding encoding)
        {
            return encoding.GetString(GetBytes(stream));
        }

        public static byte[] GetBytes(Stream stream)
        {
            return GetBytes(stream, BufferSize);
        }

        /// <summary>
        /// .Net streams are mainly working with bytes that is input and output of stream are all bytes
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static byte[] GetBytes(Stream s, int bufferSize)
        {
            Checks.NotEquals(0, bufferSize, "Buffer size must not be zero.");
            Checks.NotNull(s, "Stream can not be null.");

            if (!s.CanRead)
            {
                throw new InvalidOperationException("Stream is not readable.");
            }

            if (s.CanSeek)
            {
                s.Position = 0; // Make sure we are in the first position of stream if stream is seekable
            }

            using (s)
            {
                var bufferResult = Arrays.Make<byte>(0);
                while(true)
                {
                    var test = s.ReadByte();
                    if(test == -1)
                    {
                        return bufferResult;
                    }

                    var b  = BitConverter.GetBytes(test);
                    var b1 = Arrays.Extend(bufferResult, b.Length);
                    var bl = bufferResult.Length;
                    bufferResult = b1;
                    Buffer.BlockCopy(b,0,bufferResult,bl,b.Length);
                } 
            }
        }

        public static byte[] GetBytes(Stream s, int offset, int bufferSize)
        {
            if (bufferSize == 0)
            {
                return Array.Empty<byte>();
            }
            var buffer = new byte[bufferSize];
            var length = offset;
            do
            {
                var num = s.Read(buffer, length, bufferSize);
                if (num != 0)
                {
                    length += num;
                    bufferSize -= num;
                }
                else
                    break;
            }
            while (bufferSize > 0);

            var newLength = length - offset;
            if (newLength == buffer.Length)
            {
                return buffer;
            }
            var numArray = new byte[newLength];
            Buffer.BlockCopy((Array)buffer, 0, (Array)numArray, 0, newLength);
            buffer = numArray;
            return buffer;
        }


        public static void PutBytes(byte[] bytes, Stream output, Encoding encoding)
        {
            Checks.NotEmpty(bytes , "Collection can not be empty.");
            Checks.NotNull(output,  "Output stream can not be null.");
            
            if (!output.CanWrite)
            {
                 throw new InvalidOperationException("Output stream is not writable");
            }

            using (var writer = new BinaryWriter(output, encoding, true))
            {
                writer.Write(bytes);
                writer.Flush();
            }
        }

        public static TS Transfer<TS>(Stream input, TS output) where TS : Stream
        {
            return Transfer(input, output, Encoding.UTF8);
        }

        public static TS Transfer<TS>(Stream input, TS output, Encoding encoding) where TS : Stream
        {
            Checks.NotNull(input, "InputStream can not be null");
            Checks.NotNull(output, "OutputStream can not be null");

            if(encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if(!input.CanRead)
            {
                throw new InvalidOperationException("Input stream is not readable");
            }

            using(input)
            {
                PutBytes(GetBytes(input, BufferSize), output, encoding);
                return output;
            }
        }

    }
}