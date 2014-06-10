using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Validation.Enumerables
{
    public static class Assert
    {
        #region Manual
        /// <summary>
        /// Asserts that index value is a valid position within the specified range.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="index">Index position within the specified collection.</param>
        /// <param name="count">Number of items within the enumerable collection.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void IsValidIndexPosition(int index, int count, string paramName)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException(String.Format(Resources.Strings.Exception_Validation_Assert_Enumerables_IsValidIndexPosition, paramName));
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified range.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="index">Index position within the specified collection.</param>
        /// <param name="count">Number of items within the enumerable collection.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void IsValidIndexPosition(int index, int count, string paramName, string message)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException(string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified range.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="index">Index position within the specified collection.</param>
        /// <param name="count">Number of items within the enumerable collection.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void IsValidIndexPosition(int index, int count, string message, Exception innerException)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException(message, innerException);
        }
        #endregion

        #region Array
        /// <summary>
        /// Asserts that index value is a valid position within the specified array.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified array.</exception>
        /// <param name="index">Index position within the specified array.</param>
        /// <param name="array">Array to test the index position in.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void IsValidIndexPosition(int index, Array array, string paramName)
        {
            if (index < 0 || index >= array.Length)
                throw new IndexOutOfRangeException(String.Format(Resources.Strings.Exception_Validation_Assert_Enumerables_IsValidIndexPosition, paramName));
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified array.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified array.</exception>
        /// <param name="index">Index position within the specified array.</param>
        /// <param name="array">Array to test the index position in.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void IsValidIndexPosition(int index, Array array, string paramName, string message)
        {
            if (index < 0 || index >= array.Length)
                throw new IndexOutOfRangeException(string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified array.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified array.</exception>
        /// <param name="index">Index position within the specified array.</param>
        /// <param name="array">Array to test the index position in.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void IsValidIndexPosition(int index, Array array, string message, Exception innerException)
        {
            if (index < 0 || index >= array.Length)
                throw new IndexOutOfRangeException(message, innerException);
        }
        #endregion

        #region ICollection
        /// <summary>
        /// Asserts that index value is a valid position within the specified collection.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="index">Index position within the specified collection.</param>
        /// <param name="collection">Collection to test the index position in.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static void IsValidIndexPosition(int index, ICollection collection, string paramName)
        {
            if (index < 0 || index >= collection.Count)
                throw new IndexOutOfRangeException(String.Format(Resources.Strings.Exception_Validation_Assert_Enumerables_IsValidIndexPosition, paramName));
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified collection.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="index">Index position within the specified collection.</param>
        /// <param name="collection">Collection to test the index position in.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">A message to describe the exception</param>
        public static void IsValidIndexPosition(int index, ICollection collection, string paramName, string message)
        {
            if (index < 0 || index >= collection.Count)
                throw new IndexOutOfRangeException(string.Format(message, paramName));
        }

        /// <summary>
        /// Asserts that index value is a valid position within the specified collection.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Parameter 'index' value must be a valid position within the specified collection.</exception>
        /// <param name="index">Index position within the specified collection.</param>
        /// <param name="collection">Collection to test the index position in.</param>
        /// <param name="message">A message to describe the exception</param>
        /// <param name="innerException">The exception that is the cause of this exception.</param>
        public static void IsValidIndexPosition(int index, ICollection collection, string message, Exception innerException)
        {
            if (index < 0 || index >= collection.Count)
                throw new IndexOutOfRangeException(message, innerException);
        }
        #endregion
    }
}
