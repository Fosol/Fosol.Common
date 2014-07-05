using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Fosol.Common.UI.Xaml.Events
{
    public sealed class RetrievingStateEventArgs
        : EventArgs
    {
        #region Variables
        private object _Data;
        private ApplicationExecutionState _PreviousExecutionState;
        #endregion

        #region Properties
        public ApplicationExecutionState PreviousExecutionState
        {
            get { return _PreviousExecutionState; }
            private set { _PreviousExecutionState = value; }
        }

        public object Data
        {
            get { return _Data; }
            private set { _Data = value; }
        }
        #endregion

        #region Constructors
        public RetrievingStateEventArgs(ApplicationExecutionState previousExecutionState, object data = null)
        {
            this.PreviousExecutionState = previousExecutionState;
            this.Data = data;
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
