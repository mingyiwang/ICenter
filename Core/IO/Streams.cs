using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Collection;

namespace Core.IO
{

    public sealed class Streams
    {

        private const int BufferSize = 8; // 8K

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

        public static void Transfer(Stream input, Stream output)
        {
            Transfer(input, BufferSize, output);
        }

        public static void Transfer(Stream input, int bufferSize, Stream output)
        {
            Checks.GreaterThan(bufferSize, 0, "Buffer size must be greater than zero.");

            Checks.NotNull(input, "InputStream can not be null");
            Checks.IsTrue<InvalidOperationException>(input.CanRead, "Input stream is not readable.");

            Checks.NotNull(output, "OutputStream can not be null");
            Checks.IsTrue<InvalidOperationException>(input.CanWrite, "Output stream is not writable.");

            using (input)
            {
                Read(input, bufferSize, data =>
                {
                    if (!Arrays.IsEmpty(data))
                    {
                         Write(data, output);
                    }

                });
            }
        }

        /// <summary>
        /// Write a sequence of bytes to <param name="stream"/>, this method
        /// assumes stream is writable.
        /// </summary>
        /// <param name="bytes">a sequence of bytes</param>
        /// <param name="stream">The current stream</param>
        private static void Write(byte[] bytes, Stream stream)
        {
            Checks.NotNullOrEmpty(bytes, "Data can not be empty");

            Checks.NotNull(stream, "Output Stream can not be null.");
            Checks.IsTrue<InvalidOperationException>(stream.CanWrite, "Stream is not writable.");

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
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
                    var temp = Arrays.Make<byte>(numOfBytesRead);
                    Buffer.BlockCopy(buffer, 0, temp, 0, numOfBytesRead);
                    buffer = temp;

                    // Todo: gradually increase buffer size 
                    bufferSize = (int) (1.5 * bufferSize);
                }

                handle(buffer);
            }

        }

    }
}