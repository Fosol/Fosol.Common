using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fosol.Common.Validation.Argument
{
    /// <summary>
    /// A collection of string validation functions.
    /// </summary>
    public static class AssertFormat
    {
        #region Methods
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

            if (!Fosol.Common.Validation.AssertFormat.IsEmail(value))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsEmail, paramName));
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

            if (!Fosol.Common.Validation.AssertFormat.IsNumber(value))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsNumber, paramName));
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

            if (!Fosol.Common.Validation.AssertFormat.IsUri(value, uriKind))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsUri, paramName));
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

            if (!Fosol.Common.Validation.AssertFormat.IsPostalCode(value))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsPostalCode, paramName));
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

            if (!Fosol.Common.Validation.AssertFormat.IsFSA(value))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsFSA, paramName));
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

            if (!Fosol.Common.Validation.AssertFormat.IsLDU(value))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_Argument_IsLDU, paramName));
        }
        #endregion
    }
}
