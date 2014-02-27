using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Faults
{
    [DataContract(Name = "ParameterNullFault", Namespace = "http://team.fosol.ca")]
    public sealed class ParameterNullFault
        : ParameterFault
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ParameterNullFault(string parameterName, string message)
            : base(parameterName, message)
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
