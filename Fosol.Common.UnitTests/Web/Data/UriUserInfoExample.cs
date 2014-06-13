using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriUserInfoExample
        : Fosol.Common.UnitTests.ValueTestData<string>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public UriUserInfoExample(string userInfo)
            : base(userInfo)
        {
        }

        public UriUserInfoExample(string userInfo, string expectedResult)
            : base(userInfo, expectedResult)
        {
        }

        public UriUserInfoExample(bool shouldFail, string userInfo)
            : base(shouldFail, userInfo)
        {
        }

        public UriUserInfoExample(bool shouldFail, string userInfo, string expectedResult)
            : base(shouldFail, userInfo, expectedResult)
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
