using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriSchemeExample
        : Fosol.Common.UnitTests.CompareTestData<string>
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

        public UriSchemeExample(string scheme, string ExpectedValue)
            : base(scheme, ExpectedValue)
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
