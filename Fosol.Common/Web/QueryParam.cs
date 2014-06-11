using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Web
{
    /// <summary>
    /// QueryParam provides a way to manage a single query parameter key and all it's values.
    /// </summary>
    public sealed class QueryParam
    {
        #region Variables
        private string _Name;
        private QueryParamValue _Values;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The value of this parameter at the specified index position.
        /// </summary>
        /// <param name="index">Index position.</param>
        /// <returns>Value at the specified index position.</returns>
        public string this[int index]
        {
            get { return _Values[index]; }
            set { _Values[index] = value; }
        }

        /// <summary>
        /// get - The name to identify this query string parameter.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            private set { _Name = value; }
        }

        /// <summary>
        /// get - Array of values for this query string paramter.
        /// </summary>
        public string[] Values
        {
            get { return _Values.ToArray(); }
        }

        /// <summary>
        /// get - Whether this query string parameter has multiple values.
        /// </summary>
        public bool IsMultiValue
        {
            get { return _Values.Count > 1; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a QueryParam class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'name' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Paramter 'name' cannot be null.</exception>
        /// <param name="name">Name to identify this query parameter.</param>
        public QueryParam(string name)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(name, "name");
            _Name = name;
            _Values = new QueryParamValue();
            _Values.Add(String.Empty);
        }

        /// <summary>
        /// Creates a new instance of a QueryParam class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'name' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Paramters 'name' and 'value' cannot be null.</exception>
        /// <param name="name">Name to identify this query parameter.</param>
        /// <param name="value"></param>
        public QueryParam(string name, string value)
            : this(name)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(name, "name");
            Fosol.Common.Validation.Assert.IsNotNull(value, "value");
            _Values.Add(value);
        }

        /// <summary>
        /// Creates a new instance of a QueryParam class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'name' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentException">Parameter 'values' cannot be an empty 0 length array.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'name' and 'values' cannot be null.</exception>
        /// <param name="name">Name to identify this query parameter.</param>
        /// <param name="values">Initial array of values for this query parameter.</param>
        public QueryParam(string name, string[] values)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(name, "name");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(values, "values");

            _Name = name;
            _Values = new QueryParamValue(values);
        }

        /// <summary>
        /// Creates a new instance of a QueryParam class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'key' must have a Key property value that is not null, empty or whitespace.</exception>
        /// <exception cref="Systme.ArgumentNullException">Parameter 'key' cannot be null.</exception>
        /// <param name="parameter">KeyValuePair object.</param>
        public QueryParam(KeyValuePair<string, string> parameter)
        {
            Fosol.Common.Validation.Assert.IsNotNull(parameter, "parameter");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(parameter.Key, "parameter.Key");

            _Name = parameter.Key;
            _Values = new QueryParamValue();
            _Values.Add(parameter.Value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a full query string parameter key value pair (i.e. key=value).
        /// If the key contains mulitple value it will aggregate them (i.e. key=value1&key=value2&key=value3).
        /// </summary>
        /// <returns>Query string key value pair.</returns>
        public override string ToString()
        {
            return _Values.Aggregate((a, b) => String.Format("{0}={1}", this.Name, a) + "&" + (string.Format("{0}={1}", this.Name, b)));
        }

        /// <summary>
        /// Add a new value to the query parameter.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <param name="allowDuplicates">Whether or not to allow duplicate values to be added.</param>
        public void Add(string value, bool allowDuplicates = true)
        {
            if (allowDuplicates
                || (!allowDuplicates && !_Values.Contains(value)))
                _Values.Add(value);
        }

        /// <summary>
        /// Add each value in the array to the query parameter.
        /// </summary>
        /// <param name="values">Array of values to add.</param>
        /// <param name="allowDuplicates">Whether or not to allow duplicate values to be added.</param>
        public void Add(string[] values, bool allowDuplicates = true)
        {
            foreach (var value in values)
            {
                this.Add(value, allowDuplicates);
            }
        }

        /// <summary>
        /// Removes all copies of the value from the query parameter.
        /// </summary>
        /// <param name="value">Value to remove.</param>
        /// <returns>True if the value was found and removed.</returns>
        public bool Remove(string value)
        {
            return _Values.Remove(value);
        }

        /// <summary>
        /// Clears all the values from the query parameter.
        /// </summary>
        public void Clear()
        {
            _Values.Clear();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
