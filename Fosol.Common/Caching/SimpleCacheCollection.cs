using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Caching
{
    /// <summary>
    /// Provides a collection of cached objects that will automatically enforce garabage collection.
    /// Caching is in memory.
    /// Uses the ICacheObject.
    /// </summary>
    public class SimpleCacheCollection 
        : IList<ICacheObject>, IDisposable
    {
        #region Variables
        #endregion

        #region Properties

        public ICacheObject this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public int IndexOf(ICacheObject item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ICacheObject item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(ICacheObject item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ICacheObject item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ICacheObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ICacheObject item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ICacheObject> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
