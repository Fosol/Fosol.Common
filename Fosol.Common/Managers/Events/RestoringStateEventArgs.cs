using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Fosol.Common.Managers.Events
{
    public sealed class RestoringStateEventArgs
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
        public RestoringStateEventArgs(ApplicationExecutionState previousExecutionState, object data = null)
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
