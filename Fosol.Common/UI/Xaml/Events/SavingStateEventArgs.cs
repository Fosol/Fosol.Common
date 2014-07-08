using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UI.Xaml.Events
{
    /// <summary>
    /// SavingStateEventArgs class.
    /// </summary>
    public sealed class SavingStateEventArgs
        : EventArgs
    {
        #region Variables
        private object _Data;
        private bool _Cancel;
        #endregion

        #region Properties
        /// <summary>
        /// get - Data information included in the event.
        /// </summary>
        public object Data
        {
            get { return _Data; }
            private set { _Data = value; }
        }

        /// <summary>
        /// get/set - Cancel serializing and saving the state file.
        /// </summary>
        public bool Cancel
        {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SavingStateEventArgs class.
        /// </summary>
        /// <param name="data">Any data you want to include in the event.</param>
        public SavingStateEventArgs(object data = null)
        {
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
