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
        /// Under development
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static byte[] GetBytes(Stream stream, int bufferSize)
        {
            Checks.NotEquals(0, bufferSize, "Buffer size must not be empty.");
            Checks.NotNull(stream, "Stream can not be null.");

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("Stream is not readable or seekable.");
            }

            if (stream.CanSeek)
            {
                stream.Position = 0; // Make sure we are in the first position of stream if stream is seekable
            }

            using (stream)
            {
               var totalNumOfBytesRead = 0;
               var result = Arrays.Empty<byte>();
               while (true)
               {
                   var buffer = Arrays.Make<byte>(bufferSize);
                   var numOfBytesRead = stream.Read(buffer, totalNumOfBytesRead, bufferSize);
                   if (numOfBytesRead == 0)
                   {
                       return result;
                   }

                   if (numOfBytesRead != buffer.Length)
                   {
                       var tempBuffer = new byte[numOfBytesRead];
                       Buffer.BlockCopy(buffer, 0, tempBuffer, 0, numOfBytesRead);
                       buffer = tempBuffer;
                   }

                   var nextByte = stream.ReadByte();
                   if (nextByte == -1) // reached the end of stream
                   {
                       var tempResult = new byte[result.Length + buffer.Length];
                       Buffer.BlockCopy(buffer, 0, tempResult, totalNumOfBytesRead, buffer.Length);
                       result = tempResult;
                       return result;
                   }
                   else
                   {
                       var tempResult = new byte[result.Length + buffer.Length + 1];
                       Buffer.BlockCopy(buffer, 0, tempResult, totalNumOfBytesRead, buffer.Length);
                       totalNumOfBytesRead += numOfBytesRead;
                       totalNumOfBytesRead += 1;
                       tempResult[totalNumOfBytesRead] = (byte)nextByte;
                       result = tempResult;
                    }
                }
            }
        }

        public static void PutBytes(byte[] bytes, Stream output, Encoding encoding)
        {
            Checks.NotNullOrEmpty(bytes , "Collection can not be empty");
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