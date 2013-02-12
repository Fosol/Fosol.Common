﻿using System;
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
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #region AssertNotNull
        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertNotNull(object value, string paramName)
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
        public static void AssertNotNull(object value, string paramName, string message)
        {
            if (value == null)
                throw new ArgumentNullException(paramName,  message);
        }

        /// <summary>
        /// Asserts that the parameter value is not null.
        /// If it is null it will throw System.ArgumentNullException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the parameter is null.</exception>
        /// <param name="value">The value to check.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void AssertNotNull(object value, string message, Exception innerException)
        {
            if (value == null)
                throw new ArgumentNullException(message, innerException);
        }
        #endregion

        #region AssertNotNullOrEmtpy
        /// <summary>
        /// Asserts that the parameter value is not null or empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        public static void AssertNotNullOrEmpty(string value, string paramName, string message = null)
        {
            AssertNotNull(value, paramName);

            if (value == String.Empty)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not null or empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public static void AssertNotNullOrEmpty(string value, string paramName, string message, Exception innerException)
        {
            AssertNotNull(value, paramName);

            if (value == String.Empty)
                throw new ArgumentException(message ?? Resources.Strings.Exception_NotNullOrEmpty, paramName, innerException);
        }
        #endregion

        #region AssertMinRange
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
                throw new ArgumentOutOfRangeException(paramName, message);
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
                throw new ArgumentOutOfRangeException(paramName, message);
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

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinRange(Single value, Single minimum, string paramName)
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
        public static void AssertMinRange(Single value, Single minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(Single value, Single minimum, string message, Exception innerException)
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
                throw new ArgumentOutOfRangeException(paramName, message);
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

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinRange(Int16 value, Int16 minimum, string paramName)
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
        public static void AssertMinRange(Int16 value, Int16 minimum, string paramName, string message)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is greater than or equal to the minimum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is less than the minimum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="minimum">Minimum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinRange(Int16 value, Int16 minimum, string message, Exception innerException)
        {
            if (value < minimum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region AssertMaxRange
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
                throw new ArgumentOutOfRangeException(paramName, message);
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
                throw new ArgumentOutOfRangeException(paramName, message);
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

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMaxRange(Single value, Single maximum, string paramName)
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
        public static void AssertMaxRange(Single value, Single maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(Single value, Single maximum, string message, Exception innerException)
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
                throw new ArgumentOutOfRangeException(paramName, message);
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

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMaxRange(Int16 value, Int16 maximum, string paramName)
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
        public static void AssertMaxRange(Int16 value, Int16 maximum, string paramName, string message)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is less than or equal to the maximum value.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is greater than the maximum.</exception>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMaxRange(Int16 value, Int16 maximum, string message, Exception innerException)
        {
            if (value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
        #endregion

        #region AssertMinMaxRange
        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinMaxRange(int value, int minimum, int maximum, string paramName)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinMaxRange(int value, int minimum, int maximum, string paramName, string message)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinMaxRange(int value, int minimum, int maximum, string message, Exception innerException)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinMaxRange(long value, long minimum, long maximum, string paramName)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinMaxRange(long value, long minimum, long maximum, string paramName, string message)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinMaxRange(long value, long minimum, long maximum, string message, Exception innerException)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinMaxRange(Single value, Single minimum, Single maximum, string paramName)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinMaxRange(Single value, Single minimum, Single maximum, string paramName, string message)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinMaxRange(Single value, Single minimum, Single maximum, string message, Exception innerException)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinMaxRange(double value, double minimum, double maximum, string paramName)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinMaxRange(double value, double minimum, double maximum, string paramName, string message)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinMaxRange(double value, double minimum, double maximum, string message, Exception innerException)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void AssertMinMaxRange(Int16 value, Int16 minimum, Int16 maximum, string paramName)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void AssertMinMaxRange(Int16 value, Int16 minimum, Int16 maximum, string paramName, string message)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Asserts that the parameter value is not less than the minimum value and is not greater than the maximum value..
        /// </summary>
        /// <param name="value">Parameter value to check.</param>
        /// <param name="maximum">Maximum value allowed.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void AssertMinMaxRange(Int16 value, Int16 minimum, Int16 maximum, string message, Exception innerException)
        {
            if (value < minimum && value > maximum)
                throw new ArgumentOutOfRangeException(message, innerException);
        }
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
        #endregion

        #region Events
        #endregion
    }
}
