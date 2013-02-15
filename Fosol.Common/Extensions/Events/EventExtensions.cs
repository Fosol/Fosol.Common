using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Events
{
    /// <summary>
    /// Extension methods for Events.
    /// </summary>
    public static class EventExtensions
    {
        #region Methods
        /// <summary>
        /// Provides a nice syntax for raising events.
        /// This method makes sure the event exists before raising.
        /// This method makes sure the event has not been unregistered in another thread before raising.
        /// </summary>
        /// <typeparam name="T">Type of event arguments.</typeparam>
        /// <param name="handler">EventHandler of type T.</param>
        /// <param name="sender">Object sending the event.</param>
        /// <param name="args">Event arguments to be sent with the event.</param>
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            if (handler != null) handler(sender, args);
        }
        #endregion
    }
}
