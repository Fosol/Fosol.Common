using Fosol.Common.Parsers.Elements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers.Elements
{
    /// <summary>
    /// The Ticks value of the current date and time.
    /// </summary>
    [Element("ticks")]
    public sealed class TicksElement
        : DynamicElement
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TicksElement object.
        /// </summary>
        public TicksElement()
            : base()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// The Ticks value of the current date and time.
        /// </summary>
        /// <param name="data">Information object containing data for the keyword.</param>
        /// <returns>The Ticks value of the current date and time.</returns>
        public override string Render(object data)
        {
            return DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
