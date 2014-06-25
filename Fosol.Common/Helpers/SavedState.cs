using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// SavedState class provides a dictionary for storing state and information for Windows Store and Windows Phone apps.
    /// </summary>
    public sealed class SavedState
    {
        #region Variables
        private Collections.StateDictionary _Items;
        private string _FilePath;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The value for the specified key.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>Value for the specified key.</returns>
        public object this[string key]
        {
            get { return _Items[key]; }
            set { _Items[key] = value; }
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
        /// Creates a new instance of a SavedState object.
        /// </summary>
        /// <param name="filePath">Path and filename for the saved state.</param>
        public SavedState(string filePath)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(filePath, "filePath");

            _FilePath = filePath;

            try
            {
                // Attempt to restore from a saved file.
                var saved_state = SavedState.RestoreAsync(filePath);
                _Items = saved_state.Result;
            }
            catch
            {
                _Items = new Collections.StateDictionary();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a new state object to SavedState for the specified key name.
        /// </summary>
        /// <param name="key">Key name to identify the saved value.</param>
        /// <param name="value">Value to save.</param>
        public void Add(string key, object value)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(key, "key");
            Fosol.Common.Validation.Assert.IsNotNull(value, "value");
            _Items.Add(key, value);
        }

        /// <summary>
        /// Remove the value with the specified key name.
        /// </summary>
        /// <param name="key">Key name to identify the value.</param>
        /// <returns>True if the key was removed.</returns>
        public bool Remove(string key)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(key, "key");
            return _Items.Remove(key);
        }

        /// <summary>
        /// Clear all items from SavedState.
        /// </summary>
        public void Clear()
        {
            _Items.Clear();
        }

        /// <summary>
        /// Save the state to the file system.
        /// </summary>
        public void Save()
        {
#if WINDOWS_APP || WINDOWS_PHONE_APP
            Serialization.DataContractUtility.SerializeToFile(_Items, _FilePath, Windows.Storage.CreationCollisionOption.ReplaceExisting);
#else
            Serialization.DataContractUtility.SerializeToFile(_Items, _FilePath, FileMode.Create, FileAccess.Write, FileShare.Inheritable);
#endif
        }

        /// <summary>
        /// Save the state to the file system.
        /// </summary>
        public async void SaveAsync()
        {
#if WINDOWS_APP || WINDOWS_PHONE_APP
            await Serialization.DataContractUtility.SerializeToFileAsync(_Items, _FilePath, Windows.Storage.CreationCollisionOption.ReplaceExisting);
#else
            await Serialization.DataContractUtility.SerializeToFileAsync(_Items, _FilePath, FileMode.Create, FileAccess.Write, FileShare.Inheritable);
#endif
        }

        /// <summary>
        /// Restore the state from the file system.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>StateDictionary object.</returns>
        private static Collections.StateDictionary Restore(string path)
        {
            return Serialization.DataContractUtility.DeserializeFromFile<Collections.StateDictionary>(path);
        }

        /// <summary>
        /// Restore the state from the file system.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>StateDictionary object.</returns>
        private async static Task<Collections.StateDictionary> RestoreAsync(string path)
        {
            return await Serialization.DataContractUtility.DeserializeFromFileAsync<Collections.StateDictionary>(path);
        }

        /// <summary>
        /// Restores this SavedState information by fetching the saved state file.
        /// </summary>
        public void Restore()
        {
            _Items = SavedState.Restore(_FilePath);
        }

        /// <summary>
        /// Restores this SavedState information by fetching the saved state file.
        /// </summary>
        public async Task RestoreAsync()
        {
            _Items = await SavedState.RestoreAsync(_FilePath);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
