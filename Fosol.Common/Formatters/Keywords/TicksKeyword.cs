using Fosol.Common.Formatters.Keywords;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// The Ticks value of the current date and time.
    /// </summary>
    [FormatKeyword("ticks")]
    public sealed class TicksKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TicksKeyword object.
        /// </summary>
        public TicksKeyword()
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
