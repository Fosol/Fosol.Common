using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
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
        /// Creates a new element of type T
        /// </summary>
        /// <returns>Element of type T</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            T configurationElement = typeof(T).GetConstructor(new Type[] { }).
                    Invoke(new object[] { }) as T;
            return configurationElement;
        }

        /// <summary>
        /// Gets the element key name
        /// </summary>
        /// <param name="element">Element to fetch within collection</param>
        /// <returns>Key name of element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo keyProperty = null;
            foreach (PropertyInfo property in properties)
            {
                if (property.IsDefined(typeof(ConfigurationPropertyAttribute),
                    true))
                {
                    ConfigurationPropertyAttribute attribute = property.GetCustomAttributes(typeof(ConfigurationPropertyAttribute),

                        true)[0] as ConfigurationPropertyAttribute;

                    if (attribute != null &&
                        attribute.IsKey)
                    {
                        keyProperty = property;
                        break;
                    }
                }
            }
            object key = null;
            if (keyProperty != null)
            {
                key = keyProperty.GetValue(element, null);
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
            BaseAdd(element, true);
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
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
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
        #endregion

        #region Events
        #endregion
    }
}
