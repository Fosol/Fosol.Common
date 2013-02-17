using Fosol.Common.Extensions.Attributes;
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
    public static class Parameter
    {
        #region Methods
        #region AssertIsNotNull
        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertIsNotNull(object value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void AssertIsNotNull(object value, string paramName, string message)
        {
            if (value == null)
                throw new ArgumentNullException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void AssertIsNotNull(object value, string message, Exception innerException)
        {
            if (value == null)
                throw new ArgumentNullException(message, innerException);
        }
        #endregion

        #region AssertIsNotNullOrEmpty
        /// <summary>
        /// Asserts that the parameter value is not null or empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void AssertIsNotNullOrEmpty(string value, string paramName, string message = null)
        {
            AssertIsNotNull(value, paramName);

            if (value == String.Empty)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName);
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
        public static void AssertIsNotNullOrEmpty(string value, string paramName, string message, Exception innerException)
        {
            AssertIsNotNull(value, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (value == String.Empty)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type enumerable.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void AssertIsNotNullOrEmpty(IEnumerable<object> collection, string paramName, string message = null)
        {
            AssertIsNotNull(collection, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (collection.Count() == 0)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName);
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
        public static void AssertIsNotNullOrEmpty(IEnumerable<object> collection, string paramName, string message, Exception innerException)
        {
            AssertIsNotNull(collection, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (collection.Count() == 0)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void AssertIsNotNullOrEmpty(object[] array, string paramName, string message = null)
        {
            AssertIsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName);
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
        public static void AssertIsNotNullOrEmpty(object[] array, string paramName, string message, Exception innerException)
        {
            AssertIsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName, innerException);
        }

        /// <summary>
        /// Asserts that the parameter array is not null and not empty.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="collection">Parameter of type array.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Message to describe the error.</param>
        public static void AssertIsNotNullOrEmpty(byte[] array, string paramName, string message = null)
        {
            AssertIsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName);
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
        public static void AssertIsNotNullOrEmpty(byte[] array, string paramName, string message, Exception innerException)
        {
            AssertIsNotNull(array, paramName);

            if (message != null)
                message = string.Format(message, paramName);

            if (array.Length == 0)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName, innerException);
        }
        #endregion

        #region AssertMinRange
        #region decimal
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinRange(decimal value, decimal minimum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinRange(decimal value, decimal minimum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(decimal value, decimal minimum, string message, Exception innerException)
        {
            if (value < minimum)
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
        public static void AssertMinRange(double value, double minimum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinRange(double value, double minimum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(double value, double minimum, string message, Exception innerException)
        {
            if (value < minimum)
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
        public static void AssertMinRange(float value, float minimum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinRange(float value, float minimum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(float value, float minimum, string message, Exception innerException)
        {
            if (value < minimum)
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
        public static void AssertMinRange(int value, int minimum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinRange(int value, int minimum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(int value, int minimum, string message, Exception innerException)
        {
            if (value < minimum)
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
        public static void AssertMinRange(long value, long minimum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinRange(long value, long minimum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(long value, long minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region short
        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinRange(short value, short minimum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinRange(short value, short minimum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(short value, short minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion
        #endregion

        #region AssertMaxRange
        #region decimal
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMaxRange(decimal value, decimal maximum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMaxRange(decimal value, decimal maximum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(decimal value, decimal maximum, string message, Exception innerException)
        {
            if (value > maximum)
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
        public static void AssertMaxRange(double value, double maximum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMaxRange(double value, double maximum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(double value, double maximum, string message, Exception innerException)
        {
            if (value > maximum)
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
        public static void AssertMaxRange(float value, float maximum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMaxRange(float value, float maximum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(float value, float maximum, string message, Exception innerException)
        {
            if (value > maximum)
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
        public static void AssertMaxRange(int value, int maximum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMaxRange(int value, int maximum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(int value, int maximum, string message, Exception innerException)
        {
            if (value > maximum)
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
        public static void AssertMaxRange(long value, long maximum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMaxRange(long value, long maximum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(long value, long maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region short
        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMaxRange(short value, short maximum, string paramName)
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
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMaxRange(short value, short maximum, string paramName, string message)
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
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(short value, short maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion
        #endregion

        #region AssertRange
        #region decimal
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertRange(decimal value, decimal minimum, decimal maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(decimal value, decimal minimum, decimal maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertRange(decimal value, decimal minimum, decimal maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(double value, double minimum, double maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(double value, double minimum, double maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertRange(double value, double minimum, double maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertRange(float value, float minimum, float maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(float value, float minimum, float maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertRange(float value, float minimum, float maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertRange(int value, int minimum, int maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(int value, int minimum, int maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertRange(int value, int minimum, int maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertRange(long value, long minimum, long maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(long value, long minimum, long maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertRange(long value, long minimum, long maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region short
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertRange(short value, short minimum, short maximum, string paramName)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertRange(short value, short minimum, short maximum, string paramName, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(paramName, string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertRange(short value, short minimum, short maximum, string message, Exception innerException)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion
        #endregion

        #region AssertStartsWith
        /// <summary>
        /// If the value does not start with the appropriatevalu it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the parameter does not start with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="startsWithValue">The required start with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void AssertStartsWith(string value, string startsWithValue, StringComparison comparisonType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.StartsWith(startsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Strings.Exception_StartsWith, startsWithValue), paramName);
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
        public static void AssertStartsWith(string value, string startsWithValue, StringComparison comparisonType, string message, Exception innerException)
        {
            if (!value.StartsWith(startsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Strings.Exception_StartsWith, startsWithValue), innerException);
        }
        #endregion

        #region AssertEndsWith
        /// <summary>
        /// If the value does not end with the appropriate value it will throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the parameter does not end with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="endsWithValue">The required end with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void AssertEndsWith(string value, string endsWithValue, StringComparison comparisonType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.EndsWith(endsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Strings.Exception_EndsWith, endsWithValue), paramName);
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
        public static void AssertEndsWith(string value, string endsWithValue, StringComparison comparisonType, string message, Exception innerException)
        {
            if (!value.EndsWith(endsWithValue, comparisonType))
                throw new ArgumentException(message ?? string.Format(Resources.Strings.Exception_EndsWith, endsWithValue), innerException);
        }
        #endregion

        #region AssertIsValue
        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparisonType">StringComparison type rule.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void AssertIsValue(string value, string[] validValues, StringComparison comparisonType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (validValues.Where(v => string.Compare(v, value, comparisonType) == 0).Count() != 1)
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.ArgumentOutOfRangeException.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void AssertIsValue(object value, object[] validValues, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validValues.Contains(value))
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.ArgumentException.
        /// This method is most effective when ensuring a parameter property is appropriate.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValue">The only valid value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message describing the exception.</param>
        public static void AssertIsValue(object value, object validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.Equals(validValue))
                throw new ArgumentException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
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
        public static void AssertIsValue(object value, object[] validValues, IEqualityComparer<object> comparer, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!validValues.Contains(value, comparer))
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
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
        public static void AssertIsValue(object value, object[] validValues, Func<object, object, bool> comparer, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (validValues.Where(v => comparer(v, value)).Count() != 1)
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
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
        public static void AssertIsValue<T>(T value, T validValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!value.Equals(validValue))
                throw new ArgumentException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
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
        public static void AssertIsValue<T>(T value, T[] validValues, Func<T, T, bool> comparer, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (validValues.Where(v => comparer(v, value)).Count() != 1)
                throw new ArgumentOutOfRangeException(message ?? string.Format(Resources.Strings.Exception_InvalidValue, paramName), paramName);
        }
        #endregion

        #region AssertIsNotValue
        /// <summary>
        /// Asserts that the parameter value is equal to the invalid value.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="invalidValue">Invalid value that the value cannot be equal to.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void AssertIsNotValue(object value, object invalidValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value.Equals(invalidValue))
                throw new ArgumentException(message ?? Resources.Strings.Exception_InvalidValue, paramName);
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
        public static void AssertIsNotValue<T>(T value, T invalidValue, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (value.Equals(invalidValue))
                throw new ArgumentException(message ?? Resources.Strings.Exception_InvalidValue, paramName);
        }
        #endregion

        #region AssertHasAttribute
        /// <summary>
        /// Assert that the parameter has an attribute of the specified type defined.
        /// If not throw System.ArgumentException.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "element" must have a attribute of the specified type defined.</exception>
        /// <param name="element">Element to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Error message to describe the exception.</param>
        public static void AssertHasAttribute(object element, Type attributeType, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!element.HasAttribute(typeof(System.Runtime.Serialization.DataContractAttribute)))
                throw new ArgumentException(message ?? String.Format(Resources.Strings.Exception_AttributeMissing, attributeType.Name), paramName);
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
        public static void AssertHasAttribute(object element, Type attributeType, bool inherit, string paramName, string message = null)
        {
            if (message != null)
                message = string.Format(message, paramName);

            if (!element.HasAttribute(typeof(System.Runtime.Serialization.DataContractAttribute), inherit))
                throw new ArgumentException(message ?? String.Format(Resources.Strings.Exception_AttributeMissing, attributeType.Name), paramName);
        }
        #endregion
        #endregion
    }
}
