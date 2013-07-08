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
    /// Objects within the cache are weak referenced.
    /// Simple cache cannot contain null values.
    /// </summary>
    /// <typeparam name="T">Type of object to be cached in the dictionary.</typeparam>
    public sealed class SimpleCache<T>
        : IDisposable
    {
        #region Variables
        private readonly ReaderWriterLockSlim _Lock = new ReaderWriterLockSlim();
        private readonly Dictionary<string, WeakReference> _Cache = new Dictionary<string, WeakReference>();
        #endregion

        #region Properties
        /// <summary>
        /// get - The keys within the cache dictionary collection.
        /// </summary>
        public Dictionary<string, WeakReference>.KeyCollection Keys
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
            get 
            {
                return Get(key);
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Add an object to cache so that it doens't need to be recreated each time.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter 'item' cannot be null.</exception>
        /// <param name="cacheKey">Cache key value to identify item.</param>
        /// <param name="item">Object to add to cache.</param>
        /// <returns>Number of items in Cache.</returns>
        public int Add(string cacheKey, T item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");

            _Lock.EnterWriteLock();
            try
            {
                _Cache.Add(cacheKey, new WeakReference(item));
                return _Cache.Count;
            }
            finally
            {
                _Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Try to get the value from cache, if it is not in cache add it to cache.
        /// </summary>
        /// <param name="key">Unique cache key to identify the item.</param>
        /// <param name="add">Function to retrieve the value that will be added to cache.</param>
        /// <returns>Item from cache if it exists.</returns>
        public T LazyGet(string key, Func<T> add)
        {
            _Lock.EnterUpgradeableReadLock();
            try
            {
                if (_Cache.ContainsKey(key)
                    && _Cache[key].IsAlive)
                    return (T)_Cache[key].Target;

                _Lock.EnterWriteLock();
                try
                {
                    // Try again before attempting to add the value.
                    if (_Cache.ContainsKey(key)
                        && _Cache[key].IsAlive)
                        return (T)_Cache[key].Target;

                    var value = new WeakReference(add());
                    if (value == null)
                        return default(T);
                    _Cache.Add(key, value);
                    return (T)value.Target;
                }
                finally
                {
                    _Lock.ExitWriteLock();
                }
            }
            finally
            {
                _Lock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Get the object.
        /// If cacheKey does not exist it will return null.
        /// </summary>
        /// <param name="key">Cache key value to identify item.</param>
        /// <returns>Cached object if found, or null if not.</returns>
        public T Get(string key)
        {
            _Lock.EnterReadLock();
            try
            {
                if (_Cache.ContainsKey(key)
                    && _Cache[key].IsAlive)
                    return (T)_Cache[key].Target;

                return default(T);
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
                var found = _Cache.ContainsKey(cacheKey);

                // The reference has expired and it now contains a null value.
                if (found && !_Cache[cacheKey].IsAlive)
                {
                    _Cache.Remove(cacheKey);
                    return false;
                }

                return found;
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
