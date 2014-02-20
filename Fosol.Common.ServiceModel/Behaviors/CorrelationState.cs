using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Behaviors
{
    /// <summary>
    /// Represents information that will be passed from the AfterReceiveRequest event to the BeforeSendReply event.
    /// </summary>
    public sealed class CorrelationState
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - Property values for this CorrelationState.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of a CorrelationState class.
        /// </summary>
        public CorrelationState()
        {
            this.Properties = new Dictionary<string, object>();
        }
        #endregion

        #region Methods
        #endregion

        #region Events
        #endregion
    }
}
