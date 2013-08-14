using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// The DateTimeKeyword dynamically generates a string to represent the current DateTime.
    /// </summary>
    [FormatKeyword("datetime")]
    public class DateTimeKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The format string for the DateTime value.
        /// </summary>
        [DefaultValue("G")]
        [FormatKeywordProperty("format", new string[] { "f" })]
        public string Format { get; set; }

        /// <summary>
        /// get/set - Whether to display the ticks value instead of the DateTime.
        /// </summary>
        [FormatKeywordProperty("ticks")]
        public bool Ticks { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a DateTimeKeyword object.
        /// </summary>
        /// <param name="attributes">StringDictionary object.</param>
        public DateTimeKeyword(StringDictionary attributes = null)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Render the DateTime value.
        /// </summary>
        /// <param name="data">Object contain information for dynamic keywords.</param>
        /// <returns>DateTime value as a string with the configured format.</returns>
        public override string Render(object data)
        {
            if (this.Ticks)
                return Optimization.FastDateTime.Now.Ticks.ToString(this.Format);
            else
                return Optimization.FastDateTime.Now.ToString(this.Format);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
