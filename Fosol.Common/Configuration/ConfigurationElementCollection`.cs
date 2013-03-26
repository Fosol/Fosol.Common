using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// a ConfigurationElementCollection of Type T, where T is a ConfigurationElement.
    /// </summary>
    /// <typeparam name="T">Type of ConfigurationElement.</typeparam>
    public class ConfigurationElementCollection<T>
        : ConfigurationElementCollection, ICollection<T>
        where T : ConfigurationElement
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - Number of items within the collection
        /// </summary>
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        /// get/set - An item within the collection
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Item within collection</returns>
        public T this[int index]
        {
            get { return (T)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// get - Returns an item within the collection by key name
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Item within collection</returns>
        public new T this[string name]
        {
            get { return (T)BaseGet(name); }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Get the attribute value with the specified name.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <returns>Value of the attribute.</returns>
        public object Attribute(string name)
        {
            return base[name];
        }

        /// <summary>
        /// Set the attribute value with the specified name and value.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value to set with.</param>
        public void Attribute(string name, object value)
        {
            base[name] = value;
        }

        /// <summary>
        /// Creates a new element of type T
        /// </summary>
        /// <returns>Element of type T</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            T element = typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { }) as T;
            return element;
        }

        /// <summary>
        /// Gets the element key name.
        /// This doesn't support multiple keys.
        /// </summary>
        /// <param name="element">Element to fetch within collection</param>
        /// <returns>Key name of element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo key_property = null;
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(ConfigurationPropertyAttribute), true))
                {
                    ConfigurationPropertyAttribute attribute = property.GetCustomAttributes(
                        typeof(ConfigurationPropertyAttribute),
                        true)[0] as ConfigurationPropertyAttribute;

                    if (attribute != null && attribute.IsKey)
                    {
                        key_property = property;
                        break;
                    }
                }
            }
            object key = null;
            if (key_property != null)
            {
                key = key_property.GetValue(element, null);
            }
            return key;
        }

        /// <summary>
        /// Returns the location within the collection of the element
        /// </summary>
        /// <param name="element">Element to find</param>
        /// <returns>Index of element</returns>
        public int IndexOf(T element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Adds element to collection
        /// </summary>
        /// <param name="element">Element to add</param>
        public void Add(T element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// Adds element to collection
        /// </summary>
        /// <param name="element">Element to add</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, this.ThrowOnDuplicate);
        }

        /// <summary>
        /// Removes element from collection at index
        /// </summary>
        /// <param name="index">Index position</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Removes element from collection by key name
        /// </summary>
        /// <param name="name">Key name of element</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Clears all elements from the collection
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// Determines if element exists within collection
        /// </summary>
        /// <param name="item">Element to look for</param>
        /// <returns>True if element exists</returns>
        public bool Contains(T item)
        {
            if (item == null)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (this[j] == null)
                    {
                        return true;
                    }
                }
                return false;
            }
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < Count; i++)
            {
                if (comparer.Equals(this[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copy into the array starting at the index position.
        /// </summary>
        /// <param name="array">Array to copy data to.</param>
        /// <param name="arrayIndex">Index position within destination array to start at.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Collection is readonly when enumerating through it
        /// </summary>
        public new bool IsReadOnly
        {
            get { return base.IsReadOnly(); }
        }

        /// <summary>
        /// Removes element from collection
        /// </summary>
        /// <param name="item">Element to remove</param>
        /// <returns>True if successful</returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                BaseRemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Enumerator for the collection
        /// </summary>
        /// <returns></returns>
        public new IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        /// <summary>
        /// Checks if the key exists in the collection.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>True if the key name exists.</returns>
        public bool ContainsKey(object key)
        {
            return base.BaseGetAllKeys().FirstOrDefault(k => k.Equals(key)) != null;
        }
        #endregion

        #region Events
        #endregion
    }
}
