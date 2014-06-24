#if !WINDOWS_APP && !WINDOWS_PHONE_APP
using Fosol.Common.Extensions.Attributes;
using Fosol.Common.Extensions.Types;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Validation
{
    /// <summary>
    /// Parameter validation utilities.
    /// </summary>
    public static class Assert
    {
        #region Methods
        #region IsEqual
        /// <summary>
        /// Asserts that the function returns a value equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'function' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsEqual(Func<object> function, object validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!ReferenceEquals(function, validValue)
                || !ReferenceEquals(function(), validValue))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsEqual, paramName));
        }

        /// <summary>
        /// Asserts that the function returns a value equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'function' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual(Func<object> function, object validValue, string message, Exception innerException)
        {
            if (!ReferenceEquals(function, validValue)
                || !ReferenceEquals(function(), validValue))
                throw new ArgumentException(message, innerException);
        }

        /// <summary>
        /// Asserts that the function returns a value equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'function' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsEqual<T>(Func<T> function, T validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!ReferenceEquals(function, validValue)
                || !ReferenceEquals(function(), validValue))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsEqual, paramName));
        }

        /// <summary>
        /// Asserts that the function returns a value equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'function' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual<T>(Func<T> function, T validValue, string message, Exception innerException)
        {
            if (!ReferenceEquals(function, validValue)
                || !ReferenceEquals(function(), validValue))
                throw new ArgumentException(message, innerException);
        }

        /// <summary>
        /// Asserts that the value is equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsEqual(object value, object validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!ReferenceEquals(value, validValue))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsEqual, paramName));
        }

        /// <summary>
        /// Asserts that the value is equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' does not return a valid value.</exception>
        /// <param name="value">Value to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual(object value, object validValue, string message, Exception innerException)
        {
            if (!ReferenceEquals(value, validValue))
                throw new ArgumentException(message, innerException);
        }

        /// <summary>
        /// Asserts that the value is equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' does not return a valid value.</exception>
        /// <param name="value">Value to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsEqual<T>(T value, T validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!ReferenceEquals(value, validValue))
                throw new ArgumentException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsEqual, paramName));
        }

        /// <summary>
        /// Asserts that the value is equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'value' does not return a valid value.</exception>
        /// <param name="value">Value to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual<T>(T value, T validValue, string message, Exception innerException)
        {
            if (!ReferenceEquals(value, validValue))
                throw new ArgumentException(message, innerException);
        }
        #endregion

        #region IsNotNull
        /// <summary>
        /// Asserts that the result of the function is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "function" return value is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotNull(Func<object> function, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (function == null || function() == null)
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsNotNull, paramName));
        }

        /// <summary>
        /// Asserts that the result of the function is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "function" return value is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull(Func<object> function, string message, Exception innerException)
        {
            if (function == null || function() == null)
                throw new ArgumentNullException(message, innerException);
        }

        /// <summary>
        /// Asserts that the result of the function is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "function" return value is null.</exception>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotNull<T>(Func<T> function, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (function == null || function() == null)
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsNotNull, paramName));
        }

        /// <summary>
        /// Asserts that the result of the function is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "function" return value is null.</exception>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull<T>(Func<T> function, string message, Exception innerException)
        {
            if (function == null || function() == null)
                throw new ArgumentNullException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotNull(object value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value == null)
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsNotNull, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull(object value, string message, Exception innerException)
        {
            if (value == null)
                throw new ArgumentNullException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotNull<T>(T value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value == null)
                throw new ArgumentNullException(paramName, string.Format(message ?? Resources.Multilingual.Exception_Validation_IsNotNull, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull<T>(T value, string message, Exception innerException)
        {
            if (value == null)
                throw new ArgumentNullException(message, innerException);
        }
        #endregion

        #region IsNotNullOrEmpty
        /// <summary>
        /// Asserts that the parameter value is not null or empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotNullOrEmpty(string value, string paramName, string message = null)
        {
            IsNotNull(value, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (value == String.Empty)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not null or empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty(string value, string paramName, string message, Exception innerException)
        {
            IsNotNull(value, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (value == String.Empty)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type enumerable.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void IsNotNullOrEmpty(IEnumerable<object> collection, string paramName, string message = null)
        {
            IsNotNull(collection, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (collection.Count() == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type enumerable.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception</param>
        public static void IsNotNullOrEmpty(IEnumerable<object> collection, string paramName, string message, Exception innerException)
        {
            IsNotNull(collection, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (collection.Count() == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type enumerable.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void IsNotNullOrEmpty<T>(IEnumerable<T> collection, string paramName, string message = null)
        {
            IsNotNull(collection, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (collection.Count() == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type enumerable.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception</param>
        public static void IsNotNullOrEmpty<T>(IEnumerable<T> collection, string paramName, string message, Exception innerException)
        {
            IsNotNull(collection, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (collection.Count() == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void IsNotNullOrEmpty(object[] array, string paramName, string message = null)
        {
            IsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception</param>
        public static void IsNotNullOrEmpty(object[] array, string paramName, string message, Exception innerException)
        {
            IsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void IsNotNullOrEmpty(byte[] array, string paramName, string message = null)
        {
            IsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception</param>
        public static void IsNotNullOrEmpty(byte[] array, string paramName, string message, Exception innerException)
        {
            IsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void IsNotNullOrEmpty<T>(T[] array, string paramName, string message = null)
        {
            IsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception</param>
        public static void IsNotNullOrEmpty<T>(T[] array, string paramName, string message, Exception innerException)
        {
            IsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }
        #endregion

        #region IsNotNullOrWhiteSpace
        /// <summary>
        /// Asserts that the parameter value is not null or empty after being trimmed.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotNullOrWhiteSpace(string value, string paramName, string message = null)
        {
            IsNotNull(value, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (value.Trim() == String.Empty)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not null or empty after being trimmed.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrWhiteSpace(string value, string paramName, string message, Exception innerException)
        {
            IsNotNull(value, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (value.Trim() == String.Empty)
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_IsNotEmpty, paramName, innerException);
        }
        #endregion

        #region MinRange
        #region decimal
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(decimal value, decimal minimum, string paramName)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(decimal? value, decimal minimum, string paramName)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(decimal value, decimal minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(decimal? value, decimal minimum, string paramName, string message)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(decimal value, decimal minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(decimal? value, decimal minimum, string message, Exception innerException)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(double value, double minimum, string paramName)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(double? value, double minimum, string paramName)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(double value, double minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(double? value, double minimum, string paramName, string message)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(double value, double minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(double? value, double minimum, string message, Exception innerException)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(float value, float minimum, string paramName)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(float? value, float minimum, string paramName)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(float value, float minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(float? value, float minimum, string paramName, string message)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(float value, float minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(float? value, float minimum, string message, Exception innerException)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(int value, int minimum, string paramName)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(int? value, int minimum, string paramName)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(int value, int minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(int? value, int minimum, string paramName, string message)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(int value, int minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(int? value, int minimum, string message, Exception innerException)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(long value, long minimum, string paramName)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(long? value, long minimum, string paramName)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(long value, long minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(long? value, long minimum, string paramName, string message)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(long value, long minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(long? value, long minimum, string message, Exception innerException)
        {
            if (value.HasValue && value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(DateTime value, DateTime minimum, string paramName)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MinRange(DateTime? value, DateTime minimum, string paramName)
        {
            if (value.HasValue && value.Value < minimum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(DateTime value, DateTime minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MinRange(DateTime? value, DateTime minimum, string paramName, string message)
        {
            if (value.HasValue && value.Value < minimum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(DateTime value, DateTime minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MinRange(DateTime? value, DateTime minimum, string message, Exception innerException)
        {
            if (value.HasValue && value.Value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion
        #endregion

        #region MaxRange
        #region decimal
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(decimal value, decimal maximum, string paramName)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(decimal? value, decimal maximum, string paramName)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(decimal value, decimal maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(decimal? value, decimal maximum, string paramName, string message)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(decimal value, decimal maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(decimal? value, decimal maximum, string message, Exception innerException)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(double value, double maximum, string paramName)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(double? value, double maximum, string paramName)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(double value, double maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(double? value, double maximum, string paramName, string message)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(double value, double maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(double? value, double maximum, string message, Exception innerException)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(float value, float maximum, string paramName)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(float? value, float maximum, string paramName)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(float value, float maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(float? value, float maximum, string paramName, string message)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(float value, float maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(float? value, float maximum, string message, Exception innerException)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(int value, int maximum, string paramName)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void MaxRange(int? value, int maximum, string paramName)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(int value, int maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(int? value, int maximum, string paramName, string message)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(int value, int maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(int? value, int maximum, string message, Exception innerException)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(long value, long maximum, string paramName)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(long? value, long maximum, string paramName)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(long value, long maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(long? value, long maximum, string paramName, string message)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(long value, long maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(long? value, long maximum, string message, Exception innerException)
        {
            if (value.HasValue && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(DateTime value, DateTime maximum, string paramName)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(DateTime? value, DateTime maximum, string paramName)
        {
            if (value.HasValue && value.Value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(DateTime value, DateTime maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void MaxRange(DateTime? value, DateTime maximum, string paramName, string message)
        {
            if (value.HasValue && value.Value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(DateTime value, DateTime maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void MaxRange(DateTime? value, DateTime maximum, string message, Exception innerException)
        {
            if (value.HasValue && value.Value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion
        #endregion

        #region Range
        #region decimal
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(decimal value, decimal minimum, decimal maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(decimal? value, decimal minimum, decimal maximum, string paramName)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(decimal value, decimal minimum, decimal maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(decimal? value, decimal minimum, decimal maximum, string paramName, string message)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(decimal value, decimal minimum, decimal maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(decimal? value, decimal minimum, decimal maximum, string message, Exception innerException)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(double value, double minimum, double maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(double? value, double minimum, double maximum, string paramName)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(double value, double minimum, double maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(double? value, double minimum, double maximum, string paramName, string message)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(double value, double minimum, double maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(double? value, double minimum, double maximum, string message, Exception innerException)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(float value, float minimum, float maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(float? value, float minimum, float maximum, string paramName)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(float value, float minimum, float maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(float? value, float minimum, float maximum, string paramName, string message)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(float value, float minimum, float maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(float? value, float minimum, float maximum, string message, Exception innerException)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(int value, int minimum, int maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(int? value, int minimum, int maximum, string paramName)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(int value, int minimum, int maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(int? value, int minimum, int maximum, string paramName, string message)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(int value, int minimum, int maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(int? value, int minimum, int maximum, string message, Exception innerException)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(long value, long minimum, long maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(long? value, long minimum, long maximum, string paramName)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(long value, long minimum, long maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(long? value, long minimum, long maximum, string paramName, string message)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(long value, long minimum, long maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(long? value, long minimum, long maximum, string message, Exception innerException)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(DateTime value, DateTime minimum, DateTime maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void Range(DateTime? value, DateTime minimum, DateTime maximum, string paramName)
        {
            if (value.HasValue && (value.Value < minimum || value.Value > maximum))
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(DateTime value, DateTime minimum, DateTime maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void Range(DateTime? value, DateTime minimum, DateTime maximum, string paramName, string message)
        {
            if (value.HasValue && (value.Value < minimum || value.Value > maximum))
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(DateTime value, DateTime minimum, DateTime maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void Range(DateTime? value, DateTime minimum, DateTime maximum, string message, Exception innerException)
        {
            if (value.HasValue && (value.Value < minimum || value.Value > maximum))
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion
        #endregion

        #region StartsWith
        /// <summary>
        /// If the value does not start with the appropriatevalu it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the parameter does not start with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="startsWithValue">The required start with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void StartsWith(string value, string startsWithValue, StringComparison comparisonType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.StartsWith(startsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Multilingual.Exception_Validation_StartsWith, startsWithValue), paramName);
        }

        /// <summary>
        /// If the value does not start with the appropriatevalu it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the parameter does not start with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="startsWithValue">The required start with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The original exception that caused this exception.</param>
        public static void StartsWith(string value, string startsWithValue, StringComparison comparisonType, string message, Exception innerException)
        {
            if (!value.StartsWith(startsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Multilingual.Exception_Validation_StartsWith, startsWithValue), innerException);
        }
        #endregion

        #region EndsWith
        /// <summary>
        /// If the value does not end with the appropriate value it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the parameter does not end with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="endsWithValue">The required end with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void EndsWith(string value, string endsWithValue, StringComparison comparisonType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.EndsWith(endsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Multilingual.Exception_Validation_EndsWith, endsWithValue), paramName);
        }

        /// <summary>
        /// If the value does not end with the appropriate value it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the parameter does not end with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="endsWithValue">The required end with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The original exception that caused this exception.</param>
        public static void EndsWith(string value, string endsWithValue, StringComparison comparisonType, string message, Exception innerException)
        {
            if (!value.EndsWith(endsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Multilingual.Exception_Validation_EndsWith, endsWithValue), innerException);
        }
        #endregion

        #region IsValue
        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparisonType">StringComparison type rule.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue(string value, string[] validValues, StringComparison comparisonType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (validValues.Where(v => string.Compare(v, value, comparisonType) == 0).Count() != 1)
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue(object value, object[] validValues, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validValues.Contains(value))
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.ArgumentOutOfRangeException.
        /// This method is most effective when ensuring a parameter property is appropriate.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValue">The only valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue(object value, object validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.Equals(validValue))
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparer">Method to determine if the value is valid.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue(object value, object[] validValues, IEqualityComparer<object> comparer, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validValues.Contains(value, comparer))
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparer">Method to determine if the value is valid. Func<validValue, value, result>.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue(object value, object[] validValues, Func<object, object, bool> comparer, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (validValues.Where(v => comparer(v, value)).Count() != 1)
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.ArgumentException.
        /// This method is most effective when ensuring a parameter property is appropriate.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" is must be a valid value.</exception>
        /// <typeparam name="T">Type of object to compare.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="validValue">The only valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue<T>(T value, T validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.Equals(validValue))
                throw new ArgumentException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.ArgumentException.
        /// This method is most effective when ensuring a parameter property is appropriate.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" is must be a valid value.</exception>
        /// <typeparam name="T">Type of object to compare.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue<T>(T value, T[] validValues, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validValues.Contains(value))
                throw new ArgumentException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <typeparam name="T">Type of object to compare.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparer">Method to determine if the value is valid. Func<validValue, value, result>.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void IsValue<T>(T value, T[] validValues, Func<T, T, bool> comparer, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (validValues.Where(v => comparer(v, value)).Count() != 1)
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }
        #endregion

        #region IsNotValue
        /// <summary>
        /// Asserts that the parameter value is equal to the invalid value.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="invalidValue">Invalid value that the value cannot be equal to.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotValue(object value, object invalidValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value.Equals(invalidValue))
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_Invalid_Value, paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is equal to the invalid value.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <typeparam name="T">Type of parameter being compared.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="invalidValue">Invalid value that the value cannot be equal to.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void IsNotValue<T>(T value, T invalidValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value.Equals(invalidValue))
                throw new ArgumentException(message ?? Resources.Multilingual.Exception_Validation_Invalid_Value, paramName);
        }
        #endregion

        #region IsTrue
        /// <summary>
        /// Assert that the parameter is true.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned false.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsTrue(bool value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value)
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// Assert that the parameter is true.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned false.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsTrue(bool value, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value)
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the function returns true.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "function" returned false.</exception>
        /// <param name="function">Function to call to determine the value of the parameter.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsTrue(Func<bool> function, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!function())
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// Assert that the function returns true.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "function" returned false.</exception>
        /// <param name="function">Function to call to determine the value of the parameter.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsTrue(Func<bool> function, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!function())
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName, innerException);
        }
        #endregion

        #region IsFalse
        /// <summary>
        /// Assert that the parameter is false.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsFalse(bool value, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value)
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// Assert that the parameter is false.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsFalse(bool value, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value)
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the function returns false.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "function" returned true.</exception>
        /// <param name="function">Function to call to determine the value of the parameter.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsFalse(Func<bool> function, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (function())
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName);
        }

        /// <summary>
        /// Assert that the function returns false.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "function" returned true.</exception>
        /// <param name="function">Function to call to determine the value of the parameter.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsFalse(Func<bool> function, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (function())
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Value, paramName), paramName, innerException);
        }
        #endregion

        #region IsType
        /// <summary>
        /// Assert that the parameter is of the specified type.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsType(object value, Type validType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);
            
            if (value.GetType() != validType)
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Type, paramName), paramName);
        }

        /// <summary>
        /// Assert that the parameter is of the specified type.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsType(object value, Type validType, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value.GetType() != validType)
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Type, paramName), paramName, innerException);
        }
        #endregion

#if !WINDOWS_APP && !WINDOWS_PHONE_APP
        #region HasAttribute
        /// <summary>
        /// Assert that the parameter has an attribute of the specified type defined.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "element" must have a attribute of the specified type defined.</exception>
        /// <param name="type">Type to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message to describe the exception.</param>
        public static void HasAttribute(Type type, Type attributeType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!type.HasAttribute(attributeType))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Attribute_Required, attributeType.Name), paramName);
        }

        /// <summary>
        /// Assert that the parameter has an attribute of the specified type defined.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "element" must have a attribute of the specified type defined.</exception>
        /// <param name="type">Type to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will also look in the ancestor objects for the attribute type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message to describe the exception.</param>
        public static void HasAttribute(Type type, Type attributeType, bool inherit, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!type.HasAttribute(attributeType, inherit))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Attribute_Required, attributeType.Name), paramName);
        }

        /// <summary>
        /// Assert that the parameter has an attribute of the specified type defined.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "element" must have a attribute of the specified type defined.</exception>
        /// <param name="element">Element to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message to describe the exception.</param>
        public static void HasAttribute(object element, Type attributeType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!element.HasAttribute(attributeType))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Attribute_Required, attributeType.Name), paramName);
        }

        /// <summary>
        /// Assert that the parameter has an attribute of the specified type defined.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "element" must have a attribute of the specified type defined.</exception>
        /// <param name="element">Element to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will also look in the ancestor objects for the attribute type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message to describe the exception.</param>
        public static void HasAttribute(object element, Type attributeType, bool inherit, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!element.HasAttribute(attributeType, inherit))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Attribute_Required, attributeType.Name), paramName);
        }
        #endregion

        #region IsAssignableFromType
        /// <summary>
        /// Assert that the parameter is assignable from the specified type.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsAssignableFromType(object value, Type validType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validType.IsAssignableFrom(value.GetType()))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Type, paramName), paramName);
        }

        /// <summary>
        /// Assert that the parameter is assignable from the specified type.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="value">Parameter value.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsAssignableFromType(object value, Type validType, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validType.IsAssignableFrom(value.GetType()))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Type, paramName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the parameter is assignable from the specified type.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="valueType">Parameter value type.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        public static void IsAssignableFromType(Type valueType, Type validType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validType.IsAssignableFrom(valueType))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Type, paramName), paramName);
        }

        /// <summary>
        /// Assert that the parameter is assignable from the specified type.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" returned true.</exception>
        /// <param name="valueType">Parameter value type.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the parameter being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">Exception that occured that caused this exception.</param>
        public static void IsAssignableFromType(Type valueType, Type validType, string paramName, string message, Exception innerException)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validType.IsAssignableFrom(valueType))
                throw new ArgumentException(message ?? String.Format(Resources.Multilingual.Exception_Validation_Invalid_Type, paramName), paramName, innerException);
        }
        #endregion
#endif
        #endregion
    }
}
