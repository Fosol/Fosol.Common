using Fosol.Common.Extensions.NameValueCollections;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    public sealed class KeywordFormat
        : Format
    {
        #region Variables
        private readonly List<Keywords.FormatKeyword> _Keywords;
        #endregion

        #region Properties

        public List<Keywords.FormatKeyword> Keywords
        {
            get { return _Keywords; }
        }
        #endregion

        #region Constructors
        public KeywordFormat(string text)
            : base(new KeywordFormatter(), text)
        {
        }

        public KeywordFormat(KeywordFormatter formatter, string text)
            : base(formatter, text)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compile is automatically executed in the base constructor.
        /// Parses the FormatPart collection and creates a collection of FormatKeywords.
        /// </summary>
        protected override void Compile()
        {
            foreach (var part in this.Parts)
            {
                this.Keywords.Add(CreateKeyword(part));
            }
        }

        /// <summary>
        /// Create the FormatKeyword for the specified formatPart.
        /// </summary>
        /// <param name="part">A FormatPart which represents a FormatKeyword.</param>
        /// <returns>New instance of a FormatKeyword.</returns>
        private Formatters.Keywords.FormatKeyword CreateKeyword(FormatPart part)
        {
            string name;
            NameValueCollection query = new NameValueCollection();
            var attribute_pos = this.Formatter.AttributeBoundary.IndexOfBoundaryIn(part.Value, this.Formatter.StartBoundary.Boundary.Length - 1);
            var is_param = part.Value.StartsWith("@");

            // If there are no attributes, the whole part.Value becomes the name.
            if (attribute_pos == -1)
            {
                // It's using the parameter shortcut syntax.
                if (is_param)
                {
                    name = "parameter";
                    var pos = part.Value.IndexOf("=");

                    // Check for shortcut syntax within (i.e. {@param=value}).
                    if (pos != -1)
                    {
                        query.Add("value", part.Value.Substring(pos + 1));
                    }

                    query.Add("name", part.Value);
                }
                else
                    name = part.Value;
            }
                // If there are attributes, extract them and the name.
            else
            {
                query = System.Web.HttpUtility.ParseQueryString(part.Value.Substring(attribute_pos + 1));

                // It's using the parameter shortcut syntax.
                if (is_param)
                {
                    name = "parameter";
                    query.Add("name", part.Value.Substring(0, attribute_pos - 1));
                }
                else
                {
                    name = part.Value.Substring(0, attribute_pos - 1);
                }
            }

            // If there is a FormatKeyword with the specified name initialize it.
            if (Formatters.Keywords.FormatKeywordLibrary.ContainsKey(name))
            {
                var type = Formatters.Keywords.FormatKeywordLibrary.Get(name);
                var is_static = typeof(Fosol.Common.Formatters.Keywords.FormatStaticKeyword).IsAssignableFrom(type);
                var is_dynamic = typeof(Fosol.Common.Formatters.Keywords.FormatDynamicKeyword).IsAssignableFrom(type);

                // Return a new instance of the Keyword.
                if (type != null)
                {
                    if (is_static)
                    {
                        if (type.GetConstructor(new Type[] { typeof(StringDictionary) }) != null)
                            return (Fosol.Common.Formatters.Keywords.FormatStaticKeyword)Activator.CreateInstance(type, query.ToStringDictionary());
                        else if (type.GetConstructor(new Type[0]) != null)
                            return (Fosol.Common.Formatters.Keywords.FormatStaticKeyword)Activator.CreateInstance(type);
                    }
                    else if (is_dynamic)
                    {
                        if (type.GetConstructor(new Type[] { typeof(StringDictionary) }) != null)
                            return (Fosol.Common.Formatters.Keywords.FormatDynamicKeyword)Activator.CreateInstance(type, query.ToStringDictionary());
                        else if (type.GetConstructor(new Type[0]) != null)
                            return (Fosol.Common.Formatters.Keywords.FormatDynamicKeyword)Activator.CreateInstance(type);
                    }
                }
            }

            // There was no FormatKeyword with the specified name, simply generate a static TextKeyword.
            return new Formatters.Keywords.TextKeyword(part.Value);
        }

        /// <summary>
        /// Renders the collection of FormatKeywords into a dynamically generated string.
        /// </summary>
        /// <param name="data">Information to be used when rendering the output string.</param>
        /// <returns>The dynamically generated output string value.</returns>
        public override string Render(object data)
        {
            var builder = new StringBuilder();

            foreach (var key in _Keywords)
            {
                var static_key = key as Fosol.Common.Formatters.Keywords.FormatStaticKeyword;
                var dynamic_key = key as Fosol.Common.Formatters.Keywords.FormatDynamicKeyword;

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
