using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Strings
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods
        /// <summary>
        /// Converts a string into a byte array.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">String value to convert.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] ToByteArray(this string value)
        {
            Validation.Parameter.AssertNotNullOrEmpty(value, "value");

            var data = new byte[value.Length * sizeof(char)];
            System.Buffer.BlockCopy(value.ToCharArray(), 0, data, 0, data.Length);
            return data;
        }
        #endregion
    }
}
