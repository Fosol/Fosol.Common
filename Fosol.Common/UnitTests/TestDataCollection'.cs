using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// TestDataCollection<T> class provides a simple add only collection for generic test data.
    /// </summary>
    public class TestDataCollection<T>
        : IEnumerable<T>
        where T: ITestData
    {
        #region Variables
        private List<T> _Items;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TestDataCollection class.
        /// </summary>
        public TestDataCollection()
        {
            _Items = new List<T>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the enumerator for this collection.
        /// </summary>
        /// <returns>IEnumerator of type TestData.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator for this collection.
        /// </summary>
        /// <returns>IEnumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        /// <summary>
        /// Add the specified TestData to this collection.
        /// </summary>
        /// <param name="item">TestData object.</param>
        public void Add(T item)
        {
            _Items.Add(item);
        }

        /// <summary>
        /// Add the specified range of items to this collection.
        /// </summary>
        /// <param name="items">IEnumerable of type TestData object.</param>
        public void AddRange(IEnumerable<T> items)
        {
            _Items.AddRange(items);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
