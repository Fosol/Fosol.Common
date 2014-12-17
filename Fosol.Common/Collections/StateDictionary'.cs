using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Collections
{
    /// <summary>
    /// SavedState class provides a dictionary for storing state and information for Windows Store and Windows Phone apps.
    /// </summary>
    /// <typeparam name="T">Type of content being saved to state dictionary.</typeparam>
    public sealed class StateDictionary<T>
        : Dictionary<string, T>
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The value for the specified key.
        /// </summary>
        /// <param name="key">Key name to identify the value.</param>
        /// <returns>Value for the specified key, or default() if the key does not exist.</returns>
        public new T this[string key]
        {
            get
            {
                if (this.ContainsKey(key))
                    return base[key];

                return default(T);
            }
            set
            {
                base[key] = value;
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Copies the items in this StateDictionary into the destination StateDictionary.
        /// </summary>
        /// <param name="destination">StateDictionary destination.</param>
        public void CopyTo(StateDictionary<T> destination)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNull(destination, "destination");
            foreach (string key in this.Keys)
            {
                destination[key] = this[key];
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
