using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.StreamExtensions
{
    /// <summary>
    /// Utility extensions to help convert from and to streams.
    /// </summary>
    public static class StreamExtensions
    {
        #region Variables
        /// <summary>
        /// Default buffer size to use when one isn't specified.
        /// </summary>
        public const int DefaultBufferSize = 4096;

        /// <summary>
        /// Event is fired every time the buffer is written to the stream.
        /// </summary>
        /// <param name="readByteTotal">Number of bytes written to stream.</param>
        /// <param name="totalLength">Number of bytes that will be written to the stream.</param>
        /// <param name="data">Parameters passed through to event from caller.</param>
        public delegate void StreamCopyProgressCallback(long readByteTotal, long totalLength, params Object[] data);
        #endregion

        #region ToStream Methods
        /// <summary>
        /// Writes the string to the stream.
        /// Remember that the stream index position will be at the end of the string.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="data">String data to write to stream.</param>
        /// <param name="stream">Stream object to receive string value.</param>
        public static void ToStream(this string data, System.IO.Stream stream)
        {
            Validation.Parameter.AssertNotNullOrEmpty(data, "data");
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertNotValue(stream.CanWrite, false, "stream.CanWrite");

            using (var writer = new StreamWriter(stream))
            {
                writer.Write(data);
                writer.Flush();
            }
        }

        /// <summary>
        /// Writes the string to a stream.
        /// Returns the stream filled with the string data.
        /// Remember that the stream index position will be at the end of the string.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="data">String data to write to stream.</param>
        /// <returns>MemoryStream object containing string value.</returns>
        public static Stream ToMemoryStream(this string data)
        {
            Validation.Parameter.AssertNotNullOrEmpty(data, "data");

            var stream = new MemoryStream();
            ToStream(data, stream);
            return stream;
        }

        /// <summary>
        /// Writes the data into the specified stream.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "stream" cannot be readonly.</exception>
        /// <exception cref="System.ArgumentException">Parameter "bufferSize" cannot be 0.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "stream" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "bufferSize" cannot be less than -1 or greater than the size of the data.</exception>
        /// <param name="data">Array of byte to write into stream.</param>
        /// <param name="stream">Stream object that will receive the data.</param>
        /// <param name="bufferSize">Size of buffer to write into the stream at one time.  By default -1 means write the whole data amount at one time.</param>
        public static void ToStream(this byte[] data, System.IO.Stream stream, int bufferSize = -1)
        {
            Validation.Parameter.AssertNotNull(data, "data");
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertMinMaxRange(bufferSize, -1, data.Length, "bufferSize");
            Validation.Parameter.AssertNotValue(bufferSize, 0, "bufferSize");
            Validation.Parameter.AssertNotValue(stream.CanWrite, false, "stream.CanWrite");

            // Default the bufferSize to the size of the data.
            if (bufferSize == -1)
                bufferSize = data.Length;

            var buffer = new byte[data.Length];
            int read = 0;
            while (true)
            {
                // Calculate what remains to be streamed into the buffer.
                if (read + bufferSize > data.Length)
                    bufferSize = data.Length - read;

                stream.Write(buffer, read, bufferSize);
                read += bufferSize;

                // The data has been fully written into the stream.
                if (read == data.Length)
                    break;
            }
        }

        /// <summary>
        /// Copy one stream into another.
        /// It will start to read from the current position in the source stream.
        /// It will start to write at the current position in the destination stream.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "stream.CanRead" cannot be false.</exception>
        /// <exception cref="System.ArgumentException">Parameter "destination.CanWrite" cannot be false.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "destination" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "bufferSize" cannot be less than 1.</exception>
        /// <param name="stream">Source stream to read from.</param>
        /// <param name="destination">Destination stream to write to.</param>
        /// <param name="bufferSize">Byte size of the buffer being copied at one time.  By default global DefaultBufferSize.</param>
        /// <param name="statusCallback">Event fires every time a buffer has been written to the destination stream.</param>
        /// <param name="data">Data to pass to the statusCallback event.</param>
        public static void CopyTo(this Stream stream, Stream destination, int bufferSize = DefaultBufferSize, StreamCopyProgressCallback statusCallback = null, params Object[] data)
        {
            Validation.Parameter.AssertIsValue(stream.CanWrite, true, "stream.CanRead");
            Validation.Parameter.AssertNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite");
            Validation.Parameter.AssertMinRange(bufferSize, 1, "bufferSize");

            var buffer = new byte[bufferSize];
            int read = 0;
            int total = 0;
            do
            {
                // Read into buffer.
                read = stream.Read(buffer, 0, bufferSize);
                total += read;

                if (read > 0)
                {
                    // Write buffer to destination.
                    destination.Write(buffer, 0, read);

                    // Fire the status callback event.
                    if (statusCallback != null)
                    {
                        if (stream.CanSeek)
                            statusCallback(total, stream.Length, data);
                        else
                            statusCallback(total, -1, data);
                    }
                }
            } while (read > 0);
        }

        /// <summary>
        /// Serializes an object with the SerializableAttribute and writes it to the stream.
        /// Uses the XmlSerializer object to serialize.
        /// </summary>
        /// <param name="data">Object to serialize and write to the stream.</param>
        /// <param name="stream">Stream to receive the serialized object.</param>
        public static void XmlToStream(this object data, Stream stream)
        {
            Serialization.XmlHelper.ToStream(data, stream);
        }

        /// <summary>
        /// Serializes an object with the DataContractAttribute and writes it to the stream.
        /// Uses the DataContractSerializer object to serialize.
        /// </summary>
        /// <param name="data">Object to serialize and write to the stream.</param>
        /// <param name="stream">Stream to receive the serialized object.</param>
        public static void DataContractToStream(this object data, Stream stream)
        {
            Serialization.DataContractHelper.ToStream(data, stream);
        }
        #endregion

        #region FromStream Methods
        /// <summary>
        /// Writes the stream into a string.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "stream.CanRead" cannot be false.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "stream" cannot be null.</exception>
        /// <param name="stream">Stream to convert into a string.</param>
        /// <returns>The contents of the string as a string value.</returns>
        public static string WriteToString(this Stream stream)
        {
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertIsValue<bool>(stream.CanRead, true, "stream.CanRead");

            using (var reader = new StreamReader(stream))
            {
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Reads the stream into a byte array.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "bufferSize" cannot be less than 1.</exception>
        /// <param name="stream">Source stream.</param>
        /// <param name="bufferSize">Byte size of the buffer to read.</param>
        /// <returns>Array of byte.</returns>
        public static byte[] WriteToByteArray(this Stream stream, int bufferSize = DefaultBufferSize)
        {
            Validation.Parameter.AssertMinRange(bufferSize, 1, "bufferSize");

            var buffer = new byte[bufferSize];
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    var read = stream.Read(buffer, 0, bufferSize);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }
        #endregion
    }
}
