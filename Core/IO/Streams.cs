using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Collection;

namespace Core.IO
{

    public sealed class Streams
    {

        private const int BufferSize = 1; // 8K

        public static MemoryStream Of(string content)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }

        public static MemoryStream Of(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static BufferedStream Of(Stream stream, int bufferSize)
        {
            return new BufferedStream(stream, bufferSize);
        }

        public static FileStream Of(FileInfo fileInfo)
        {
            return new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public static string GetString(Stream stream)
        {
            return Encoding.UTF8.GetString(GetBytes(stream, BufferSize));
        }

        public static string GetString(Stream stream, Encoding encoding)
        {
            return encoding.GetString(GetBytes(stream));
        }

        public static byte[] GetBytes(Stream stream)
        {
            return GetBytes(stream, BufferSize);
        }

        public static byte[] GetBytesAndClose(Stream stream, int bufferSize)
        {
            using (stream)
            {
                return GetBytes(stream, bufferSize);
            }
        }

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
                stream.Position = 0;
            }

            var result = new List<byte>();
            Read(stream, bufferSize, data =>
            {
                if (!Arrays.IsEmpty(data))
                {
                     result.AddRange(data);
                }
            });
            return result.ToArray();
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

        public static void TransferAndClose(Stream input, Stream output)
        {
            using (output)
            {
                Transfer(input, output, Encoding.UTF8);
            }
        }

        public static void Transfer(Stream input, Stream output)
        {
            Transfer(input, output, Encoding.UTF8);
        }

        public static void Transfer(Stream input, Stream output, Encoding encoding)
        {
            Transfer(input, 0, output, encoding);
        }

        public static void Transfer(Stream input, int offset, Stream output, Encoding encoding)
        {
            Checks.NotNull(input,  "InputStream can not be null");
            Checks.NotNull(output, "OutputStream can not be null");

            if (!input.CanRead)
            {
                throw new InvalidOperationException("Input stream is not readable");
            }

            if (!input.CanWrite)
            {
                throw new InvalidOperationException("Output stream is not writable");
            }

            using (input)
            {
                Read(input, BufferSize, data =>
                {
                    if (Arrays.IsEmpty(data))
                    {
                        return;
                    }

                    PutBytes(data, output, encoding);
                });
            }
        }

        /// <summary>
        /// Read the whole stream with specified buffer. This method assumes <param name="stream"/>
        /// is readable and <param name="bufferSize"/> is greater than zero. This method can not be used
        /// directly.
        /// </summary>
        /// <param name="stream">The current stream to read.</param>
        /// <param name="bufferSize">Specified buffer size</param>
        /// <param name="handle">Handler handing a sequence of bytes read.</param>
        private static void Read(Stream stream, int bufferSize, Action<byte[]> handle)
        {
            
            while (true)
            {
                
                var buffer = Arrays.Make<byte>(bufferSize);
                var numOfBytesRead = stream.Read(buffer, 0, bufferSize);
                if (numOfBytesRead == 0)
                {
                    break;
                }

                if (numOfBytesRead != buffer.Length)
                {
                    var temp1 = Arrays.Make<byte>(numOfBytesRead);
                    Buffer.BlockCopy(buffer,0,temp1,0, numOfBytesRead);
                    buffer = temp1;
                }

                // We need to know that we are reached the end of stream.
                var nextByte = stream.ReadByte();
                if (nextByte == -1)
                {
                    handle(buffer);
                    break;
                }

                // We need to handle the Next Byte.
                var temp2 = new byte[buffer.Length+1];
                Buffer.BlockCopy(buffer, 0, temp2, 0, buffer.Length);
                temp2[buffer.Length] = (byte) nextByte;
                buffer = temp2;
                handle(buffer);
            }

        }

    }
}