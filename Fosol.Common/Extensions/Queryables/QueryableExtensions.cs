using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Queryables
{
    /// <summary>
    /// Extension methods for queryable objects.
    /// </summary>
    public static class QueryableExtensions
    {
        #region Methods
        /// <summary>
        /// Outputs the LINQ query to a string.
        /// </summary>
        /// <param name="obj">IQueryable object.</param>
        /// <returns>LINQ statement as a string.</returns>
        public static string ToTraceString(this IQueryable obj)
        {
            return ((ObjectQuery)obj).ToTraceString();
        }
        #endregion
    }
}
