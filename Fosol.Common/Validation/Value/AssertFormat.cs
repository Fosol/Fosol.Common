using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fosol.Common.Validation.Value
{
    /// <summary>
    /// AssertFormat static class is a collection of string validation functions that will throw a ValueException if the value passed to them is invalid.
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
        /// <param name="message">A message to describe the exception.</param>
        public static void IsEmail(string value, string message = null)
        {
            if (!Fosol.Common.Validation.AssertFormat.IsEmail(value))
                throw new Exceptions.ValueException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Value_IsEmail, value));
        }

        /// <summary>
        /// Determines if the value is a number.
        /// If it is not a valid number it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid number.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNumber(string value, string message = null)
        {
            if (!Fosol.Common.Validation.AssertFormat.IsNumber(value))
                throw new Exceptions.ValueException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Value_IsNumber, value));
        }

        /// <summary>
        /// Determines if the value is a valid Uri.
        /// If it is not a valid Uri it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid Uri.</exception>
        /// <param name="value">The value to check.</param>
        
        /// <param name="message">A message to describe the exception.</param>
        public static void IsUri(string value, UriKind uriKind = UriKind.RelativeOrAbsolute, string message = null)
        {
            if (!Fosol.Common.Validation.AssertFormat.IsUri(value, uriKind))
                throw new Exceptions.ValueException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Value_IsUri, value));
        }

        /// <summary>
        /// Determines if the value is a postal code.
        /// If it is not a valid postal code it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid postal code.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsPostalCode(string value, string message = null)
        {
            if (!Fosol.Common.Validation.AssertFormat.IsPostalCode(value))
                throw new Exceptions.ValueException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Value_IsPostalCode, value));
        }

        /// <summary>
        /// Determines if the value is a FSA code.
        /// If it is not a valid FSA code it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid FSA code.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsFSA(string value, string message = null)
        {
            if (!Fosol.Common.Validation.AssertFormat.IsFSA(value))
                throw new Exceptions.ValueException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Value_IsFSA, value));
        }

        /// <summary>
        /// Determines if the value is a LDU code.
        /// If it is not a valid LDU code it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' is not a valid LDU code.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsLDU(string value, string message = null)
        {
            if (!Fosol.Common.Validation.AssertFormat.IsLDU(value))
                throw new Exceptions.ValueException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Value_IsLDU, value));
        }
        #endregion
    }
}
