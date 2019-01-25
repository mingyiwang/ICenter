using System;
using System.IO;
using System.Text;

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
            return Encoding.UTF8.GetString(GetBytes(stream, 1000));
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
        /// .Net Streams are mainly working with bytes that is input and output of stream are all bytes
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static byte[] GetBytes(Stream s, int bufferSize)
        {
            Checks.NotEquals(0, bufferSize, "Buffer size must not be empty.");
            Checks.NotNull(s, "Stream can not be null.");

            if (!s.CanRead)
            {
                throw new InvalidOperationException("Stream is not readable.");
            }

            if (s.CanSeek)
            {
                s.Position = 0; // Make sure we are in the first position of stream if stream is seekable
            }
            
            using (var reader = new BinaryReader(s))
            {
                return reader.ReadBytes(bufferSize);
            }
            
        }

        public static void PutBytes(byte[] bytes, Stream output, Encoding encoding)
        {
            Checks.NotNullOrEmpty(bytes , "Collection can not be empty");
            Checks.NotNull(output, "Output Stream can not be null.");
            
            if (!output.CanWrite)
            {
                 throw new InvalidOperationException("Output Stream is not writable");
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
                throw new InvalidOperationException("Input Stream is not readable");
            }

            using(input)
            {
                PutBytes(GetBytes(input, BufferSize), output, encoding);
                return output;
            }
        }

    }
}