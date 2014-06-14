using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriExample
        : Fosol.Common.UnitTests.ValueTestData<string>
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
        public UriExample(string uri)
            : base(uri)
        {
        }

        public UriExample(string uri, string expectedResult)
            : base(uri, expectedResult)
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
