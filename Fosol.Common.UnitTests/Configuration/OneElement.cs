using Fosol.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Configuration
{
    [ConfigurationElement("one")]
    class OneElement
        : TestSection
    {
        #region Variables
        #endregion

        #region Properties
        [ConfigurationProperty("one", IsKey = true, IsRequired = true)]
        public string One
        {
            get { return (string)this["one"]; }
            set { this["one"] = value; }
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
