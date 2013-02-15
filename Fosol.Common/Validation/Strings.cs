﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Validation
{
    /// <summary>
    /// A collection of string validation functions.
    /// </summary>
    public static class Strings
    {
        #region Methods
        /// <summary>
        /// Determines if the value is an email address.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <returns>True if the value is an email address.</returns>
        public static bool IsEmail(string value)
        {
            var reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            return reg.IsMatch(value);
        }

        /// <summary>
        /// Determines if the value is a number.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <returns>True if the value is a number.</returns>
        public static bool IsNumber(string value)
        {
            double result;
            return double.TryParse(value, out result);
        }

        /// <summary>
        /// Determines if the value is a valid Uri.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <param name="uriKind">Uri Kind option.</param>
        /// <returns>True if the value is a Uri.</returns>
        public static bool IsUri(string value, UriKind uriKind = UriKind.RelativeOrAbsolute)
        {
            return Uri.IsWellFormedUriString(value, uriKind);
        }

        /// <summary>
        /// Determines if the value is a postal code.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <returns>True if the value is a postal code.</returns>
        public static bool IsPostalCode(string value)
        {
            var reg = new Regex(@"^[ABCEGHJKLMNPRSTVXY][0-9][A-Z][\s][0-9][A-Z][0-9]$", RegexOptions.IgnoreCase);
            return reg.IsMatch(value);
        }

        /// <summary>
        /// Determines if the value is a FSA code.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <returns>True if the value is a FSA code.</returns>
        public static bool IsFSA(string value)
        {
            var reg = new Regex(@"^[ABCEGHJKLMNPRSTVWXY][0-9][A-Z]$", RegexOptions.IgnoreCase);
            return reg.IsMatch(value);
        }

        /// <summary>
        /// Determines if the value is a LDU code.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <returns>True if the value is a LDU code.</returns>
        public static bool IsLDU(string value)
        {
            var reg = new Regex(@"^[0-9][A-Z][0-9]$", RegexOptions.IgnoreCase);
            return reg.IsMatch(value);
        }
        #endregion
    }
}
