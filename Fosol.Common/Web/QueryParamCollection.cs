using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fosol.Common.Web
{
    /// <summary>
    /// QueryParamCollection provides a way to maintain a collection of query string parameters and their values.
    /// - Handles query parameters that have multiple values.
    /// </summary>
    public sealed class QueryParamCollection
        : IEnumerable<QueryParam>
    {
        #region Variables
        private readonly Dictionary<string, QueryParam> _Values;
        #endregion

        #region Properties
        /// <summary>
        /// get - A collection of QueryParam objects that contain the query parameter value.
        /// </summary>
        public ICollection<QueryParam> Values
        {
            get { return _Values.Select(v => v.Value).ToList(); }
        }

        /// <summary>
        /// get/set - The QueryParam object with the specified key name.
        /// </summary>
        /// <param name="key">Name to identify the query parameter.</param>
        /// <returns>QueryParam object for the specified key name.</returns>
        public QueryParam this[string key]
        {
            get
            {
                return _Values[key];
            }
            set
            {
                _Values[key] = value;
            }
        }

        /// <summary>
        /// get - Collection of key names.
        /// </summary>
        public ICollection<string> Keys
        {
            get { return _Values.Keys; }
        }

        /// <summary>
        /// get - Number of QueryParam objects in the collection.
        /// </summary>
        public int Count
        {
            get { return _Values.Count; }
        }

        /// <summary>
        /// get - Whether this collection is readonly.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a QueryParamCollection class.
        /// </summary>
        public QueryParamCollection()
        {
            _Values = new Dictionary<string, QueryParam>();
        }

        /// <summary>
        /// Creates a new instance of a QueryParamCollection class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'queryString' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter 'queryString' cannot be null.</exception>
        /// <param name="queryString">Initialize the query string parameters with this value.</param>
        /// <param name="decode">Whether the key value pairs should be URL decoded.</param>
        public QueryParamCollection(string queryString, bool decode = true)
            : base()
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(queryString, "queryString");

            var values = UriBuilder.ParseQueryStringToKeyValuePair(queryString, decode);

            foreach (var kv in values)
            {
                _Values.Add(kv.Key, new QueryParam(kv.Key, kv.Value));
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the enumerator for the collection of QueryParam objects.
        /// </summary>
        /// <returns>Enumerator for the collection of QueryParam objects.</returns>
        public IEnumerator<QueryParam> GetEnumerator()
        {
            var parameters = _Values.ToArray();

            foreach (var p in parameters)
            {
                yield return p.Value;
            }
        }

        /// <summary>
        /// Get the enumerator for the collection of QueryParam objects.
        /// </summary>
        /// <returns>Enumerator for the collection of QueryParam objects.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Values.GetEnumerator();
        }

        /// <summary>
        /// Add the QueryParam object to the collection.
        /// If the collection already contains a QueryParam with the specified key name it will be overwritten by this one.
        /// </summary>
        /// <param name="item">QueryParam object to add to the collection.</param>
        public void Add(QueryParam item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");

            if (this.ContainsKey(item.Name))
                this[item.Name] = item;
            else
                _Values.Add(item.Name, item);
        }

        /// <summary>
        /// Add the query parameter to the collection.
        /// If the collection already contains a QueryParam with the specified key name it will add this value to it.
        /// </summary>
        /// <param name="item">KeyValuePair item to add to the collection.</param>
        public void Add(KeyValuePair<string, string> item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");

            if (this.ContainsKey(item.Key))
                this[item.Key].Add(item.Value);
            else
                _Values.Add(item.Key, new QueryParam(item.Key, item.Value));
        }

        /// <summary>
        /// Add the key and value to the query parameter collection.
        /// If the collection already contains a QueryParam with the specified key name it will add this value to it.
        /// </summary>
        /// <param name="key">Name to identify the query parameter.</param>
        /// <param name="value">Value of the query parameter.</param>
        public void Add(string key, string value)
        {
            Fosol.Common.Validation.Assert.IsNotNull(key, "key");
            Fosol.Common.Validation.Assert.IsNotNull(value, "value");

            if (this.ContainsKey(key))
                this[key].Add(value);
            else
                this.Add(new QueryParam(key, value));
        }

        /// <summary>
        /// Remove the QueryParam that has the specified key name.
        /// </summary>
        /// <param name="key">Name to identify the query parameter.</param>
        /// <returns>True if the item was removed from the collection.</returns>
        public bool Remove(string key)
        {
            return _Values.Remove(key);
        }

        /// <summary>
        /// Attempt to find the query parameter with the specified key name and return it.
        /// </summary>
        /// <param name="key">Name to identify the query parameter.</param>
        /// <param name="value">QueryParam object with the specified key name.</param>
        /// <returns>True if the query parameter was found.</returns>
        public bool TryGetValue(string key, out QueryParam value)
        {
            return _Values.TryGetValue(key, out value);
        }

        /// <summary>
        /// Clear all of the query parameters from the collection.
        /// </summary>
        public void Clear()
        {
            _Values.Clear();
        }

        /// <summary>
        /// Determine if the collection contains a query parameter with the specified key name.
        /// </summary>
        /// <param name="key">Name to identify the query parameter.</param>
        /// <returns>True if the collection contains a query parameter with the specified key name.</returns>
        public bool Contains(string key)
        {
            Fosol.Common.Validation.Assert.IsNotNull(key, "key");
            return _Values.ContainsKey(key);
        }

        /// <summary>
        /// Determines if the collection contains the specified key name.
        /// </summary>
        /// <param name="key">Name to identify the query parameter.</param>
        /// <returns>True if the collection contains a query parameter with the specified key name.</returns>
        public bool ContainsKey(string key)
        {
            Fosol.Common.Validation.Assert.IsNotNull(key, "key");
            return _Values.ContainsKey(key);
        }

        /// <summary>
        /// Copy te contents of the collection into the specified array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">Index position to start copying within the destination array.</param>
        public void CopyTo(QueryParam[] array, int arrayIndex)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(array, "array");
            Fosol.Common.Validation.Enumerables.Assert.IsValidIndexPosition(arrayIndex, array, "arrayIndex");
            Fosol.Common.Validation.Assert.MaxRange(array.Length - arrayIndex, _Values.Count, "Parameter 'array' must have a length greater than equal to the number of items in this collection.");

            var index = 0;
            foreach (var value in _Values)
            {
                array[arrayIndex + index++] = new QueryParam(value.Key, value.Value.Values);
            }
        }

        /// <summary>
        /// Returns the full query string with all key value pairs.
        /// </summary>
        /// <returns>Full query string with all key value pairs.</returns>
        public override string ToString()
        {
            return _Values.Select(v => v.Value.ToString()).Aggregate((a, b) => a + "&" + b);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
