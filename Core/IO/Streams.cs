using System;
using System.IO;
using System.Text;
using Core.Collection;

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
            return GetString(stream, Encoding.UTF8);
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
        /// Read a sequence of bytes from current stream, if the current stream is not readable or
        /// seekable an <exception cref="T:System.InvalidOperationException"/> will be throw.
        /// </summary>
        /// <param name="stream">The current stream to read</param>
        /// <param name="bufferSize">The specified buffer size</param>
        /// <returns>Total number of bytes read</returns>
        /// <exception cref="T:System.ArgumentException">Buffer size is empty.</exception>
        /// <exception cref="T:System.ArgumentException">The stream is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The stream is not readable or seekable.</exception>
        public static byte[] GetBytes(Stream stream, int bufferSize)
        {
            Checks.NotEquals(0, bufferSize, "Buffer size can not be empty.");
            Checks.NotNull(stream, "Stream can not be null.");

            if (!stream.CanRead || !stream.CanSeek)
            {
                 throw new InvalidOperationException("Stream is not readable or seekable.");
            }

            if (stream.CanSeek)
            {
                // Make sure we are in the first position of stream if stream is seekable.
                stream.Position = 0; 
            }

            var totalLengthOfStream = stream.Length;
            var totalNumOfBytesRead = 0;
            var result = Arrays.Empty<byte>(0);
            using (stream)
            {
                do
                {
                    var buffer = Arrays.Make<byte>(bufferSize);
                    var numOfBytesRead = stream.Read(buffer, totalNumOfBytesRead, bufferSize);
                    if(numOfBytesRead == 0)
                    {
                        // already reached the end of stream or this stream is not prepared.
                        break;
                    }

                    if(numOfBytesRead != buffer.Length)
                    {
                        var tempBuffer = new byte[numOfBytesRead];
                        Buffer.BlockCopy(buffer, 0, tempBuffer, 0, numOfBytesRead);
                        buffer = tempBuffer;
                    }

                    var tempResult = new byte[result.Length + buffer.Length];
                    Buffer.BlockCopy(buffer, 0, tempResult, totalNumOfBytesRead, buffer.Length);
                    totalNumOfBytesRead += numOfBytesRead;
                    result = tempResult;
                }
                while (
                    totalNumOfBytesRead < totalLengthOfStream
                );

                return result;
            }
        }

        public static void PutBytes(byte[] bytes, Stream output, Encoding encoding)
        {
            Checks.NotNullOrEmpty(bytes , "Collection can not be empty");
            Checks.NotNull(output, "Output Stream can not be null.");

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

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
            Checks.NotNull(input,   "InputStream can not be null");
            Checks.NotNull(output,  "OutputStream can not be null");

            if(!input.CanRead || !input.CanSeek)
            {
                throw new InvalidOperationException("Input Stream is not readable");
            }

            using(input)
            {
                PutBytes(GetBytes(input, BUFFER_SIZE), output, encoding);
                return output;
            }
        }

    }
}