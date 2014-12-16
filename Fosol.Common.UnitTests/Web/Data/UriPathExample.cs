using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriPathExample
        : Fosol.Common.UnitTests.CompareTestData<string>
    {
        #region Variables
        #endregion

        #region Properties
        public int NumberOfSegments { get; set; }
        #endregion

        #region Constructors
        public UriPathExample(string path)
            : base(path)
        {
        }

        public UriPathExample(string path, string ExpectedValue)
            : base(path, ExpectedValue)
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
