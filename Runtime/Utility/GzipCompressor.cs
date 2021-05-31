using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SavegameSystem.Utility
{
    public static class GzipCompressor
    {
        public static string Compress(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Compression Input cannot be NULL or EMPTY");
            }

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var compressedBytes = Compress(inputBytes);
            var compressedBase64 = Convert.ToBase64String(compressedBytes);
            return compressedBase64;
        }

        private static byte[] Compress(byte[] buffer)
        {
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }
            return AppendLength(memoryStream, buffer.Length);
        }

        //We append the length of the stream to get to know how big the buffer is when decompressing
        private static byte[] AppendLength(Stream stream, int length)
        {
            stream.Position = 0;
            var compressedData = new byte[stream.Length];
            stream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(length), 0, gZipBuffer, 0, 4);
            return gZipBuffer;
        }

        public static string Decompress(string compressedInput)
        {
            if (string.IsNullOrWhiteSpace(compressedInput))
            {
                throw new ArgumentException("Decompression Input cannot be NULL or EMPTY");
            }

            var gZipBuffer = Convert.FromBase64String(compressedInput);
            var decompressedBytes = Decompress(gZipBuffer);
            var encodedStr = Encoding.UTF8.GetString(decompressedBytes);

            return encodedStr;
        }

        private static byte[] Decompress(byte[] gZipBuffer)
        {
            using (var memoryStream = new MemoryStream())
            {
                //we appended the length of the buffer in itself to know how big it actually is
                var dataSize = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataSize];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return buffer;
            }
        }
    }
}
