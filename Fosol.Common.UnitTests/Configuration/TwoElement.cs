using Fosol.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Configuration
{
    [ConfigurationElement("two")]
    class TwoElement
        : TestSection
    {
        #region Variables
        #endregion

        #region Properties
        [ConfigurationProperty("two", IsKey = true, IsRequired = true)]
        public string Two
        {
            get { return (string)this["two"]; }
            set { this["two"] = value; }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods

        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
