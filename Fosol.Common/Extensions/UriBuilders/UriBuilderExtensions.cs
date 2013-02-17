using Fosol.Common.Extensions.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.UriBuilders
{
    /// <summary>
    /// Extension methods for UriBuilder objects.
    /// </summary>
    public static class UriBuilderExtensions
    {
        #region Methods
        /// <summary>
        /// Sets the specified query parameter key-value pair of the URI.
        /// If the key already exists, the value is overwritten.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "key" cannot be null.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "key" cannot be null.</exception>
        /// <param name="uri">UriBuilder object to update.</param>
        /// <param name="key">Query string parameter key name.</param>
        /// <param name="value">Query string parameter value.</param>
        /// <returns>Updated UriBuilder object.</returns>
        public static UriBuilder SetQueryParam(this UriBuilder uri, string key, string value)
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");

            var collection = uri.ParseQuery();

            // add (or replace existing) key-value pair
            collection[key] = value;

            var query = string.Join("&", collection
                .AsKeyValuePairs()
                .Select(p => p.Key == null ? p.Value : p.Key + "=" + p.Value));

            uri.Query = query;

            return uri;
        }

        /// <summary>
        /// Removes a query parameter from the Uri.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "key" cannot be null.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "key" cannot be null.</exception>
        /// <param name="uri">UriBuilder object.</param>
        /// <param name="key">Key name of the query parameter to remove.</param>
        /// <returns>Updated UriBuilder object.</returns>
        public static UriBuilder RemoveQueryParam(this UriBuilder uri, string key)
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");

            var collection = uri.ParseQuery();

            // add (or replace existing) key-value pair
            collection.Remove(key);

            var query = string.Join("&", collection
                .AsKeyValuePairs()
                .Select(p => p.Key == null ? p.Value : p.Key + "=" + p.Value));

            uri.Query = query;

            return uri;
        }

        /// <summary>
        /// Gets the query string key-value pairs of the URI.
        /// Note that the one of the keys may be null ("?123") and
        /// that one of the keys may be an empty string ("?=123").
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "uri" cannot be null.</exception>
        /// <returns>Enumeration of KeyValuePairs.</returns>
        public static IEnumerable<KeyValuePair<string, string>> GetQueryParams(this UriBuilder uri)
        {
            Validation.Parameter.AssertIsNotNull(uri, "uri");
            return uri.ParseQuery().AsKeyValuePairs();
        }

#if WINDOWS_PHONE
        /// <summary>
        /// Converts the legacy NameValueCollection into a strongly-typed KeyValuePair sequence.
        /// </summary>
        private static IEnumerable<KeyValuePair<string, string>> AsKeyValuePairs(this Dictionary<string, string> collection)
        {
            return collection.ToList<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Parses the query string of the URI into a NameValueCollection.
        /// </summary>
        private static Dictionary<string, string> ParseQuery(this UriBuilder uri)
        {
            return uri.Query.ParseQueryString();
        }
#else
        /// <summary>
        /// Converts the legacy NameValueCollection into a strongly-typed KeyValuePair sequence.
        /// </summary>
        private static IEnumerable<KeyValuePair<string, string>> AsKeyValuePairs(this System.Collections.Specialized.NameValueCollection collection)
        {
            foreach (string key in collection.AllKeys)
            {
                yield return new KeyValuePair<string, string>(key, collection.Get(key));
            }
        }

        /// <summary>
        /// Parses the query string of the URI into a NameValueCollection.
        /// </summary>
        private static System.Collections.Specialized.NameValueCollection ParseQuery(this UriBuilder uri)
        {
            return System.Web.HttpUtility.ParseQueryString(uri.Query);
        }
#endif
        #endregion
    }
}
