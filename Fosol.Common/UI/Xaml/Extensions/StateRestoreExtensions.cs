using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Fosol.Common.UI.Xaml.Extensions
{
    /// <summary>
    /// StateRestoreExtensions provides a way to convert ApplicationExecutionState into StateRestoreOption.
    /// </summary>
    public static class StateRestoreExtensions
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Convert the ApplicationExecutionState value into a StateRestoreOption.
        /// </summary>
        /// <param name="previousExecutionState">ApplicationExecutionState value.</param>
        /// <returns>StateRestoreOption value.</returns>
        public static StateRestoreOption Convert(this ApplicationExecutionState previousExecutionState)
        {
            switch (previousExecutionState)
            {
                case (ApplicationExecutionState.NotRunning):
                    return StateRestoreOption.NotRunning;
                case (ApplicationExecutionState.Running):
                    return StateRestoreOption.Running;
                case (ApplicationExecutionState.Suspended):
                    return StateRestoreOption.Suspended;
                case (ApplicationExecutionState.Terminated):
                    return StateRestoreOption.Terminated;
                case (ApplicationExecutionState.ClosedByUser):
                default:
                    return StateRestoreOption.ClosedByUser;
            }
        }

        /// <summary>
        /// Determines whether the specified ApplicationExecutionState is within the specified StateRestoreOption value.
        /// </summary>
        /// <param name="option">StateRestoreOption value.</param>
        /// <param name="previousExecutionState">ApplicationExecutionState value.</param>
        /// <returns>True if the StateRestoreOption contains the specified ApplicationExecutionState value.</returns>
        public static bool HasFlag(this StateRestoreOption option, ApplicationExecutionState previousExecutionState)
        {
            var pes = previousExecutionState.Convert();
            if (option.HasFlag(pes))
                return true;

            return false;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
