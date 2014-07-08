using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Fosol.Common.UI.Xaml.Events
{
    /// <summary>
    /// RestoringStateEventArgs class 
    /// </summary>
    public sealed class RestoringStateEventArgs
        : EventArgs
    {
        #region Variables
        private ApplicationExecutionState _PreviousExecutionState;
        private bool _HasState;
        private bool _Cancel;
        #endregion

        #region Properties
        /// <summary>
        /// get - Previous execution state value.
        /// </summary>
        public ApplicationExecutionState PreviousExecutionState
        {
            get { return _PreviousExecutionState; }
            private set { _PreviousExecutionState = value; }
        }

        /// <summary>
        /// get - Whether there is currently any state information available.
        /// </summary>
        public bool HasState
        {
            get { return _HasState; }
            internal set { _HasState = value; }
        }

        /// <summary>
        /// get/set - Whether to cancel restoring state.
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a RestoringStateEventArgs class.
        /// </summary>
        /// <param name="previousExecutionState">ApplicationExecutionState object.</param>
        public RestoringStateEventArgs(ApplicationExecutionState previousExecutionState)
        {
            this.PreviousExecutionState = previousExecutionState;
        }

        /// <summary>
        /// Creates a new instance of a RestoringStateEventArgs class.
        /// </summary>
        /// <param name="retrievingStateEventArgs">RetrievingStateEventArgs object</param>
        internal RestoringStateEventArgs(RetrievingStateEventArgs retrievingStateEventArgs)
        {
            this.PreviousExecutionState = retrievingStateEventArgs.PreviousExecutionState;
            this.HasState = retrievingStateEventArgs.HasState;
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
