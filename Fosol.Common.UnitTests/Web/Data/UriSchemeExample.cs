using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriSchemeExample
        : Fosol.Common.UnitTests.ValueTestData<string>
    {
        #region Variables
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public UriSchemeExample(string scheme)
            : base(scheme)
        {
        }

        public UriSchemeExample(string scheme, string expectedResult)
            : base(scheme, expectedResult)
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
