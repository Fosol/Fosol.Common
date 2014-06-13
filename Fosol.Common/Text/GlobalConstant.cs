using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Text
{
    /// <summary>
    /// GlobalConstant is a static class with utility methods and properties.
    /// </summary>
    public static class GlobalConstant
    {
        #region 
        /// <summary>
        /// Lowercase characters.
        /// </summary>
        public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Uppercase characters.
        /// </summary>
        public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Digit characters.
        /// </summary>
        public const string Digit = "0123456789";

        /// <summary>
        /// All lowercase and uppercase characters.
        /// </summary>
        public const string Letter = Lowercase + Uppercase;

        /// <summary>
        /// All lowercase, uppercase and digit characters.
        /// </summary>
        public const string Alphanumeric = Letter + Digit;

        /// <summary>
        /// Digit characters, negative symbol and decimal point.
        /// </summary>
        public const string Decimals = Digit + "-.";

        /// <summary>
        /// Digit characters and negative symbol.
        /// </summary>
        public const string Integers = "-" + Digit;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
