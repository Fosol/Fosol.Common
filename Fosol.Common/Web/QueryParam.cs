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
        /// Gets a single string value that represents all the values for this query parameter.
        /// </summary>
        /// <param name="delimiter">Delimiter to separate multiple values.  Default delimiter is a comma ','.</param>
        /// <returns>String value of this query parameter.</returns>
        public string GetValue(string delimiter = ",")
        {
            if (this.IsMultiValue)
                return _Values.ToString(delimiter);
            else
                return _Values[0];
        }

        /// <summary>
        /// Returns a full query string parameter key value pair (i.e. key=value).
        /// If the key contains mulitple value it will aggregate them with a comma delimiter (i.e. key=value1,value2,value3).
        /// </summary>
        /// <returns>Query string key value pair.</returns>
        public override string ToString()
        {
            return this.ToString(",");
        }

        /// <summary>
        /// Returns a full query string parameter key value pair (i.e. key=value).
        /// If the key contains mulitple value it will aggregate them with a comma delimiter (i.e. key=value1,value2,value3).
        /// </summary>
        /// <param name="delimiter">Delimiter to use to separate multiple values.</param>
        /// <returns>Query string key value pair.</returns>
        public string ToString(string delimiter)
        {
            return String.Format("{0}={1}", this.Name, this.GetValue(delimiter));
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
