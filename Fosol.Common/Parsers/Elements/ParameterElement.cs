using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers.Elements
{
    /// <summary>
    /// A ParameterElement provides a dynamic way to apply static parameter names while including attributes for further logic.
    /// It is useful for database parameters (i.e. {parameter?name=@Id&SqlDbType=NVarChar}).
    /// You can use the shortcut syntax too (i.e. {@Id?value={message}}} or {@Id={message}}}.
    /// Note that the Value property/attribute can contain keywords, just be sure to escape the end boundary if it is next to the parameter keyword end boundary.
    /// </summary>
    [Element("parameter")]
    public sealed class ParameterElement
        : DynamicElement
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The parameter name.
        /// </summary>
        [ElementProperty("name", true)]
        public string ParameterName { get; set; }

        /// <summary>
        /// get/set - The parameter value.
        /// </summary>
        [ElementProperty("value", true, typeof(Converters.ElementFormatConverter))]
        public Format Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ParameterElement.
        /// </summary>
        /// <param name="attributes">StringDictionary containing attribute values.</param>
        public ParameterElement(StringDictionary attributes)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Dynamically generate the value this keyword makes.
        /// </summary>
        /// <param name="data">Data to be used in the result.</param>
        /// <returns>String value.</returns>
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
