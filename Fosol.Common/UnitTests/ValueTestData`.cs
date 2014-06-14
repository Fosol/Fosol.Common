using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// ValueTestData class provides a generic way to test a value and confirm it is equal to the expected result.
    /// </summary>
    /// <typeparam name="T">Type of value to test.</typeparam>
    public class ValueTestData<T>
        : TestData
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The value to test.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// get/set - The expected result after the test is run.
        /// </summary>
        public T ExpectedResult { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ValueTestData class.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public ValueTestData(T value)
            : this(value, value)
        {
        }

        /// <summary>
        /// Creates a new instance of a ValueTestData class.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="expectedResult">The expected result after the test is run.</param>
        public ValueTestData(T value, T expectedResult)
        {
            this.Value = value;
            this.ExpectedResult = expectedResult;
        }
        #endregion

        #region Methods

        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
