using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// A number of safe native methods.
    /// </summary>
    public static class SafeNativeMethods
    {
        #region Methods
        /// <summary>
        /// Get the computer name the processes is currently being executed on.
        /// </summary>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
        public static extern bool GetComputerName(StringBuilder lpBuffer, int[] nSize);

        /// <summary>
        /// Writes message to local machines debug listener.
        /// </summary>
        /// <param name="message"></param>
        [DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);
        #endregion
    }
}
