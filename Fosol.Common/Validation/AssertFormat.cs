using System;
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
    public static class AssertFormat
    {
        #region IsEmail
        /// <summary>
        /// Determines if the value is an email address.
        /// Maximum size of an email is 254 characters.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <returns>True if the value is an email address.</returns>
        public static bool IsEmail(string value)
        {
            if (value.Length > 254)
                return false;

            var reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            return reg.IsMatch(value);
        }

        /// <summary>
        /// Determines if the value is an email address.
        /// Maximum size of an email is 254 characters.
        /// If it is not a valid email it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid email.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsEmail(string value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!AssertFormat.IsEmail(value))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Strings.Exception_Validation_AssertFormat_IsEmail, paramName));
        }
        #endregion

        #region IsNumber
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
        /// Determines if the value is a number.
        /// If it is not a valid number it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid number.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNumber(string value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!AssertFormat.IsNumber(value))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Strings.Exception_Validation_AssertFormat_IsNumber, paramName));
        }
        #endregion

        #region IsUri
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
        /// Determines if the value is a valid Uri.
        /// If it is not a valid Uri it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid Uri.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsUri(string value, string paramName, UriKind uriKind = UriKind.RelativeOrAbsolute, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!AssertFormat.IsUri(value, uriKind))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Strings.Exception_Validation_AssertFormat_IsUri, paramName));
        }
        #endregion

        #region IsPostalCode
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
        /// Determines if the value is a postal code.
        /// If it is not a valid postal code it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid postal code.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsPostalCode(string value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!AssertFormat.IsPostalCode(value))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Strings.Exception_Validation_AssertFormat_IsPostalCode, paramName));
        }
        #endregion

        #region IsFSA
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
        /// Determines if the value is a FSA code.
        /// If it is not a valid FSA code it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid FSA code.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsFSA(string value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!AssertFormat.IsFSA(value))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Strings.Exception_Validation_AssertFormat_IsFSA, paramName));
        }
        #endregion

        #region IsLDU
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

        /// <summary>
        /// Determines if the value is a LDU code.
        /// If it is not a valid LDU code it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid LDU code.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsLDU(string value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!AssertFormat.IsLDU(value))
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Strings.Exception_Validation_AssertFormat_IsLDU, paramName));
        }
        #endregion
    }
}
