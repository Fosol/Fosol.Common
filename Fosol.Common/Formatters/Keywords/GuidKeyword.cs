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
    /// Renders a guid into the message.
    /// </summary>
    [FormatKeyword("guid")]
    public sealed class GuidKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The format string for the Guid.
        /// </summary>
        [DefaultValue("N")]
        [FormatKeywordProperty("format", new string[] { "f" })]
        public string Format { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a GuidKeyword.
        /// </summary>
        /// <param name="attribute">StringDictionary object.</param>
        public GuidKeyword(StringDictionary attributes)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a Guid id value.
        /// </summary>
        /// <param name="data">Object data to be used by the dynamic keyword.</param>
        /// <returns>Guid id value.</returns>
        public override string Render(object data)
        {
            return Guid.NewGuid().ToString(this.Format);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
