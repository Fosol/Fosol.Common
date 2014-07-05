using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.IO
{
    /// <summary>
    /// SavedState class provides a dictionary for storing state and information for Windows Store and Windows Phone apps.
    /// </summary>
    public class SavedState
    {
        #region Variables
        private readonly System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private Collections.StateDictionary _Items;
        private string _FilePath;
        #endregion

        #region Properties
        /// <summary>
        /// get - The full filename and path that is used to store state information.
        /// </summary>
        private string FilePath
        {
            get { return _FilePath; }
        }

        /// <summary>
        /// get/set - The value for the specified key.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>Value for the specified key.</returns>
        public object this[string key]
        {
            get { return _Items[key]; }
            set 
            {
                _SlimLock.EnterWriteLock();
                try
                {
                    _Items[key] = value;
                }
                finally
                {
                    _SlimLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// get - Number of items in SavedState.
        /// </summary>
        public int Count
        {
            get { return _Items.Count; }
        }

        /// <summary>
        /// get - KeyCollection containing all key names.
        /// </summary>
        public Dictionary<string, object>.KeyCollection Keys
        {
            get { return _Items.Keys; }
        }

        /// <summary>
        /// get - ValueCollection containing all values in SavedState.
        /// </summary>
        public Dictionary<string, object>.ValueCollection Values
        {
            get { return _Items.Values; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SavedState class.
        /// </summary>
        /// <param name="filePath">Path and filename for the saved state.</param>
        public SavedState(string filePath)
            : this(filePath, true)
        {
        }

        /// <summary>
        /// Creates a new instance of a SavedState class.
        /// </summary>
        /// <param name="filePath">Path and filename for the saved state.</param>
        /// <param name="restoreIfFileExists">Attempt to restore the prior state if the file exists.</param>
        public SavedState(string filePath, bool restoreIfFileExists = true)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(filePath, "filePath");

            _FilePath = filePath;

            if (restoreIfFileExists)
            {
                try
                {
                    // Attempt to restore from a saved file.
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            await this.RestoreAsync();
                        }
                        catch
                        {
                            _Items = new Collections.StateDictionary();
                        }
                    });
                    task.Wait();
                }
                catch
                {
                    _Items = new Collections.StateDictionary();
                }
            }
            else
                _Items = new Collections.StateDictionary();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks if the collection has the specified key.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>True if the key exists.</returns>
        public bool ContainsKey(string key)
        {
            return _Items.ContainsKey(key);
        }

        /// <summary>
        /// Add a new state object to SavedState for the specified key name.
        /// </summary>
        /// <param name="key">Key name to identify the saved value.</param>
        /// <param name="value">Value to save.</param>
        public void Add(string key, object value)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(key, "key");
            Fosol.Common.Validation.Assert.IsNotNull(value, "value");

            _SlimLock.EnterWriteLock();
            try
            {
                _Items.Add(key, value);
            }
            finally
            {
                _SlimLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Remove the value with the specified key name.
        /// </summary>
        /// <param name="key">Key name to identify the value.</param>
        /// <returns>True if the key was removed.</returns>
        public bool Remove(string key)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(key, "key");

            _SlimLock.EnterWriteLock();
            try
            {
                return _Items.Remove(key);
            }
            finally
            {
                _SlimLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Clear all items from SavedState.
        /// </summary>
#if WINDOWS_APP || WINDOWS_PHONE_APP
        public async void Clear()
        {
            _Items.Clear();

            try
            {
                var file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(this.FilePath) as Windows.Storage.StorageFile;
                await file.DeleteAsync(Windows.Storage.StorageDeleteOption.PermanentDelete);
            }
            catch (FileNotFoundException)
            {
                // Ignore error if the file was not found.  Currently there is no way to see if a file exists.
            }
        }
#else
        public void Clear()
        {
            _Items.Clear();
            System.IO.File.Delete(this.FilePath);
        }
#endif

        /// <summary>
        /// Save the state to the file system.
        /// </summary>
        public void Save()
        {
            _SlimLock.EnterWriteLock();
            try
            {
#if WINDOWS_APP || WINDOWS_PHONE_APP
                Serialization.DataContractUtility.SerializeToFile(_Items, _FilePath, Windows.Storage.CreationCollisionOption.ReplaceExisting);
#else
                Serialization.DataContractUtility.SerializeToFile(_Items, _FilePath, FileMode.Create, FileAccess.Write, FileShare.Inheritable);
#endif
            }
            finally
            {
                _SlimLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Save the state to the file system.
        /// </summary>
        public async Task SaveAsync()
        {
#if WINDOWS_APP || WINDOWS_PHONE_APP
            await Serialization.DataContractUtility.SerializeToFileAsync(_Items, _FilePath, Windows.Storage.CreationCollisionOption.ReplaceExisting);
#else
            await Serialization.DataContractUtility.SerializeToFileAsync(_Items, _FilePath, FileMode.Create, FileAccess.Write, FileShare.Inheritable);
#endif
        }

        /// <summary>
        /// Restores this SavedState information by fetching the saved state file.
        /// </summary>
        public void Restore()
        {
            _SlimLock.EnterWriteLock();
            try
            {
                _Items = Serialization.DataContractUtility.DeserializeFromFile<Collections.StateDictionary>(_FilePath);
            }
            finally
            {
                _SlimLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Restores this SavedState information by fetching the saved state file.
        /// </summary>
        public async Task RestoreAsync()
        {
            _Items = await Serialization.DataContractUtility.DeserializeFromFileAsync<Collections.StateDictionary>(_FilePath);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
