using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests.Configuration
{
    class SpecialConfigurationElementTypes
        : Fosol.Common.Configuration.SpecialConfigurationElementType
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public SpecialConfigurationElementTypes()
            : base(typeof(OneElement), typeof(TwoElement))
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
