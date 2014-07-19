using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Configuration
{
    class TestSection
        : ConfigurationSection
    {
        #region Variables
        #endregion

        #region Properties
        [ConfigurationProperty("items", IsDefaultCollection = true, IsRequired = true)]
        public TestConfigurationCollection Items
        {
            get { return (TestConfigurationCollection)this["items"]; }
            set { this["items"] = value; }
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
