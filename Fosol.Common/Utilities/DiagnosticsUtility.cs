using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Utilities
{
    /// <summary>
    /// Diagnostic utility methods.
    /// </summary>
    public class DiagnosticsUtility
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Watch how long it takes to execute the specified action.
        /// </summary>
        /// <param name="action">Action to watch execute.</param>
        /// <returns>Length of time to execute the specified action.</returns>
        public static TimeSpan Watch(Action action)
        {
            var stop_watch = Stopwatch.StartNew();
            action();
            stop_watch.Stop();
            return stop_watch.Elapsed;
        }
        #endregion

        #region Events
        #endregion
    }
}
