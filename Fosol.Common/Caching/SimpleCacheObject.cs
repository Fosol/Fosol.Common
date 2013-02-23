using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Caching
{
    /// <summary>
    /// Provides finite control over the object and its caching behavior.
    /// </summary>
    public sealed class SimpleCacheObject
        : IDisposable, Fosol.Common.Caching.ICacheObject
    {
        #region Variables
        private readonly object _Lock = new object();
        private object _Item;
        private TimeSpan? _LengthOfTime;
        private bool _RenewOnRequest;
        private DateTime? _ExpiresOn;
        #endregion

        #region Properties
        /// <summary>
        /// get - The object being cached.
        /// </summary>
        public object Item 
        {
            get { return _Item; }
            private set { _Item = value; }
        }

        /// <summary>
        /// get/set - The length of time before cache expires for this item.
        /// </summary>
        public TimeSpan? LengthOfTime
        {
            get { return _LengthOfTime; }
            set 
            {
                lock (_Lock)
                {
                    _LengthOfTime = value;

                    if (value.HasValue)
                        _ExpiresOn = Optimization.FastDateTime.UtcNow.Add(_LengthOfTime.Value);
                }
            }
        }

        /// <summary>
        /// get/set - When 'true' it will reset the ExpiresOn based on the LengthOfTime when the object has been requested.
        /// </summary>
        public bool RenewOnRequest
        {
            get { return _RenewOnRequest; }
            set 
            {
                lock (_Lock)
                {
                    _RenewOnRequest = value;
                }
            }
        }

        /// <summary>
        /// get/set - When the item will expire and be disposed.
        /// </summary>
        public DateTime? ExpiresOn
        {
            get { return _ExpiresOn; }
            set 
            {
                lock (_Lock)
                {
                    _ExpiresOn = value;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SimpleCacheObject.
        /// This constructor will by default cache this item indefinitely.
        /// </summary>
        /// <param name="item">Object to cache in memory.</param>
        public SimpleCacheObject(object item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Creates a new instance of a SimpleCacheObject.
        /// </summary>
        /// <param name="item">Object to cache in memory.</param>
        /// <param name="lengthOfTime">Length of time before the item is removed from cache.</param>
        /// <param name="renewOnRequest">When 'true' it will reset the ExpiresOn based on the LengthOfTime when the item has been requested.</param>
        public SimpleCacheObject(object item, TimeSpan lengthOfTime, bool renewOnRequest = true)
        {
            this.Item = item;
            this.LengthOfTime = lengthOfTime;
            this.RenewOnRequest = renewOnRequest;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renew the ExpiresOn based on the LengthOfTime.
        /// If LengthOfTime is not set this method cannot do anything.
        /// </summary>
        public void Renew()
        {
            lock (_Lock)
            {
                if (_LengthOfTime.HasValue)
                    _ExpiresOn = Optimization.FastDateTime.UtcNow.Add(_LengthOfTime.Value);
            }
        }

        /// <summary>
        /// Renew the ExpiresOn based on the LengthOfTime.
        /// </summary>
        /// <param name="lengthOfTime">Length of time before this item is released from cache.</param>
        public void Renew(TimeSpan lengthOfTime)
        {
            Validation.Assert.IsNotNull(lengthOfTime, "lengthOfTime");
            lock (_Lock)
            {
                _LengthOfTime = lengthOfTime;
                _ExpiresOn = Optimization.FastDateTime.UtcNow.Add(_LengthOfTime.Value);
            }
        }

        /// <summary>
        /// Renew the ExpiresOn cache length.
        /// </summary>
        /// <param name="expiresOn">Time when object should be released from cache.</param>
        public void Renew(DateTime expiresOn)
        {
            Validation.Assert.IsNotNull(expiresOn, "expiresOn");
            lock (_Lock)
            {
                _ExpiresOn = expiresOn;
            }
        }

        /// <summary>
        /// Dispose the reference to the cached object.
        /// </summary>
        public void Dispose()
        {
            lock (_Lock)
            {
                // Release reference, this will allow GC to clean up on its own.
                _Item = null;
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
