using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Collections
{
    /// <summary>
    /// I'm not sure what purpose this comparer has, but it's from Microsoft source code and is used within the List collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class FunctorComparer<T> : IComparer<T>
    {
        Comparison<T> comparison;
        Comparer<T> c = Comparer<T>.Default;

        public FunctorComparer(Comparison<T> comparison)
        {
            this.comparison = comparison;
        }

        public int Compare(T x, T y)
        {
            return comparison(x, y);
        }
    } 
}
