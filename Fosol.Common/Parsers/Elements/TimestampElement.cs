using Fosol.Common.Parsers.Elements;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers.Elements
{
    /// <summary>
    /// The Timestamp from the trace event.
    /// </summary>
    [Element("timestamp")]
    public sealed class TimestampElement
        : DynamicElement
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a StackFormatElement object.
        /// </summary>
        /// <param name="attributes">Attributes to include with this keyword.</param>
        public TimestampElement(StringDictionary attributes = null)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the timestamp of the TraceEvent.
        /// </summary>
        /// <param name="data">Information object containing data for the keyword.</param>
        /// <returns>The trace event call stack.</returns>
        public override string Render(object data)
        {
            return DateTime.Now.Ticks.ToString();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
