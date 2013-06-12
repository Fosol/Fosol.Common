using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// A ParameterKeyword provides a dynamic way to apply static parameter names while including attributes for further logic.
    /// It is useful for database parameters (i.e. {parameter?name=@Id&SqlDbType=NVarChar}).
    /// </summary>
    [FormatKeyword("parameter")]
    public sealed class ParameterKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        [FormatKeywordProperty("name", true)]
        public string ParameterName { get; set; }

        [FormatKeywordProperty("value", true, typeof(Converters.StringFormatterConverter))]
        public Formatters.StringFormatter Value { get; set; }
        #endregion

        #region Constructors
        public ParameterKeyword(StringDictionary attributes)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        public override string Render(object data)
        {
            return this.ParameterName;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
