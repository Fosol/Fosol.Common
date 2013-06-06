using Fosol.Common.Extensions.NameValueCollections;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    /// <summary>
    /// A StringFormatter provides a way to generate an output based on a configured format.
    /// </summary>
    public class StringFormatter
        : IDisposable
    {
        #region Variables
        private static readonly Fosol.Common.Caching.SimpleCache<Keywords.FormatKeyword> _Cache = new Fosol.Common.Caching.SimpleCache<Keywords.FormatKeyword>();
        private readonly string _Format;
        private readonly List<Keywords.FormatKeyword> _Keywords = new List<Keywords.FormatKeyword>();
        private readonly Common.Parsers.SimpleParser _Parser;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a StringFormatter.
        /// By default the keywords are bound by curly-brackets (i.e. {keyword?attribute=value}).
        /// </summary>
        /// <param name="format">String format to use which will generate a result.</param>
        public StringFormatter(string format)
            : this(format, "{", "}")
        {

        }

        /// <summary>
        /// Creates a new instance of a StringFormatter.
        /// </summary>
        /// <param name="format">String format to use which will generate a result.</param>
        /// <param name="startBoundary">Keyword start boundary.</param>
        /// <param name="endBoundary">Keyword end boundary.</param>
        public StringFormatter(string format, string startBoundary, string endBoundary)
        {
            _Format = format;
            _Parser = new Common.Parsers.SimpleParser(startBoundary, endBoundary);

            var keywords = Compile(format);

            foreach (var key in keywords)
            {
                _Keywords.Add(key);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates the dynamic text for this LogFormat.
        /// </summary>
        /// <param name="data">Object containing information for dynamic keywords.</param>
        /// <returns>Dynamicly generated text.</returns>
        public string Render(object data)
        {
            var builder = new StringBuilder();

            foreach (var key in _Keywords)
            {
                var static_key = key as Keywords.FormatStaticKeyword;
                var dynamic_key = key as Keywords.FormatDynamicKeyword;

                if (static_key != null)
                    builder.Append(static_key.Text);
                else if (dynamic_key != null)
                    builder.Append(dynamic_key.Render(data));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Parse the formatted string into a collection of Keywords.
        /// </summary>
        /// <param name="format">Formatting string value.</param>
        /// <returns>Collection of Keywords.</returns>
        private IEnumerable<Keywords.FormatKeyword> Compile(string format)
        {
            return StringFormatter.Compile(_Parser, format);
        }

        /// <summary>
        /// Parse the formatted string into a collection of Keywords.
        /// Pulls keywords from cache if they are already added.
        /// Adds keywords to cache if they haven't been added.
        /// </summary>
        /// <param name="parser">Common.Parsers.SimpleParser object.</param>
        /// <param name="format">Formatting string value.</param>
        /// <returns>Collection of Keywords.</returns>
        private static IEnumerable<Keywords.FormatKeyword> Compile(Common.Parsers.SimpleParser parser, string format)
        {
            var phrases = parser.Parse(format);
            var is_cached = false;

            foreach (var phrase in phrases)
            {
                // Cached version found, return it.
                is_cached = _Cache.ContainsKey(phrase.Text);

                if (is_cached)
                    yield return _Cache.Get(phrase.Text);

                Keywords.FormatKeyword keyword = null;
                var key = phrase as Common.Parsers.Keyword;

                if (key == null)
                    // Return a new instance of the LiteralKeyword which will contain the text value.
                    keyword = new Keywords.TextKeyword(phrase.Text);
                else
                {
                    // Determine the appropriate Keyword to use.
                    var type = Keywords.FormatKeywordLibrary.Get(key.Name);

                    var is_static = typeof(Keywords.FormatStaticKeyword).IsAssignableFrom(type);
                    var is_dynamic = typeof(Keywords.FormatDynamicKeyword).IsAssignableFrom(type);

                    // Return a new instance of the Keyword.
                    if (type != null)
                    {
                        if (is_static)
                        {
                            if (type.GetConstructor(new Type[] { typeof(string), typeof(StringDictionary) }) != null)
                                keyword = (Keywords.FormatStaticKeyword)Activator.CreateInstance(type, phrase.Text, key.Params.ToStringDictionary());
                            else if (type.GetConstructor(new Type[] { typeof(string) }) != null)
                                keyword = (Keywords.FormatStaticKeyword)Activator.CreateInstance(type, phrase.Text);
                            else if (type.GetConstructor(new Type[] { typeof(StringDictionary) }) != null)
                                keyword = (Keywords.FormatStaticKeyword)Activator.CreateInstance(type, key.Params.ToStringDictionary());
                            else if (type.GetConstructor(new Type[0]) != null)
                                keyword = (Keywords.FormatStaticKeyword)Activator.CreateInstance(type);
                        }
                        else if (is_dynamic)
                        {
                            if (type.GetConstructor(new Type[] { typeof(StringDictionary) }) != null)
                                keyword = (Keywords.FormatDynamicKeyword)Activator.CreateInstance(type, key.Params.ToStringDictionary());
                            else if (type.GetConstructor(new Type[0]) != null)
                                keyword = (Keywords.FormatDynamicKeyword)Activator.CreateInstance(type);
                        }
                        // If for some reason they've inherited directly from the FormatKeyword abstract class instead of the normal ones.
                        else
                        {
                            if (type.GetConstructor(new Type[] { typeof(string), typeof(StringDictionary) }) != null)
                                keyword = (Keywords.FormatKeyword)Activator.CreateInstance(type, phrase.Text, key.Params.ToStringDictionary());
                            else if (type.GetConstructor(new Type[] { typeof(string) }) != null)
                                keyword = (Keywords.FormatKeyword)Activator.CreateInstance(type, phrase.Text);
                            else if (type.GetConstructor(new Type[] { typeof(StringDictionary) }) != null)
                                keyword = (Keywords.FormatKeyword)Activator.CreateInstance(type, key.Params.ToStringDictionary());
                            else if (type.GetConstructor(new Type[0]) != null)
                                keyword = (Keywords.FormatKeyword)Activator.CreateInstance(type);
                        }
                    }
                    else
                        throw new System.Configuration.ConfigurationErrorsException(string.Format(Resources.Strings.Exception_FormatKeyword_Does_Not_Exist, key.Name));
                }

                if (!is_cached)
                {
                    _Cache.Add(phrase.Text, keyword);
                    yield return keyword;
                }
            }
        }

        /// <summary>
        /// Returns the format string.
        /// </summary>
        /// <returns>Returns the format string.</returns>
        public override string ToString()
        {
            return _Format;
        }

        /// <summary>
        /// Dispose this LogFormat object.
        /// </summary>
        public void Dispose()
        {
            _Keywords.Clear();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
