using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Collection;

namespace Core.IO
{

    public sealed class Streams
    {

        private const int BUFFER_SIZE = 1024; // 8K

        public static MemoryStream Of(string content)
        {
            return Of(content, Encoding.UTF8);
        }

        public static MemoryStream Of(string content, Encoding encoding)
        {
            return Of(encoding.GetBytes(content));
        }

        public static MemoryStream Of(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static FileStream Of(FileInfo fileInfo)
        {
            return new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public static BufferedStream Of(Stream stream, int bufferSize)
        {
            return new BufferedStream(stream, bufferSize);
        }

        public static string GetString(Stream stream)
        {
            return Encoding.UTF8.GetString(GetBytes(stream, BUFFER_SIZE));
        }

        public static string GetString(Stream stream, Encoding encoding)
        {
            return encoding.GetString(GetBytes(stream));
        }

        public static void Transfer(Stream input, Stream output)
        {
            Transfer(input, BUFFER_SIZE, output, Encoding.UTF8);
        }

        public static void Transfer(Stream input, int bufferSize, Stream output)
        {
            Transfer(input, bufferSize, output, Encoding.UTF8);
        }

        public static void Transfer(Stream input, Stream output, Encoding encoding)
        {
            Transfer(input, BUFFER_SIZE, output, encoding);
        }

        public static void Transfer(Stream input, int bufferSize, Stream output, Encoding encoding)
        {
            Checks.NotNull(input,  "InputStream can not be null");
            Checks.NotNull(output, "OutputStream can not be null");
            Checks.GreaterThan<ArgumentException>(bufferSize,  0, "");
            Checks.IsTrue<InvalidOperationException>(input.CanRead,  "");
            Checks.IsTrue<InvalidOperationException>(input.CanWrite, "");

            using (input)
            {
                Read(input, bufferSize, data =>
                {
                    if (Arrays.IsEmpty(data))
                    {
                        return;
                    }

                    PutBytes(data, output, encoding);
                });
            }
        }

        public static byte[] GetBytes(Stream stream)
        {
            return GetBytes(stream, BUFFER_SIZE);
        }

        public static byte[] GetBytes(Stream stream, int bufferSize)
        {
            Checks.NotNull(stream, "Stream can not be null.");
            Checks.IsTrue<InvalidOperationException>(stream.CanRead, "Stream is not readable.");

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
            Checks.NotNullOrEmpty(bytes, "Collection can not be empty");
            Checks.NotNull(output, "Output Stream can not be null.");
            Checks.IsTrue<InvalidOperationException>(output.CanWrite, "Stream is not writable.");

            using (var writer = new BinaryWriter(output, encoding, true))
            {
                writer.Write(bytes);
                writer.Flush();
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

                // We need to handle the next byte.
                var temp2 = new byte[buffer.Length+1];
                Buffer.BlockCopy(buffer, 0, temp2, 0, buffer.Length);
                temp2[buffer.Length] = (byte) nextByte;
                buffer = temp2;
                handle(buffer);
            }

        }

    }
}