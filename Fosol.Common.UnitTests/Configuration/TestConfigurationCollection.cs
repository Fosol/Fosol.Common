using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fosol.Common.UnitTests.Configuration
{
    [ConfigurationCollection(typeof(ConfigurationElement), AddItemName = "one, two")]
    class TestConfigurationCollection
        : Fosol.Common.Configuration.SpecialConfigurationElementCollection<SpecialConfigurationElementTypes>
    {
        #region Variables
        #endregion

        #region Properties
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
