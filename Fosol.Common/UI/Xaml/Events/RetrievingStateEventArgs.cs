using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Fosol.Common.UI.Xaml.Events
{
    /// <summary>
    /// RetrievingStateEventArgs class provides a way to pass information to the methods and event handlers that are involved in loading and deserializing the saved state file.
    /// </summary>
    public sealed class RetrievingStateEventArgs
        : EventArgs
    {
        #region Variables
        private ApplicationExecutionState _PreviousExecutionState;
        private bool _HasState;
        private Exception _Exception;
        #endregion

        #region Properties
        /// <summary>
        /// get - The previous execution state value.
        /// </summary>
        public ApplicationExecutionState PreviousExecutionState
        {
            get { return _PreviousExecutionState; }
            private set { _PreviousExecutionState = value; }
        }

        /// <summary>
        /// get - Whether the saved state file exists and was deserialized successfully.
        /// </summary>
        public bool HasState
        {
            get { return _HasState; }
            internal set { _HasState = value; }
        }

        /// <summary>
        /// get - If an exception occurs while attempting to load and deserialize the saved state file it will be stored here.
        /// </summary>
        public Exception Exception
        {
            get { return _Exception; }
            internal set { _Exception = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a RetrievingStateEventArgs class.
        /// </summary>
        /// <param name="previousExecutionState">ApplicationExecutionState object describing the previous execution state.</param>
        public RetrievingStateEventArgs(ApplicationExecutionState previousExecutionState)
        {
            this.PreviousExecutionState = previousExecutionState;
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
