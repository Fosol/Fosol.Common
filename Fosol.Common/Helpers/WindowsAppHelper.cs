using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Fosol.Common.Utilities
{
    /// <summary>
    /// WindowsAppUtility provides helpful methods for Windows Store Apps and Windows Phone Apps.
    /// </summary>
    public static class WindowsAppUtility
    {
        #region Methods
        /// <summary>
        /// Forces the current application to shutdown.
        /// When using this method, one should be careful because this method cannot generally be used to provide the “exit” mechanism for the app like the back key and also that the method does not raise any application lifecycle events. 
        /// This method will just terminate thye current process.
        /// </summary>
        public static void Exit()
        {
            Application.Current.Exit();
        }
        #endregion
    }
}
