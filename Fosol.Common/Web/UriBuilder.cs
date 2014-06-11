using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Fosol.Common.Web
{
    /// <summary>
    /// UriBuilder provides a way to parse URI values into logical parts.
    /// - The reason for this class is to provide a clean and simple way to deal with URI values.
    /// - The current .NET classes and methods do not provide all the desired functionality.
    /// </summary>
    public sealed class UriBuilder
    {
        #region Variables
        public const string ReservedGenDelims = @"[:/\?#\[@\]]";
        public const string ReservedSubDelims = @"[!\$&'\(\)\*\+,;=]";
        public const string UnreservedCharacters = @"(?i)[a-z0-9-\._~]";
        public const string ValidSchemeRegex = @"(?i)^[a-z]([a-z0-9\+\-\.])*\Z";
        public const string ValidUserInfoRegex = UnreservedCharacters + ReservedSubDelims;
        public const int MaximumURILength = 2048;
        private string _Scheme;
        private string _Host;
        private int _Port;
        private string _Path;
        private QueryParamCollection _Query;
        private string _Fragment;

        private static readonly Regex _SchemeRegex = new Regex(UriBuilder.ValidSchemeRegex, RegexOptions.Compiled);
        #endregion

        #region Properties
        public string Scheme
        {
            get { return _Scheme; }
            set
            {
                var match = _SchemeRegex.Match(value);

                if (!match.Success)
                    throw new UriFormatException("Scheme value is not valid.");

                _Scheme = value;
            }
        }

        public string Host
        {
            get { return _Host; }
            set
            {

            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a UriBuilder class.
        /// </summary>
        /// <param name="uri">Initial URI value.</param>
        public UriBuilder(string uri)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(uri, "uri");
            var temp_uri = new Uri(uri, UriKind.RelativeOrAbsolute);
            if (temp_uri.IsAbsoluteUri)
            {
                this.Initialize(temp_uri);
                return;
            }
            this.Initialize(new Uri(Uri.UriSchemeHttp + Uri.SchemeDelimiter + uri));
        }

        /// <summary>
        /// Creates a new instance of a UriBuilder class.
        /// </summary>
        /// <param name="uri">Initial URI value.</param>
        public UriBuilder(Uri uri)
        {
            this.Initialize(uri);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the UriBuilder with the URI specified.
        /// </summary>
        /// <param name="uri">Initial URI value.</param>
        private void Initialize(Uri uri)
        {
            _Scheme = uri.Scheme;
            _Host = uri.Host;
            _Port = uri.Port;
            _Path = uri.AbsolutePath;
            _Fragment = uri.Fragment;

            if (uri.Query != null)
                _Query = new QueryParamCollection(uri.Query);
            else
                _Query = new QueryParamCollection();
        }

        /// <summary>
        /// Returns the URI as a string.
        /// </summary>
        /// <returns>A new URI value.</returns>
        public override string ToString()
        {
            return string.Concat(new string[] 
            {
                _Scheme,
                "://",
                _Host,
                (_Port != 80 ? ":" + _Port : String.Empty),
                (_Host.Length > 0 && _Path.Length != 0 && _Path[0] != '/') ? "/" : String.Empty,
                _Path,
                _Query.ToString(),
                _Fragment
            });
        }

        /// <summary>
        /// Parse the specified query string into a QueryParamCollection object.
        /// </summary>
        /// <param name="queryString">Raw query string value.</param>
        /// <param name="decode">Whether the key value pairs should be decoded.</param>
        /// <returns>A new instance of a QueryParamCollection object.</returns>
        public static QueryParamCollection ParseQueryString(string queryString, bool decode = true)
        {
            return new QueryParamCollection(queryString, decode);
        }

        /// <summary>
        /// Parse the specified query string into a NameValueCollection object.
        /// </summary>
        /// <param name="queryString">Raw query string value.</param>
        /// <param name="decode">Whether the key value pairs should be decoded.</param>
        /// <returns>A new instance of a NameValueCollection object.</returns>
        public static NameValueCollection ParseQueryStringToNameValueCollection(string queryString, bool decode = true)
        {
            var result = new NameValueCollection();
            var values = UriBuilder.ParseQueryStringToKeyValuePair(queryString, decode);

            foreach (var value in values)
            {
                result.Add(value.Key, value.Value);
            }

            return result;
        }

        /// <summary>
        /// Parse the specified query string into a collection of KeyValuePair objects.
        /// </summary>
        /// <param name="queryString">Raw query string value.</param>
        /// <param name="decode">Whether the key value pairs should be decoded.</param>
        /// <returns>A new instance of a Collection of KeyValuePair objects.</returns>
        public static List<KeyValuePair<string, string>> ParseQueryStringToKeyValuePair(string queryString, bool decode = true)
        {
            var keys = new List<KeyValuePair<string, string>>();

            if (String.IsNullOrEmpty(queryString))
                return keys;

            var index_of_q = queryString.IndexOf('?');

            if (index_of_q > -1)
            {
                queryString = queryString.Substring(index_of_q + 1);
            }

            if (String.IsNullOrEmpty(queryString))
                return keys;

            var k_start = 0;
            for (var i = 0; i < queryString.Length; i++)
            {
                if (queryString[i] == '&')
                {
                    keys.Add(UriBuilder.ParseKeyValuePair(queryString.Substring(k_start, i - k_start), decode));

                    i++;
                    k_start = i;
                }
            }

            // There was only one key value pair.
            if (k_start == 0
                && keys.Count == 0)
                keys.Add(UriBuilder.ParseKeyValuePair(queryString, decode));
            else if (k_start > 0)
                // This is the last key value pair in the query string.
                keys.Add(UriBuilder.ParseKeyValuePair(queryString.Substring(k_start), decode));

            return keys;
        }

        /// <summary>
        /// Parse the key value pair into a KeyValuePair object.
        /// </summary>
        /// <param name="keyAndValue">Raw key value pair text value.</param>
        /// <param name="decode">Whether the key value pair should be URL decoded.</param>
        /// <returns>A new instance of a KeyValuePair object.</returns>
        private static KeyValuePair<string, string> ParseKeyValuePair(string keyAndValue, bool decode = true)
        {
            // This is an empty key value.
            if (keyAndValue.Equals("="))
                new KeyValuePair<string, string>(String.Empty, String.Empty);

            for (var i = 0; i < keyAndValue.Length; i++)
            {
                // Found the separater for the key value pair.
                if (keyAndValue[i] == '=')
                {
                    // This one has a key value.
                    if (i > 0)
                    {
                        var key = decode ? HttpUtility.UrlDecode(keyAndValue.Substring(0, i)) : keyAndValue.Substring(0, i);
                        var value = decode ? HttpUtility.UrlDecode(keyAndValue.Substring(i + 1)) : keyAndValue.Substring(i + 1);
                        return new KeyValuePair<string, string>(key, value);
                    }
                    else
                    {
                        // This one has no key value.
                        var value = decode ? HttpUtility.UrlDecode(keyAndValue.Substring(i + 1)) : keyAndValue.Substring(i + 1);
                        return new KeyValuePair<string, string>(String.Empty, value);
                    }
                }
            }

            // The whole keyAndValue is only a value.
            return new KeyValuePair<string, string>(String.Empty, (decode ? HttpUtility.UrlDecode(keyAndValue) : keyAndValue));
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
