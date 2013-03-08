using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Enumerables
{
    /// <summary>
    /// Enumerable extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Methods
        /// <summary>
        /// Syntax sugar...
        /// Preforms foreach to enumerable source object.
        /// </summary>
        /// <typeparam name="T">Type of object in source.</typeparam>
        /// <param name="source">Source enumerable object to iterate through.</param>
        /// <param name="action">Action to perform on each item within the source.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
        #endregion
    }
}
