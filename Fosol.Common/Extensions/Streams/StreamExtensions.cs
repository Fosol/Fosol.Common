using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Streams
{
    /// <summary>
    /// Utility extensions to help convert from and to streams.
    /// </summary>
    public static class StreamExtensions
    {
        #region Variables
        /// <summary>
        /// It is more efficient to use a buffer that is the same size as the internal buffer of the stream.
        /// Where the internal buffer is set to your desired block size, and to always read less than the block size. 
        /// If the size of the internal buffer was unspecified when the stream was constructed, its default size is 4 kilobytes (4096 bytes)."
        /// </summary>
        public const int DefaultBufferSize = 4096;

        /// <summary>
        /// Event is fired every time the buffer is written to the stream.
        /// </summary>
        /// <param name="readByteTotal">Number of bytes written to stream.</param>
        /// <param name="totalLength">Number of bytes that will be written to the stream.</param>
        /// <param name="data">Parameters passed through to event from caller.</param>
        public delegate void StreamWriteProgressCallback(long readByteTotal, long totalLength, params Object[] data);
        
        /// <summary>
        /// Event is fired every time the stream is read from.
        /// </summary>
        /// <param name="readByteTotal">Number of bytes read from the stream.</param>
        /// <param name="totalLength">Number of bytes that will be read from the stream.</param>
        /// <param name="data">Parameters passed through to event from caller.</param>
        public delegate void StreamReadProgressCallback(long readByteTotal, long totalLength, params Object[] data);
        
        #endregion

        #region ToStream Methods
        /// <summary>
        /// Writes the string to a stream.
        /// Returns the stream filled with the string data.
        /// Remember that the stream index position will be at the end of the string.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="value">String data to write to stream.</param>
        /// <returns>MemoryStream object containing string value.</returns>
        public static Stream ToMemoryStream(this string value)
        {
            Validation.Parameter.AssertNotNullOrEmpty(value, "value");

            var stream = new MemoryStream();
            ToStream(value, stream);
            return stream;
        }

        /// <summary>
        /// Writes the string to the stream.
        /// Remember that the stream index position will be at the end of the string.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="value">String data to write to stream.</param>
        /// <param name="stream">Stream object to receive string value.</param>
        public static void ToStream(this string value, System.IO.Stream stream)
        {
            Validation.Parameter.AssertNotNullOrEmpty(value, "value");
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertNotValue(stream.CanWrite, false, "stream.CanWrite");

            using (var writer = new StreamWriter(stream))
            {
                writer.Write(value);
                writer.Flush();
            }
        }

        /// <summary>
        /// Writes the data into the specified stream with the specified encoding.
        /// </summary>
        /// <param name="data">String data to write to stream.</param>
        /// <param name="stream">Stream object to receive the string data.</param>
        /// <param name="encoding">Encoding to use when streaming the data.</param>
        /// <param name="bufferSize">Byte size of the buffer being copied at one time.  By default global DefaultBufferSize.</param>
        /// <param name="statusCallback">Event fires every time a buffer has been written to the destination stream.</param>
        /// <param name="args">Data to pass to the statusCallback event.</param>
        public static void ToStream(this string value, System.IO.Stream stream, Encoding encoding, int bufferSize = DefaultBufferSize, StreamWriteProgressCallback statusCallback = null, params Object[] args)
        {
            Validation.Parameter.AssertNotNull(value, "value");
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertRange(bufferSize, -1, value.Length, "bufferSize");
            Validation.Parameter.AssertNotValue(bufferSize, 0, "bufferSize");
            Validation.Parameter.AssertNotValue(stream.CanWrite, false, "stream.CanWrite");

            if (bufferSize > value.Length)
                bufferSize = value.Length;

            var bytes = encoding.GetBytes(value);
            ToStream(bytes, stream, bufferSize);
        }

        /// <summary>
        /// Writes the data into the specified stream.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "stream" cannot be readonly.</exception>
        /// <exception cref="System.ArgumentException">Parameter "bufferSize" cannot be 0.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "stream" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "bufferSize" cannot be less than 1 or greater than the size of the data.</exception>
        /// <param name="value">Array of byte to write into stream.</param>
        /// <param name="stream">Stream object that will receive the data.</param>
        /// <param name="bufferSize">Size of buffer to write into the stream at one time.  Default size is the global DefaultBufferSize.</param>
        /// <param name="statusCallback">Event fires every time a buffer has been written to the destination stream.</param>
        /// <param name="args">Data to pass to the statusCallback event.</param>
        public static void ToStream(this byte[] value, System.IO.Stream stream, int bufferSize = DefaultBufferSize, StreamWriteProgressCallback statusCallback = null, params Object[] args)
        {
            Validation.Parameter.AssertNotNull(value, "value");
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertMinRange(bufferSize, 1, "bufferSize");
            Validation.Parameter.AssertNotValue(stream.CanWrite, false, "stream.CanWrite");

            if (bufferSize > value.Length)
                bufferSize = value.Length;

            var buffer = new byte[bufferSize];
            int index = 0;
            int total = 0;
            while (true)
            {
                // Calculate what remains to be streamed into the buffer.
                if (index + bufferSize > value.Length)
                    bufferSize = value.Length - index;

                stream.Write(buffer, index, bufferSize);
                total += index;

                // Fire the status callback event.
                if (statusCallback != null)
                {
                    if (stream.CanSeek)
                        statusCallback(total, stream.Length, args);
                    else
                        statusCallback(total, -1, args);
                }

                index += bufferSize;

                // The data has been fully written into the stream.
                if (index == value.Length)
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
        /// <param name="source">Source stream to read from.</param>
        /// <param name="destination">Destination stream to write to.</param>
        /// <param name="bufferSize">Byte size of the buffer being copied at one time.  By default global DefaultBufferSize.</param>
        /// <param name="statusCallback">Event fires every time a buffer has been written to the destination stream.</param>
        /// <param name="args">Data to pass to the statusCallback event.</param>
        public static void CopyTo(this Stream source, Stream destination, int bufferSize = DefaultBufferSize, StreamWriteProgressCallback statusCallback = null, params Object[] args)
        {
            Validation.Parameter.AssertIsValue(source.CanWrite, true, "source.CanRead");
            Validation.Parameter.AssertNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite");
            Validation.Parameter.AssertMinRange(bufferSize, 1, "bufferSize");

            var buffer = new byte[bufferSize];
            int read = 0;
            int total = 0;
            do
            {
                // Read into buffer.
                read = source.Read(buffer, 0, bufferSize);
                total += read;

                if (read > 0)
                {
                    // Write buffer to destination.
                    destination.Write(buffer, 0, read);

                    // Fire the status callback event.
                    if (statusCallback != null)
                    {
                        if (source.CanSeek)
                            statusCallback(total, source.Length, args);
                        else
                            statusCallback(total, -1, args);
                    }
                }
            } while (read > 0);
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
        /// Writes the stream to a string with the specified encoding.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters "stream", and "encoding" cannot be null.</exception>
        /// <param name="stream">Stream object to read from.</param>
        /// <param name="encoding">Encoding to use when creating the string.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        /// <returns>String value.</returns>
        public static string WriteToString(this Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks = false)
        {
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertNotNull(encoding, "encoding");

            using (var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Writes the stream to a string with the specified encoding.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters "stream", and "encoding" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "bufferSize" must be greater than 0.</exception>
        /// <param name="stream">Stream object to read from.</param>
        /// <param name="encoding">Encoding to use when creating the string.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        /// <param name="bufferSize">Buffer size to read at one time from the stream.</param>
        /// <param name="statusCallback">Callback event fires each time the buffer is read from.</param>
        /// <param name="args">Parameters to pass to the statusCallback event.</param>
        /// <returns>String value.</returns>
        public static string WriteToString(this Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks = false, int bufferSize = DefaultBufferSize, StreamReadProgressCallback statusCallback = null, params Object[] args)
        {
            Validation.Parameter.AssertNotNull(stream, "stream");
            Validation.Parameter.AssertNotNull(encoding, "encoding");
            Validation.Parameter.AssertMinRange(bufferSize, 1, "bufferSize");

            using (var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize))
            {
                var result = new List<char>();
                var buffer = new char[bufferSize];
                var read = 0;

                while (true)
                {
                    read += reader.Read(buffer, read, bufferSize);

                    if (read == 0)
                        break;

                    // Fire the status callback event.
                    if (statusCallback != null)
                    {
                        if (stream.CanSeek)
                            statusCallback(read, stream.Length, args);
                        else
                            statusCallback(read, -1, args);
                    }

                    result.AddRange(buffer);
                }

                return new string(result.ToArray());
            }
        }

        /// <summary>
        /// Reads the stream into a byte array.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "stream" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "bufferSize" must be greater than 0.</exception>
        /// <param name="stream">Source stream.</param>
        /// <param name="bufferSize">Byte size of the buffer to read.</param>
        /// <param name="statusCallback">Callback event fires each time the buffer is read from.</param>
        /// <param name="args">Parameters to pass to the statusCallback event.</param>
        /// <returns>Array of byte.</returns>
        public static byte[] WriteToByteArray(this Stream stream, int bufferSize = DefaultBufferSize, StreamReadProgressCallback statusCallback = null, params Object[] args)
        {
            Validation.Parameter.AssertNotNull(stream, "stream");
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

                    // Fire the status callback event.
                    if (statusCallback != null)
                    {
                        if (stream.CanSeek)
                            statusCallback(read, stream.Length, args);
                        else
                            statusCallback(read, -1, args);
                    }
                }
            }
        }
        #endregion
    }
}
