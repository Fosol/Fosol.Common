using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Faults
{
    [DataContract(Name = "ParameterInvalidCastFault", Namespace = "http://team.fosol.ca")]
    public sealed class ParameterInvalidCastFault
        : ParameterFault
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ParameterInvalidCastFault(string paramName, string message)
            : base(paramName, message)
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
