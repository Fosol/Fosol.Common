using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriUserInfoExample
        : Fosol.Common.UnitTests.CompareTestData<string>
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

        public UriUserInfoExample(string userInfo, string ExpectedValue)
            : base(userInfo, ExpectedValue)
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
