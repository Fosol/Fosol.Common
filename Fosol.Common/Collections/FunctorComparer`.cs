using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Collections
{
    /// <summary>
    /// FunctorComparer provides a way to sort collections of type T.
    /// </summary>
    /// <typeparam name="T">Type of object to sort.</typeparam>
    internal sealed class FunctorComparer<T> 
        : IComparer<T>
    {
        #region Variables
        Comparison<T> _Comparison;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a FunctorComparer object.
        /// </summary>
        /// <param name="comparison">Comparison object of type T.</param>
        public FunctorComparer(Comparison<T> comparison)
        {
            _Comparison = comparison;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compaires object 'x' to object 'y'.
        /// </summary>
        /// <param name="x">First object to compare.</param>
        /// <param name="y">Second object to compare.</param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            return _Comparison(x, y);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    } 
}
