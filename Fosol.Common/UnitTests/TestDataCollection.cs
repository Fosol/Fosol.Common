using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// TestDataCollection provides a central collection to manage valid and invalid testing data.
    /// </summary>
    public class TestDataCollection
    {
        #region Variables
        private List<TestData> _Pass;
        private List<TestData> _Fail;
        #endregion

        #region Properties
        /// <summary>
        /// get - This collection of test data should contain valid data.
        /// </summary>
        public List<TestData> Pass
        {
            get { return _Pass; }
            private set { _Pass = value; }
        }

        /// <summary>
        /// get - This collection of test data should contain data that will throw exceptions.
        /// </summary>
        public List<TestData> Fail
        {
            get { return _Fail; }
            private set { _Fail = value; }
        }
        #endregion

        #region Constructors
        public TestDataCollection()
        {
            this.Pass = new List<TestData>();
            this.Fail = new List<TestData>();
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
