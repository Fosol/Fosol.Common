using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Faults
{
    /// <summary>
    /// ParameterOutOfRangeFault provides a way to describe an exception that occurs when an invalid value is specifed (which is out of range).
    /// </summary>
    [DataContract(Name = "ParameterOutOfRangeFault", Namespace = "http://team.fosol.ca")]
    public sealed class ParameterOutOfRangeFault
        : ParameterFault
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ParameterOutOfRangeFault object.
        /// </summary>
        /// <param name="paramterName">Name of the parameter that this fault is related to.</param>
        /// <param name="message">Description of the fault.</param>
        public ParameterOutOfRangeFault(string parameterName, string message)
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
