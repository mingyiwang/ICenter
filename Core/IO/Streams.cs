using System;
using System.IO;
using System.Text;

namespace Core.IO
{

    public sealed class Streams
    {

        private const int BUFFER_SIZE = 1024 * 32; // 32K

        public static MemoryStream Of(byte[] bytes)
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
            return GetBytes(stream, BUFFER_SIZE);
        }

        /// <summary>
        /// .Net Streams are mainly working with bytes that is input and output of stream are all bytes
        /// </summary>
        /// <param name="stream">The current stream to read</param>
        /// <param name="bufferSize">The specified buffer size</param>
        /// <returns>Total number of bytes read</returns>
        /// <exception cref="T:System.ArgumentException">Buffer size is empty.</exception>
        /// <exception cref="T:System.ArgumentException">The stream is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The stream is not readable or seekable.</exception>
        public static byte[] GetBytes(Stream stream, int bufferSize)
        {
            Checks.NotEquals(0, bufferSize, "Buffer size must not be empty.");
            Checks.NotNull(s, "Stream can not be null.");

            if (!stream.CanRead || !stream.CanSeek)
            {
                 throw new InvalidOperationException("Stream is not readable or seekable.");
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
            Checks.NotEmpty(bytes , "Collection can not be empty");
            Checks.NotNull(output, "Output Stream can not be null.");
            
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
            Checks.NotNull(input,   "InputStream can not be null");
            Checks.NotNull(output,  "OutputStream can not be null");

            if(!input.CanRead || !input.CanSeek)
            {
                throw new InvalidOperationException("Input stream is not readable");
            }

            using(input)
            {
                PutBytes(GetBytes(input, BUFFER_SIZE), output, encoding);
                return output;
            }
        }

    }
}