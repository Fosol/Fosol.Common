using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriFragmentExample
        : Fosol.Common.UnitTests.ValueTestData<string>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public UriFragmentExample(string fragment)
            : base(fragment)
        {
        }

        public UriFragmentExample(string fragment, string expectedResult)
            : base(fragment, expectedResult)
        {

        }

        public UriFragmentExample(bool shouldFail, string fragment)
            : base(shouldFail, fragment)
        {
        }

        public UriFragmentExample(bool shouldFail, string fragment, string expectedResult)
            : base(shouldFail, fragment, expectedResult)
        {
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
