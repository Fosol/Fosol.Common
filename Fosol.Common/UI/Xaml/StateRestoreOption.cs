using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UI.Xaml
{
    /// <summary>
    /// StateRestoreOption enum provides a way to control when state will be restored based on the ApplicationExecutionState value.
    /// </summary>
    [Flags]
    public enum StateRestoreOption
    {
        /// <summary>
        /// NotRunning - The app is not running.
        /// </summary>
        NotRunning = 1,
        /// <summary>
        /// Running - The app is running.
        /// </summary>
        Running = 2,
        /// <summary>
        /// Suspended - The app is suspended.
        /// </summary>
        Suspended = 4,
        /// <summary>
        /// Terminated - The app was terminated after being suspended.
        /// </summary>
        Terminated = 8,
        /// <summary>
        /// ClosedByUser - The app was closed by the user.
        /// </summary>
        ClosedByUser = 16
    }
}
