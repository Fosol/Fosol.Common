﻿#if !WINDOWS_APP && !WINDOWS_PHONE_APP
using Fosol.Common.Extensions.Attributes;
using Fosol.Common.Extensions.Types;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Fosol.Common.Validation.Property
{
    /// <summary>
    /// Assert static class provides a simple way to validate property values and throw a PropertyException if the value is invalid.
    /// </summary>
    public static class Assert
    {
        #region Methods

        #region IsValidIndexPosition
        /// <summary>
        /// Asserts that index value is a valid position within the specified range.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="indexPosition">Index position within the specified collection.</param>
        /// <param name="count">Number of items within the enumerable collection.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void IsValidIndexPosition(int indexPosition, int count, string paramName, string message = null, Exception innerException = null)
        {
            if (indexPosition < 0 || indexPosition >= count)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(Resources.Multilingual.Exception_Validation_Property_IsValidIndexPosition, paramName), innerException);
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified array.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property 'index' value must be a valid position within the specified array.</exception>
        /// <param name="indexPosition">Index position within the specified array.</param>
        /// <param name="array">Array to test the index position in.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void IsValidIndexPosition(int indexPosition, Array array, string paramName, string message = null, Exception innerException = null)
        {
            if (indexPosition < 0 || indexPosition >= array.Length)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(Resources.Multilingual.Exception_Validation_Property_IsValidIndexPosition, paramName), innerException);
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified collection.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="indexPosition">Index position within the specified collection.</param>
        /// <param name="collection">Collection to test the index position in.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void IsValidIndexPosition(int indexPosition, ICollection collection, string paramName, string message = null, Exception innerException = null)
        {
            if (indexPosition < 0 || indexPosition >= collection.Count)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(Resources.Multilingual.Exception_Validation_Property_IsValidIndexPosition, paramName), innerException);
        }
        #endregion

        #region IsEqual
        /// <summary>
        /// Asserts that the function returns a value equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.PropertyException">Property 'function' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual(Func<object> function, object validValue, string paramName, string message = null, Exception innerException = null)
        {
            if (!ReferenceEquals(function, validValue)
                || !ReferenceEquals(function(), validValue))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsEqual, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the function returns a value equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.PropertyException">Property 'function' does not return a valid value.</exception>
        /// <param name="function">Function to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual<T>(Func<T> function, T validValue, string paramName, string message = null, Exception innerException = null)
        {
            if (!ReferenceEquals(function, validValue)
                || !ReferenceEquals(function(), validValue))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsEqual, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the value is equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.PropertyException">Property 'value' does not return a valid value.</exception>
        /// <param name="value">Value to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual(object value, object validValue, string paramName, string message = null, Exception innerException = null)
        {
            if (!ReferenceEquals(value, validValue))
                throw new Exceptions.PropertyException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsEqual, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the value is equal to the validValue specified.
        /// </summary>
        /// <exception cref="System.PropertyException">Property 'value' does not return a valid value.</exception>
        /// <param name="value">Value to validate.</param>
        /// <param name="validValue">Valid value.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsEqual<T>(T value, T validValue, string paramName, string message = null, Exception innerException = null)
        {
            if (!ReferenceEquals(value, validValue))
                throw new Exceptions.PropertyException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsEqual, paramName), paramName, innerException);
        }
        #endregion

        #region IsNotNull
        /// <summary>
        /// Asserts that the result of the function is not null.
        /// If it is null it will throw System.PropertyNullException.
        /// </summary>
        /// <exception cref="System.PropertyNullException">Property "function" return value is null.</exception>
        /// <param name="function">The function to execute.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull(Func<object> function, string paramName, string message = null, Exception innerException = null)
        {
            if (function == null || function() == null)
                throw new Exceptions.PropertyNullException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotNull, paramName), innerException);
        }

        /// <summary>
        /// Asserts that the result of the function is not null.
        /// If it is null it will throw System.PropertyNullException.
        /// </summary>
        /// <exception cref="System.PropertyNullException">Property "function" return value is null.</exception>
        /// <typeparam name="T">Type of the value the function returns.</typeparam>
        /// <param name="function">The function to execute.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull<T>(Func<T> function, string paramName, string message = null, Exception innerException = null)
        {
            if (function == null || function() == null)
                throw new Exceptions.PropertyNullException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotNull, paramName), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not null.
        /// If it is null it will throw System.PropertyNullException.
        /// </summary>
        /// <exception cref="System.PropertyNullException">If the Property is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull(object value, string paramName, string message = null, Exception innerException = null)
        {
            if (value == null)
                throw new Exceptions.PropertyNullException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotNull, paramName), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not null.
        /// If it is null it will throw System.PropertyNullException.
        /// </summary>
        /// <exception cref="System.PropertyNullException">If the Property is null.</exception>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNull<T>(T value, string paramName, string message = null, Exception innerException = null)
        {
            if (value == null)
                throw new Exceptions.PropertyNullException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotNull, paramName), innerException);
        }
        #endregion

        #region IsNotNullOrEmpty
        /// <summary>
        /// Asserts that the Property value is not null or empty.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty(string value, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(value, paramName);
            if (value == String.Empty)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the Property collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="collection">Property of type enumerable.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty(IEnumerable<object> collection, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(collection, paramName);
            if (collection.Count() == 0)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the Property collection is not null and not empty.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="collection">Property of type enumerable.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty<T>(IEnumerable<T> collection, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(collection, paramName);
            if (collection.Count() == 0)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the Property array is not null and not empty.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="collection">Property of type array.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty(object[] array, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(array, paramName);
            if (array.Length == 0)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the Property array is not null and not empty.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="collection">Property of type array.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty(byte[] array, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(array, paramName);
            if (array.Length == 0)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the Property array is not null and not empty.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="collection">Property of type array.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Message to describe the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrEmpty<T>(T[] array, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(array, paramName);
            if (array.Length == 0)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }
        #endregion

        #region IsNotNullOrWhiteSpace
        /// <summary>
        /// Asserts that the Property value is not null or empty after being trimmed.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <exception cref="System.PropertyNullException">Property "value" cannot be null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotNullOrWhiteSpace(string value, string paramName, string message = null, Exception innerException = null)
        {
            IsNotNull(value, paramName);
            if (value.Trim() == String.Empty)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotEmpty, paramName), paramName, innerException);
        }
        #endregion

        #region MinRange
        #region decimal
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(decimal value, decimal minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(decimal? value, decimal minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(double value, double minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(double? value, double minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(float value, float minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(float? value, float minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(int value, int minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(int? value, int minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(long value, long minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(long? value, long minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(DateTime value, DateTime minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MinRange(DateTime? value, DateTime minimum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value.Value < minimum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MinRange, paramName, minimum), innerException);
        }
        #endregion
        #endregion

        #region MaxRange
        #region decimal
        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(decimal value, decimal maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(decimal? value, decimal maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(double value, double maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(double? value, double maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(float value, float maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(float? value, float maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(int value, int maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(int? value, int maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(long value, long maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(long? value, long maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(DateTime value, DateTime maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void MaxRange(DateTime? value, DateTime maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && value.Value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_MaxRange, paramName, maximum), innerException);
        }
        #endregion
        #endregion

        #region WithinRange
        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="withinRange">Property value to check.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void WithinRange(bool withinRange, string paramName, string message = null, Exception innerException = null)
        {
            if (!withinRange)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_WithinRange, paramName), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="withinRange">Function to check.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void WithinRange(Func<bool> withinRange, string paramName, string message = null, Exception innerException = null)
        {
            if (!withinRange())
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_WithinRange, paramName), innerException);
        }
        #endregion

        #region Range
        #region decimal
        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(decimal value, decimal minimum, decimal maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum || value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(decimal? value, decimal minimum, decimal maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }
        #endregion

        #region double
        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(double value, double minimum, double maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum || value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(double? value, double minimum, double maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }
        #endregion

        #region float
        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(float value, float minimum, float maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum || value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(paramName);
            throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(float? value, float minimum, float maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }
        #endregion

        #region int
        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(int value, int minimum, int maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum || value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(int? value, int minimum, int maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }
        #endregion

        #region long
        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(long value, long minimum, long maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum || value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(long? value, long minimum, long maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && (value < minimum || value > maximum))
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(DateTime value, DateTime minimum, DateTime maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value < minimum || value > maximum)
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }

        /// <summary>
        /// Asserts that the Property value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Value must be within the specified range.</exception>
        /// <param name="value">Property value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void Range(DateTime? value, DateTime minimum, DateTime maximum, string paramName, string message = null, Exception innerException = null)
        {
            if (value.HasValue && (value.Value < minimum || value.Value > maximum))
                throw new Exceptions.PropertyOutOfRangeException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_Range, paramName, minimum, maximum), innerException);
        }
        #endregion
        #endregion

        #region StartsWith
        /// <summary>
        /// If the value does not start with the appropriatevalu it will throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">If the Property does not start with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="startsWithValue">The required start with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void StartsWith(string value, string startsWithValue, StringComparison comparisonType, string paramName, string message = null, Exception innerException = null)
        {
            if (!value.StartsWith(startsWithValue, comparisonType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_StartsWith, paramName, startsWithValue), paramName, innerException);
        }
        #endregion

        #region EndsWith
        /// <summary>
        /// If the value does not end with the appropriate value it will throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">If the Property does not end with the value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="endsWithValue">The required end with value.</param>
        /// <param name="comparisonType">StringComparison type.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void EndsWith(string value, string endsWithValue, StringComparison comparisonType, string paramName, string message = null, Exception innerException = null)
        {
            if (!value.EndsWith(endsWithValue, comparisonType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_EndsWith, paramName, endsWithValue), paramName, innerException);
        }
        #endregion

        #region IsValue
        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.PropertyOutOfRangeException.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparisonType">StringComparison type rule.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue(string value, string[] validValues, StringComparison comparisonType, string paramName, string message = null, Exception innerException = null)
        {
            if (validValues.Where(v => string.Compare(v, value, comparisonType) == 0).Count() != 1)
                throw new Exceptions.PropertyOutOfRangeException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.PropertyOutOfRangeException.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue(object value, object[] validValues, string paramName, string message = null, Exception innerException = null)
        {
            if (!validValues.Contains(value))
                throw new Exceptions.PropertyOutOfRangeException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.PropertyOutOfRangeException.
        /// This method is most effective when ensuring a Property property is appropriate.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValue">The only valid value.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue(object value, object validValue, string paramName, string message = null, Exception innerException = null)
        {
            if (!value.Equals(validValue))
                throw new Exceptions.PropertyOutOfRangeException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.PropertyOutOfRangeException.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparer">Method to determine if the value is valid.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue(object value, object[] validValues, IEqualityComparer<object> comparer, string paramName, string message = null, Exception innerException = null)
        {
            if (!validValues.Contains(value, comparer))
                throw new Exceptions.PropertyOutOfRangeException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.PropertyOutOfRangeException.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property "value" is must be a valid value.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparer">Method to determine if the value is valid. Func<validValue, value, result>.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue(object value, object[] validValues, Func<object, object, bool> comparer, string paramName, string message = null, Exception innerException = null)
        {
            if (validValues.Where(v => comparer(v, value)).Count() != 1)
                throw new Exceptions.PropertyOutOfRangeException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.PropertyException.
        /// This method is most effective when ensuring a Property property is appropriate.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" is must be a valid value.</exception>
        /// <typeparam name="T">Type of object to compare.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="validValue">The only valid value.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue<T>(T value, T validValue, string paramName, string message = null, Exception innerException = null)
        {
            if (!value.Equals(validValue))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not equal the valid value it will throw System.PropertyException.
        /// This method is most effective when ensuring a Property property is appropriate.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" is must be a valid value.</exception>
        /// <typeparam name="T">Type of object to compare.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue<T>(T value, T[] validValues, string paramName, string message = null, Exception innerException = null)
        {
            if (!validValues.Contains(value))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }

        /// <summary>
        /// If the value does not exist in the valid values array it will throw System.PropertyOutOfRangeException.
        /// </summary>
        /// <exception cref="System.PropertyOutOfRangeException">Property "value" is must be a valid value.</exception>
        /// <typeparam name="T">Type of object to compare.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">An array of valid values to compare against.</param>
        /// <param name="comparer">Method to determine if the value is valid. Func<validValue, value, result>.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message describing the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsValue<T>(T value, T[] validValues, Func<T, T, bool> comparer, string paramName, string message = null, Exception innerException = null)
        {
            if (validValues.Where(v => comparer(v, value)).Count() != 1)
                throw new Exceptions.PropertyOutOfRangeException(string.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsValue, paramName), innerException);
        }
        #endregion

        #region IsNotValue
        /// <summary>
        /// Asserts that the Property value is equal to the invalid value.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="invalidValue">Invalid value that the value cannot be equal to.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotValue(object value, object invalidValue, string paramName, string message = null, Exception innerException = null)
        {
            if (value.Equals(invalidValue))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotValue, paramName), paramName, innerException);
        }

        /// <summary>
        /// Asserts that the Property value is equal to the invalid value.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" cannot be empty.</exception>
        /// <typeparam name="T">Type of Property being compared.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="invalidValue">Invalid value that the value cannot be equal to.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsNotValue<T>(T value, T invalidValue, string paramName, string message = null, Exception innerException = null)
        {
            if (value.Equals(invalidValue))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsNotValue, paramName), paramName, innerException);
        }
        #endregion

        #region IsTrue
        /// <summary>
        /// Assert that the Property is true.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" returned false.</exception>
        /// <param name="value">Property value.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsTrue(bool value, string paramName, string message = null, Exception innerException = null)
        {
            if (!value)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsTrue, paramName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the function returns true.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "function" returned false.</exception>
        /// <param name="function">Function to call to determine the value of the Property.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsTrue(Func<bool> function, string paramName, string message = null, Exception innerException = null)
        {
            if (!function())
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsTrue, paramName), paramName, innerException);
        }
        #endregion

        #region IsFalse
        /// <summary>
        /// Assert that the Property is false.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" returned true.</exception>
        /// <param name="value">Property value.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsFalse(bool value, string paramName, string message = null, Exception innerException = null)
        {
            if (value)
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsFalse, paramName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the function returns false.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "function" returned true.</exception>
        /// <param name="function">Function to call to determine the value of the Property.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsFalse(Func<bool> function, string paramName, string message = null, Exception innerException = null)
        {
            if (function())
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsFalse, paramName), paramName, innerException);
        }
        #endregion

        #region IsType
        /// <summary>
        /// Assert that the Property is of the specified type.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" returned true.</exception>
        /// <param name="value">Property value.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsType(object value, Type validType, string paramName, string message = null, Exception innerException = null)
        {
            if (value == null 
                || !(value.GetType() == validType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsType, paramName, validType.FullName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the Property is of the specified type.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" returned true.</exception>
        /// <param name="type">Property type.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsType(Type type, Type validType, string paramName, string message = null, Exception innerException = null)
        {
            if (type == null
                || !(type == validType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsType, paramName, validType.FullName), paramName, innerException);
        }
        #endregion

#if !WINDOWS_APP && !WINDOWS_PHONE_APP
        #region HasAttribute
        /// <summary>
        /// Assert that the Property has an attribute of the specified type defined.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "element" must have a attribute of the specified type defined.</exception>
        /// <param name="type">Type to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void HasAttribute(Type type, Type attributeType, string paramName, string message = null, Exception innerException = null)
        {
            if (!type.HasAttribute(attributeType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_HasAttribute, paramName, attributeType.FullName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the Property has an attribute of the specified type defined.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "element" must have a attribute of the specified type defined.</exception>
        /// <param name="type">Type to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will also look in the ancestor objects for the attribute type.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void HasAttribute(Type type, Type attributeType, bool inherit, string paramName, string message = null, Exception innerException = null)
        {
            if (!type.HasAttribute(attributeType, inherit))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_HasAttribute, paramName, attributeType.FullName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the Property has an attribute of the specified type defined.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "element" must have a attribute of the specified type defined.</exception>
        /// <param name="element">Element to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void HasAttribute(object element, Type attributeType, string paramName, string message = null, Exception innerException = null)
        {
            if (!element.HasAttribute(attributeType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_HasAttribute, paramName, attributeType.FullName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the Property has an attribute of the specified type defined.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "element" must have a attribute of the specified type defined.</exception>
        /// <param name="element">Element to check for the specified attribute.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will also look in the ancestor objects for the attribute type.</param>
        /// <param name="paramName">Name of the Property.</param>
        /// <param name="message">Error message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void HasAttribute(object element, Type attributeType, bool inherit, string paramName, string message = null, Exception innerException = null)
        {
            if (!element.HasAttribute(attributeType, inherit))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_HasAttribute, paramName, attributeType.FullName), paramName, innerException);
        }
        #endregion

        #region IsAssignable
        /// <summary>
        /// Assert that the Property is of the specified type.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" returned true.</exception>
        /// <param name="value">Property value.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsAssignable(object value, Type validType, string paramName, string message = null, Exception innerException = null)
        {
            if (value == null
                || !validType.IsAssignableFrom(value.GetType()))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsAssignable, paramName, validType.FullName), paramName, innerException);
        }

        /// <summary>
        /// Assert that the Property is of the specified type.
        /// If not throw System.PropertyException.
        /// </summary>
        /// <exception cref="System.PropertyException">Property "value" returned true.</exception>
        /// <param name="type">Property type.</param>
        /// <param name="validType">Valid type.</param>
        /// <param name="paramName">Name of the Property being tested.</param>
        /// <param name="message">Error message describing the exception</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void IsAssignable(Type type, Type validType, string paramName, string message = null, Exception innerException = null)
        {
            if (type == null
                || (!type.IsSubclassOf(validType)
                && type == validType))
                throw new Exceptions.PropertyException(String.Format(message ?? Resources.Multilingual.Exception_Validation_Property_IsAssignable, paramName, validType.FullName), paramName, innerException);
        }
        #endregion
#endif
        #endregion
    }
}
