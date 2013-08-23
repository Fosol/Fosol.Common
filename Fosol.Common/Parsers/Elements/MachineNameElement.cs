using Fosol.Common.Parsers.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers.Elements
{
    /// <summary>
    /// The MachineNameElement output is the machine name.
    /// </summary>
    [Element("machineName")]
    public sealed class MachineNameElement
        : StaticElement
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new MachineNameElement object.
        /// Initialize with MachineName.
        /// </summary>
        public MachineNameElement()
            : base(Environment.MachineName)
        {
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
