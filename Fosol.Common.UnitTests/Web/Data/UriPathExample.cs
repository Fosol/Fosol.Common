using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriPathExample
        : Fosol.Common.UnitTests.ValueTestData<string>
    {
        #region Variables
        #endregion

        #region Properties
        public int NumberOfSegments { get; set; }
        #endregion

        #region Constructors
        public UriPathExample(string path)
            : this(false, path, path)
        {
        }

        public UriPathExample(string path, string expectedResult)
            : this(false, path, expectedResult)
        {
        }

        public UriPathExample(bool shouldFail, string path)
            : this(shouldFail, path, path)
        {
        }

        public UriPathExample(bool shouldFail, string path, string expectedResult)
            : base(shouldFail, path, expectedResult)
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
