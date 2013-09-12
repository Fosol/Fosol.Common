using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Dictionaries
{
    /// <summary>
    /// Dictionary extension methods.
    /// </summary>
    public static class DictionaryExtensions
    {
        #region Methods
        /// <summary>
        /// Determines if the StringDictionary has the same key and value pairs.
        /// </summary>
        /// <param name="obj">Original StringDictionary object.</param>
        /// <param name="compare">StringDictionary to compare with.</param>
        /// <returns>True if they are equal.</returns>
        public static bool IsEqual(this StringDictionary obj, StringDictionary compare)
        {
            foreach (string key in obj.Keys)
            {
                if (!compare.ContainsKey(key))
                    return false;
                else if (!obj[key].Equals(compare[key]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Aggregates the StringDictionary into a query string.
        /// </summary>
        /// <param name="obj">StringDictionary object.</param>
        /// <returns>Query string.</returns>
        public static string ToQueryString(this StringDictionary obj)
        {
            var builder = new StringBuilder();
            foreach (string key in obj.Keys)
            {
                if (builder.Length > 0)
                    builder.Append("&");
                builder.Append(key + "=" + obj[key]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Converts the StringDictionary into a generic Dictionary.
        /// </summary>
        /// <param name="obj">StringDictionary object.</param>
        /// <returns>New Dictionary object.</returns>
        public static Dictionary<string, object> ToDictionary(this StringDictionary obj)
        {
            if (obj == null)
                return null;

            var dictionary = new Dictionary<string, object>();

            foreach (string key in obj.Keys)
            {
                var v = obj[key];
                dictionary.Add(key, obj[key]);
            }

            return dictionary;
        }
        #endregion
    }
}
