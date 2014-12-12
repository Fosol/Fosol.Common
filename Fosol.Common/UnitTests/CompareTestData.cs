using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// CompareTestData class provides a way to test a value and confirm it is equal to the expected result.
    /// </summary>
    public class CompareTestData
        : TestData<object>
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The expected result after the test is run.
        /// </summary>
        public object ExpectedValue { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ValueTestData class.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public CompareTestData(object value)
            : this(value, value)
        {
        }

        /// <summary>
        /// Creates a new instance of a ValueTestData class.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="expectedValue">The expected result after the test is run.</param>
        public CompareTestData(object value, object expectedValue)
            : base(value)
        {
            this.ExpectedValue = expectedValue;
        }
        #endregion

        #region Methods
        public bool AreEqual(Func<object, object> func)
        {
            return func(this.Value).Equals(this.ExpectedValue);
        }

        public bool AreEqual(object value)
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
