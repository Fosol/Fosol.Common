using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// CompareTestData class provides a generic way to test a value and confirm it is equal to the expected result.
    /// </summary>
    /// <typeparam name="T">Type of value to test.</typeparam>
    public class CompareTestData<T>
        : TestData<T>
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The expected result after the test is run.
        /// </summary>
        public T ExpectedValue { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ValueTestData class.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public CompareTestData(T value)
            : this(value, value)
        {
        }

        /// <summary>
        /// Creates a new instance of a ValueTestData class.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="expectedValue">The expected result after the test is run.</param>
        public CompareTestData(T value, T expectedValue)
            : base(value)
        {
            this.ExpectedValue = expectedValue;
        }
        #endregion

        #region Methods
        public bool AreEqual(Func<T, T> func)
        {
            return func(this.Value).Equals(this.ExpectedValue);
        }

        public bool AreEqual(T value)
        {
            return value.Equals(this.ExpectedValue);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
