using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fosol.Common.Caching
{
    /// <summary>
    /// A SimpleCache object contains a dictionary that resides in memory.
    /// </summary>
    /// <typeparam name="T">Type of object to be cached in the dictionary.</typeparam>
    public sealed class SimpleCache<T>
        : IDisposable
    {
        #region Variables
        private readonly ReaderWriterLockSlim _Lock = new ReaderWriterLockSlim();
        private readonly Dictionary<string, T> _Cache = new Dictionary<string, T>();
        #endregion

        #region Properties
        /// <summary>
        /// get - The keys within the cache dictionary collection.
        /// </summary>
        public Dictionary<string, T>.KeyCollection Keys
        {
            get { return _Cache.Keys; }
        }

        /// <summary>
        /// get - The number of items in cache dictionary collection.
        /// </summary>
        public int Count
        {
            get { return _Cache.Count; }
        }

        /// <summary>
        /// get - The cached item with the specified key.
        /// </summary>
        /// <param name="key">Cache key value.</param>
        /// <returns>Item with the specified cache key.</returns>
        public T this[string key]
        {
            get { return _Cache[key]; }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Add an object to cache so that it doens't need to be recreated each time.
        /// </summary>
        /// <param name="cacheKey">Cache key value to identify item.</param>
        /// <param name="item">Object to add to cache.</param>
        /// <returns>Number of items in Cache.</returns>
        public int Add(string cacheKey, T item)
        {
            _Lock.EnterWriteLock();
            try
            {
                _Cache.Add(cacheKey, item);
                return _Cache.Count;
            }
            finally
            {
                _Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Get the object.
        /// If cacheKey does not exist it will return null.
        /// </summary>
        /// <param name="cacheKey">Cache key value to identify item.</param>
        /// <returns>Cached object if found, or null if not.</returns>
        public T Get(string cacheKey)
        {
            _Lock.EnterReadLock();
            try
            {
                return _Cache[cacheKey];
            }
            finally
            {
                _Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Determines if the cache contains the key.
        /// </summary>
        /// <param name="cacheKey">Cache key to search for.</param>
        /// <returns>True if the cache key exists.</returns>
        public bool ContainsKey(string cacheKey)
        {
            _Lock.EnterReadLock();
            try
            {
                return _Cache.ContainsKey(cacheKey);
            }
            finally
            {
                _Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes the item with the specified cache key.
        /// </summary>
        /// <param name="cacheKey">Cache key value to identify item.</param>
        /// <returns>True if item existed and was removed.</returns>
        public bool Remove(string cacheKey)
        {
            _Lock.EnterWriteLock();
            try
            {
                return _Cache.Remove(cacheKey);
            }
            finally
            {
                _Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Clear the cache.
        /// </summary>
        public void Dispose()
        {
            _Lock.EnterWriteLock();
            try
            {
                _Cache.Clear();
            }
            finally
            {
                _Lock.ExitWriteLock();
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
