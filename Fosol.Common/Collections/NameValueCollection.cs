using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Collections
{
    /// <summary>
    /// NameValueCollection class provides a collection of name value pairs for Windows Mobile devices.
    /// </summary>
    public sealed class NameValueCollection
        : List<KeyValuePair<string, string>>
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The value at the specified index position.
        /// </summary>
        /// <param name="index">Index position.</param>
        /// <returns>String value.</returns>
        public new string this[int index]
        {
            get { return base[index].Value; }
        }

        /// <summary>
        /// get - The value for the specified name.
        /// </summary>
        /// <param name="name">Name to identify which value to return.</param>
        /// <returns>String value.</returns>
        public string this[string name]
        {
            get { return this.SingleOrDefault(kv => kv.Key.Equals(name)).Value; }
        }

        /// <summary>
        /// get - A collection of key name values.
        /// </summary>
        public IEnumerable<string> AllKeys
        {
            get { return this.Select(kv => kv.Key); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a NameValueCollection object.
        /// </summary>
        public NameValueCollection()
        {
        }

        /// <summary>
        /// Creates a new instance of a NameValueCollection object.
        /// </summary>
        /// <param name="capacity">Capacity of the collection.</param>
        public NameValueCollection(int capacity)
            : base(capacity)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add the specified name and value to the collection.
        /// </summary>
        /// <param name="name">Name to identify.</param>
        /// <param name="value">Value of key.</param>
        public void Add(string name, string value)
        {
            this.Add(new KeyValuePair<string, string>(name, value));
        }

        /// <summary>
        /// Remove all occurances of the specified name.
        /// </summary>
        /// <param name="name">Name to identify the values.</param>
        public void Remove(string name)
        {
            var remove_indexes = new List<int>();
            for (var i = 0; i < this.Count; i++)
            {
                if (base[i].Key.Equals(name))
                    remove_indexes.Add(i);
            }

            foreach (var i in remove_indexes)
            {
                base.RemoveAt(i);
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
