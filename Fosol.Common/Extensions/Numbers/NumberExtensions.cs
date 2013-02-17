using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Numbers
{
    /// <summary>
    /// Extension methods for numbers.
    /// </summary>
    public static class NumberExtensions
    {
        #region Methods
        #region byte
        /// <summary>
        /// Converts a number into a hex value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Number to convert.</param>
        /// <returns>Hex value that represents the number.</returns>
        public static string ToHex(this byte value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return Convert.ToString(value, 16);
        }

        /// <summary>
        /// Converts a hex value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Hex value to convert.</param>
        /// <returns>The number from the hex.</returns>
        public static byte HexToByte(this string value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return byte.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion

        #region int
        /// <summary>
        /// Converts a number into a hex value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Number to convert.</param>
        /// <returns>Hex value that represents the number.</returns>
        public static string ToHex(this int value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return Convert.ToString(value, 16);
        }

        /// <summary>
        /// Converts a hex value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Hex value to convert.</param>
        /// <returns>The number from the hex.</returns>
        public static int HexToInt(this string value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return int.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion

        #region long
        /// <summary>
        /// Converts a number into a hex value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Number to convert.</param>
        /// <returns>Hex value that represents the number.</returns>
        public static string ToHex(this long value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return Convert.ToString(value, 16);
        }

        /// <summary>
        /// Converts a hex value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Hex value to convert.</param>
        /// <returns>The number from the hex.</returns>
        public static long HexToLong(this string value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return long.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion


        #region short
        /// <summary>
        /// Converts a number into a hex value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Number to convert.</param>
        /// <returns>Hex value that represents the number.</returns>
        public static string ToHex(this short value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return Convert.ToString(value, 16);
        }

        /// <summary>
        /// Converts a hex value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Hex value to convert.</param>
        /// <returns>The number from the hex.</returns>
        public static short HexToShort(this string value)
        {
            Validation.Parameter.AssertIsNotNull(value, "value");
            return short.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion
        #endregion
    }
}
