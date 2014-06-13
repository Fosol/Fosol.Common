using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriAuthorityExample
        : Fosol.Common.UnitTests.ValueTestData<string>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public UriAuthorityExample(string authority)
            : base(authority)
        {
        }

        public UriAuthorityExample(string authority, string expectedResult)
            : base(authority, expectedResult)
        {

        }

        public UriAuthorityExample(bool shouldFail, string authority)
            : base(shouldFail, authority)
        {
        }

        public UriAuthorityExample(bool shouldFail, string authority, string expectedResult)
            : base(shouldFail, authority, expectedResult)
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
