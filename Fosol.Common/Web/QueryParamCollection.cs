using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Web
{
    public sealed class QueryParamCollection
        : IEnumerable<QueryParam>
    {
        #region Variables
        private readonly Dictionary<string, QueryParam> _Values;
        #endregion

        #region Properties

        public ICollection<QueryParam> Values
        {
            get { return _Values.Select(v => v.Value).ToList(); }
        }

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

        public ICollection<string> Keys
        {
            get { return _Values.Keys; }
        }

        public int Count
        {
            get { return _Values.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Constructors
        public QueryParamCollection()
        {
            _Values = new Dictionary<string, QueryParam>();
        }
        #endregion

        #region Methods
        public IEnumerator<QueryParam> GetEnumerator()
        {
            var parameters = _Values.ToArray();

            foreach (var p in parameters)
            {
                yield return p.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Values.GetEnumerator();
        }

        public void Add(QueryParam item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");
            _Values.Add(item.Name, item);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(item.Key, "item.Key");
            _Values.Add(item.Key, new QueryParam(item.Key, item.Value));
        }

        public bool ContainsKey(string key)
        {
            return _Values.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return _Values.Remove(key);
        }

        public bool TryGetValue(string key, out QueryParam value)
        {
            return _Values.TryGetValue(key, out value);
        }

        public void Clear()
        {
            _Values.Clear();
        }

        public bool Contains(QueryParam item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");
            return _Values.Contains(new KeyValuePair<string,QueryParam>(item.Name, item));
        }

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

        public bool Remove(QueryParam item)
        {
            Fosol.Common.Validation.Assert.IsNotNull(item, "item");
            return _Values.Remove(item.Name);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
