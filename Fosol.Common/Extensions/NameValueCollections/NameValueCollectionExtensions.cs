using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.NameValueCollections
{
    /// <summary>
    /// Extension methods for NameValueCollection objects.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        #region Methods
        /// <summary>
        /// Determines if the NameValueCollection has the same key and value pairs.
        /// </summary>
        /// <param name="obj">Original NameValueCollection object.</param>
        /// <param name="compare">NameValueCollection to compare with.</param>
        /// <returns>True if they are equal.</returns>
        public static bool IsEqual(this NameValueCollection obj, NameValueCollection compare)
        {
            return obj.AllKeys.OrderBy(k => k)
                .SequenceEqual(compare.AllKeys.OrderBy(k => k))
                && obj.AllKeys.All(k => obj[k] == compare[k]);
        }
        #endregion
    }
}
