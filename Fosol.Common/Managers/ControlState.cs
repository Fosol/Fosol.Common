using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Managers
{
    /// <summary>
    /// ControlState enum provides a way to determine whether the state information has changed or not.
    /// </summary>
    public enum ControlState
    {
        /// <summary>
        /// Unaltered means the state information has not changed; it is has been saved.
        /// </summary>
        Unaltered,
        /// <summary>
        /// Altered means the state information has changed and should be saved.
        /// </summary>
        Altered
    }
}
