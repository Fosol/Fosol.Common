using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// Format is a collection of FormatElement objects that are used to create a dynamically rendered text.
    /// </summary>
    public class Format 
        : IFormat<FormatElement>
    {
        #region Variables
        private readonly List<FormatElement> _Elements;
        #endregion

        #region Properties
        /// <summary>
        /// get - Collection of FormatElement objects.
        /// </summary>
        public List<FormatElement> Elements { get { return _Elements; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a FormatElement object.
        /// </summary>
        public Format()
        {
            _Elements = new List<FormatElement>();
        }

        /// <summary>
        /// Creates a new instance of a FormatElement object.
        /// </summary>
        internal Format(List<FormatElement> elements)
        {
            Validation.Assert.IsNotNull(elements, "elements");
            _Elements = elements;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the collection of Elements into a dynamically generated string.
        /// </summary>
        /// <param name="data">Information to be used when rendering the output string.</param>
        /// <returns>The dynamically generated output string value.</returns>
        public virtual string Render(object data)
        {
            var builder = new StringBuilder();

            foreach (var key in this.Elements)
            {
                var static_key = key as Fosol.Common.Parsers.StaticElement;
                var dynamic_key = key as Fosol.Common.Parsers.DynamicElement;

                if (static_key != null)
                    builder.Append(static_key.Text);
                else if (dynamic_key != null)
                    builder.Append(dynamic_key.Render(data));
            }

            return builder.ToString();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
