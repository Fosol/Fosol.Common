using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriQueryExample
        : Fosol.Common.UnitTests.CompareTestData<string>
    {
        #region Variables
        private KeyValuePair<string, string[]>[] _QueryParameters = new KeyValuePair<string, string[]>[0];
        #endregion

        #region Properties
        public KeyValuePair<string, string[]>[] QueryParameters
        {
            get { return _QueryParameters; }
            set { _QueryParameters = value; }
        }
        #endregion

        #region Constructors
        public UriQueryExample(string query)
            : base(query)
        {
        }

        public UriQueryExample(string query, string ExpectedValue)
            : base(query, ExpectedValue)
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
