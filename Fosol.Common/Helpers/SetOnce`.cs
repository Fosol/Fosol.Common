using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// SetOnce class provides a way to ensure a value is only initialized one time.
    /// This class is thread safe.
    /// </summary>
    /// <typeparam name="T">Type of value that will be managed.</typeparam>
    public sealed class SetOnce<T>
    {
        #region Variables
        private readonly System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private T _Value;
        private bool _HasValue;
        #endregion

        #region Properties
        /// <summary>
        /// get - Whether the value has been set yet.
        /// </summary>
        public bool HasValue
        {
            get
            {
                _SlimLock.EnterReadLock();
                try
                {
                    return _HasValue;
                }
                finally
                {
                    _SlimLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// get/set - The actual value of the SetOnce class.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The value can only be set one time.  The value must be set before it can be retrieved.</exception>
        public T Value
        {
            get
            {
                _SlimLock.EnterReadLock();
                try
                {
                    if (!_HasValue) throw new InvalidOperationException(Resources.Multilingual.Exception_SetOnce_NotSet);

                    return _Value;
                }
                finally
                {
                    _SlimLock.ExitReadLock();
                }
            }
            set
            {
                _SlimLock.EnterWriteLock();
                try
                {
                    if (_HasValue) throw new InvalidOperationException(Resources.Multilingual.Exception_SetOnce_AlreadySet);

                    _HasValue = true;
                    _Value = value;
                }
                finally
                {
                    _SlimLock.ExitWriteLock();
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SetOnce class.
        /// </summary>
        public SetOnce()
        {
            _HasValue = false;
        }

        /// <summary>
        /// Creates a new instances of a SetOnce class.
        /// </summary>
        /// <param name="value">The value to initialize with.</param>
        public SetOnce(T value)
        {
            _HasValue = true;
            _Value = value;
        }
        #endregion

        #region Methods

        #endregion

        #region Operators
        /// <summary>
        /// Returns the SetOnce.Value property value.
        /// </summary>
        /// <param name="obj">SetOnce object.</param>
        /// <returns>Value of type T.</returns>
        public static implicit operator T(SetOnce<T> obj)
        {
            return obj.Value;
        }

        /// <summary>
        /// Returns a new instance of a SetOnce class with the intialized value.
        /// </summary>
        /// <param name="value">Value to intialize SetOnce with.</param>
        /// <returns>A new instance of a SetOnce class.</returns>
        public static implicit operator SetOnce<T>(T value)
        {
            return new SetOnce<T>(value);
        }
        #endregion

        #region Events
        #endregion
    }
}
