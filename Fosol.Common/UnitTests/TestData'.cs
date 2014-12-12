using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// TestData is a generic abstract class that provides a base class for testing data.
    /// </summary>
    public abstract class TestData<T>
        : ITestData
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The actual value of the test data.
        /// </summary>
        public T Value { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TestData class.
        /// </summary>
        /// <param name="value">Value that will be used for testing.</param>
        public TestData(T value)
        {
            this.Value = value;
        }
        #endregion

        #region Methods

        #endregion

        #region Operators
        /// <summary>
        /// Returns the TestData.Value property.
        /// </summary>
        /// <param name="data">TestData object of type T.</param>
        /// <returns>TestData.Value property.</returns>
        public static implicit operator T(TestData<T> data)
        {
            return data.Value;
        }
        #endregion

        #region Events
        #endregion
    }
}
