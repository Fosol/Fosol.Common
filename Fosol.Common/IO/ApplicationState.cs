using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.IO
{
    /// <summary>
    /// ApplicationState class provides a way to manage how many state values can be saved.
    /// Based on cache size it follows FIFO flow.
    /// </summary>
    public sealed class ApplicationState
        : SavedState
    {
        #region Variables
        private int _CacheSize = 1;
        private Queue<string> _Queue;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The number of values that can be cached into this state collection.
        /// </summary>
        public int CacheSize
        {
            get { return _CacheSize; }
            set 
            {
                Fosol.Common.Validation.Assert.MinRange(value, 1, "CacheSize");

                if (value != _CacheSize)
                {
                    if (value < _CacheSize)
                    {
                        for (var i = (_CacheSize - value); i > 0; i--)
                        {
                            var remove_key = _Queue.Dequeue();
                            base.Remove(remove_key);
                        }
                    }

                    // Create a new queue and populate it with the prior queue.
                    var queue = new Queue<string>(value);

                    while (_Queue.Count > 0)
                    {
                        queue.Enqueue(_Queue.Dequeue());
                    }
                    _CacheSize = value;
                    _Queue = queue;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an ApplicationState class.
        /// </summary>
        /// <param name="filePath">Filename and path to saved file.</param>
        public ApplicationState(string filePath)
            : this(filePath, false)
        {
        }

        /// <summary>
        /// Creates a new instance of an ApplicationState class.
        /// </summary>
        /// <param name="filePath">Filename and path to saved file.</param>
        /// <param name="restoreIfFileExists">Whether the file should be loaded to restore state upon initialization.</param>
        public ApplicationState(string filePath, bool restoreIfFileExists = true)
            : base(filePath, restoreIfFileExists)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the state and populate the cache queue.
        /// </summary>
        /// <param name="state">StateDictionary object.</param>
        protected override void Initialize(Collections.StateDictionary state)
        {
            base.Initialize(state);
            this.InitializeQueue();
        }

        /// <summary>
        /// Initialize the queue with the state collection information.
        /// </summary>
        private void InitializeQueue()
        {
            _CacheSize = this.Count;
            _Queue = new Queue<string>(_CacheSize);

            foreach (string key in this.Keys)
            {
                _Queue.Enqueue(key);
            }
        }

        /// <summary>
        /// When application state is restored from a file update the queue with the information.
        /// </summary>
        public override void Restore()
        {
            base.Restore();
            this.InitializeQueue();
        }

        /// <summary>
        /// Add a new state object to SavedState for the specified key name.
        /// Ensures only the CacheSize limit is enforced.  Older cached content is removed.
        /// </summary>
        /// <param name="key">Key name to identify the saved value.</param>
        /// <param name="value">Value to save.</param>
        public new void Add(string key, object value)
        {
            if (this.Count != 0
                && this.Count >= this.CacheSize)
            {
                var remove_key = _Queue.Dequeue();
                base.Remove(remove_key);
            }

            base.Add(key, value);
            _Queue.Enqueue(key);
        }

        /// Remove the value with the specified key name.
        /// </summary>
        /// <param name="key">Key name to identify the value.</param>
        /// <returns>True if the key was removed.</returns>
        public new void Remove(string key)
        {
            // Need to remove the key from the queue.
            for (var i = 0; i < _Queue.Count; i++)
            {
                var q_key = _Queue.Dequeue();

                if (q_key.Equals(key))
                {
                    // Remove the value from the state collection.
                    base.Remove(key);
                }
                else
                {
                    // It's not the key to be removed, so add it back to the query.
                    _Queue.Enqueue(q_key);
                }
            }
        }

        /// <summary>
        /// Clear all values in ApplicationState.
        /// </summary>
        public new void Clear()
        {
            _Queue.Clear();
            base.Clear();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
