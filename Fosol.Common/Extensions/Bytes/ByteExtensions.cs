using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Bytes
{
    /// <summary>
    /// Extentions methods for bytes.
    /// </summary>
    public static class ByteExtensions
    {
        #region Methods
        /// <summary>
        /// Converts a byte array into a string value.
        /// By default it uses the Encoding.Default to convert the byte array into a string.
        /// </summary>
        /// <param name="value">Byte array to convert into a string.</param>
        /// <param name="encoding">Encoding to use when creating the string.</param>
        /// <returns>String value.</returns>
        public static string ToStringValue(this byte[] value, Encoding encoding = null)
        {
#if WINDOWS_APP || WINDOWS_PHONE_APP
            if (encoding == null)
                encoding = Encoding.UTF8;
            return encoding.GetString(value, 0, value.Length);
#else
            if (encoding == null)
                encoding = Encoding.Default;
            return encoding.GetString(value);
#endif
        }

        /// <summary>
        /// Converts a byte array into a string value.
        /// Use this method when the source byte array was encoded with a different encoding, or you want to convert to a different encoding.
        /// </summary>
        /// <param name="value">Byte array to convert into a string.</param>
        /// <param name="sourceEncoding">Encoding the byte array was created with.</param>
        /// <param name="destEncoding">Encoding to use when creating the string.</param>
        /// <returns>String value.</returns>
        public static string ToStringValue(this byte[] value, Encoding sourceEncoding, Encoding destEncoding)
        {
            Validation.Assert.IsNotNull(sourceEncoding, "sourceEncoding");
            Validation.Assert.IsNotNull(destEncoding, "destEncoding");
            var buffer = Encoding.Convert(sourceEncoding, destEncoding, value);
#if WINDOWS_APP || WINDOWS_PHONE_APP
            return destEncoding.GetString(buffer, 0, value.Length);
#else
            return destEncoding.GetString(buffer);
#endif
        }

        /// <summary>
        /// Inserts the data into the destination array starting at the destIndex position.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "destIndex" must be a valid index position within the destination.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "length" must be greater than or equal to '0' and less than or equal to the length of the value array.</exception>
        /// <param name="destination">Destination byte array object.</param>
        /// <param name="data">Byte array to be copied into destination.</param>
        /// <param name="destIndex">Index position to start copying into destination array.</param>
        /// <param name="length">Length of data to append into the destination array.</param>
        /// <returns>Position within the destination array after the data has been copied.</returns>
        public static int Insert(this byte[] destination, byte[] data, int destIndex = 0, int length = 0)
        {
            return Insert(destination, data, destIndex, 0, length);
        }

        /// <summary>
        /// Inserts the data into the destination array starting at the destIndex position.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "destIndex" must be a valid index position within the destination.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "length" must be greater than or equal to '0' and less than or equal to the length of the value array.</exception>
        /// <param name="destination">Destination byte array object.</param>
        /// <param name="data">Byte array to be copied into destination.</param>
        /// <param name="destIndex">Index position to start copying into destination array.</param>
        /// <param name="dataIndex">Index position to start copying from the data array.</param>
        /// <param name="length">Length of data to append into the destination array.</param>
        /// <returns>Position within the destination array after the data has been copied.</returns>
        public static int Insert(this byte[] destination, byte[] data, int destIndex, int dataIndex, int length)
        {
            Validation.Assert.IsNotNull(data, "data");

            // Default to the value.Length.
            if (length <= 0)
                length = data.Length - dataIndex;

            Validation.Assert.Range(destIndex, 0, destination.Length - length, "destIndex");
            Validation.Assert.Range(length, 0, data.Length, "length");

            if (destination.Length < length + destIndex)
                throw new ArgumentOutOfRangeException("data", String.Format(Resources.Multilingual.Exception_Destination_Too_Small, "data"));

            int i;
            for (i = 0; i < length; i++)
                destination[destIndex + i] = data[dataIndex + i];
            return i + destIndex;
        }

        /// <summary>
        /// Returns a hex value for the specified byte.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Byte value to convert to a hex value.</param>
        /// <returns>Hex value that represents the byte.</returns>
        public static string ToHex(this byte value)
        {
            return Extensions.Numbers.NumberExtensions.ToHex(value);
        }

        /// <summary>
        /// Returns a hex value for the specified byte array.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Byte array to convert to a hex value.</param>
        /// <returns>Hex value represents the byte array.</returns>
        public static string ToHex(this byte[] value)
        {
            Validation.Assert.IsNotNull(value, "value");

            return BitConverter.ToString(value).Replace("-", "");
        }

        /// <summary>
        /// Look for the first index position of the search value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "value" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "startIndex" must be a valid index position within the data.</exception>
        /// <param name="data">Byte array to search through.</param>
        /// <param name="value">Byte array value to search for.</param>
        /// <param name="startIndex">Index position to start searching at within the data.</param>
        /// <returns>Index position of value within data, or -1 if not found.</returns>
        public static int IndexOf(this byte[] data, byte[] value, int startIndex = 0)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.IsNotNull(value, "value");
            Validation.Assert.Range(startIndex, 0, data.Length, "startIndex");

            if (data.Length == 0 || value.Length == 0)
                return -1;

            for (int i = startIndex; i < data.Length; i++)
                if (IsMatch(data, value, i))
                    return i;

            return -1;
        }

        /// <summary>
        /// Confirm whether the value is found within the data at the index position.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "value" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "index" must be a valid index position within the data.</exception>
        /// <param name="data">Data to confirm against.</param>
        /// <param name="value">Value to confirm that it is a match.</param>
        /// <param name="startIndex">Index position within the data that the value should be found at.</param>
        /// <returns>True if the value is at the index position within the data.</returns>
        public static bool IsMatch(this byte[] data, byte[] value, int startIndex = 0)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.IsNotNull(value, "value");
            Validation.Assert.Range(startIndex, 0, data.Length, "startIndex");

            if (value.Length > (data.Length - startIndex))
                return false;

            for (int i = 0; i < value.Length; i++)
                if (data[startIndex + i] != value[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Confirm whether the value is found within the data at the index position.
        /// Also provides a reference to the index position if the value is a match.
        /// This provides a way to avoid retracing the same positions within the data.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "value" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "index" must be a valid index position within the data.</exception>
        /// <param name="data">Data to confirm against.</param>
        /// <param name="value">Value to confirm that it is a match.</param>
        /// <param name="index">Index position within the data that the value should be found at.</param>
        /// <returns>True if the value is at the index position within the data.</returns>
        public static bool IsMatch(this byte[] data, byte[] value, ref int index)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.IsNotNull(value, "value");
            Validation.Assert.Range(index, 0, data.Length, "index");

            if (value.Length > (data.Length - index))
                return false;

            for (int i = 0; i < value.Length; i++)
                if (data[index + i] != value[i])
                    return false;

            // Update the index value to the end of the found search value.
            index += value.Length;
            return true;
        }

        /// <summary>
        /// Search for the value within the data and return all the index positions it was found.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "value" cannot be null.</exception>
        /// <param name="data">Data to search through for value.</param>
        /// <param name="value">Pattern to look for.</param>
        /// <returns>An array of index positions that the value was found.</returns>
        public static int[] IndexOfAll(this byte[] data, byte[] value)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.IsNotNull(value, "value");

            if (data.Length == 0 || value.Length == 0)
                return new int[0];

            var result = new List<int>();

            for (int i = 0; i < data.Length; i++)
            {
                if (!IsMatch(data, value, ref i))
                    continue;
                result.Add(i);
            }

            return result.ToArray();
        }
        #endregion
    }
}
