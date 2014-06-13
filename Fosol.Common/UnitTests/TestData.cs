using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// TestData is an abstract class that provides a base class for testing data.
    /// Defines whether this particular test should be a failure or a success.
    /// </summary>
    public abstract class TestData
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - This test data should fail if set to 'true'.
        /// </summary>
        public bool ShouldFail { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TestData class.
        /// </summary>
        /// <param name="shouldFail">Whether this test data should fail.</param>
        public TestData(bool shouldFail)
        {
            this.ShouldFail = shouldFail;
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
