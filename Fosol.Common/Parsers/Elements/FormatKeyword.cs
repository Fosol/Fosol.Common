﻿using Fosol.Common.Extensions.Dictionaries;
using Fosol.Common.Extensions.Reflection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// Abstract class that represents a Keyword within a formatted string value.
    /// Do not inherit from this class, instead inherit from the DynamicKeyword and/or StaticKeyword abstract classes.
    /// </summary>
    public abstract class FormatKeyword
    {
        #region Variables
        private string _Name;
        private readonly StringDictionary _Attributes = new StringDictionary();
        #endregion

        #region Properties
        /// <summary>
        /// get - The name value in the KeywordAttribute.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            private set { _Name = value; }
        }

        /// <summary>
        /// get - A dictionary of attributes for this keyword.
        /// </summary>
        public StringDictionary Attributes
        {
            get { return _Attributes; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a Keyword object.
        /// Initializes the Name property with the KeywordAttribute.Name property.
        /// </summary>
        /// <exception cref="Fosol.Common.Exceptions.AttributeRequiredException">The KeywordAttributeAttribute is required.</exception>
        internal FormatKeyword()
        {
            var attr = (FormatKeywordAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(FormatKeywordAttribute));
            if (attr != null)
                this.Name = attr.Name;
            else
                throw new Common.Exceptions.AttributeRequiredException(typeof(FormatKeywordAttribute));
        }

        /// <summary>
        /// Creates a new instance of a Keyword object.
        /// </summary>
        /// <param name="attributes">Dictionary of attributes to include with this keyword.</param>
        internal FormatKeyword(StringDictionary attributes)
            : this()
        {
            InitAttributes(attributes);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the Attributes collection.
        /// </summary>
        /// <param name="attributes">Dictionary of attributes to include with this keyword.</param>
        protected void InitAttributes(StringDictionary attributes)
        {
            // Get all the valid attributes.
            var properties = (
                from p in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where p.GetCustomAttributes(typeof(FormatKeywordPropertyAttribute), false).FirstOrDefault() != null
                select p);

            // Loop through the valid properties (this will stop incorrect parameters from being provided).
            foreach (var prop in properties)
            {
                string value = null;
                var attr = prop.GetCustomAttributes(typeof(FormatKeywordPropertyAttribute), false).FirstOrDefault() as FormatKeywordPropertyAttribute;

                // Check if the parameters have a match.
                if (attributes != null)
                {
                    value = attributes[attr.Name];

                    // Go through abbreviations.
                    if (value == null && attr.Abbreviations != null)
                    {
                        foreach (var abbr in attr.Abbreviations)
                        {
                            // Found a valid abbreviation.
                            if (attributes[abbr] != null)
                            {
                                value = attributes[abbr];
                                break;
                            }
                        }
                    }
                }

                // Parameter was found so use it.
                if (value != null)
                {
                    if (attr.Converter == null)
                        prop.SetValue2(this, value);
                    else
                        prop.SetValue2(this, value, attr.Converter);

                    this.Attributes.Add(attr.Name, value);
                    continue;
                }
                
                var default_attr = prop.GetCustomAttributes(typeof(DefaultValueAttribute), false).FirstOrDefault() as DefaultValueAttribute;

                // Use the default value.
                if (default_attr != null)
                    prop.SetValue(this, default_attr.Value);
                // This parameter is required, throw exception.
                else if (attr.IsRequired)
                    throw new Exceptions.FormatKeywordAttributeRequiredException(this.Name, attr.Name);
            }
        }

        /// <summary>
        /// Returns a formatted string value to create this keyword.
        /// </summary>
        /// <example>
        /// {datetime?format=u}
        /// </example>
        /// <returns>Special formatted string value.</returns>
        public override string ToString()
        {
            if (this.Attributes.Count == 0)
                return "{" + this.Name + "}";
            else
                return string.Format("{{{0}?{1}}}", this.Name, this.Attributes.ToQueryString());
        }

        /// <summary>
        /// Returns a formatted string value based on the formatter which can be used to create this keyword.
        /// </summary>
        /// <param name="formatter">KeywordFormatter object.</param>
        /// <returns>Special formatted string value.</returns>
        public string ToString(KeywordFormatter formatter)
        {
            if (this.Attributes.Count == 0)
                return formatter.StartBoundary + this.Name + formatter.EndBoundary;
            else
                return string.Format("{0}{1}{2}{3}{4}", formatter.StartBoundary, this.Name, formatter.AttributeBoundary, this.Attributes.ToQueryString(), formatter.EndBoundary);
        }

        /// <summary>
        /// Returns the HashCode for this keyword.
        /// This HashCode is composed of the Name and the Parameters.
        /// </summary>
        /// <returns>HashCode for this keyword.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode()
                + this.Attributes.GetHashCode();
        }

        /// <summary>
        /// Determine if the object is equal to this keyword.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>True if they are equal.</returns>
        public override bool Equals(object obj)
        {
            var keyword = obj as FormatKeyword;

            if (ReferenceEquals(keyword, null))
                return false;

            if (ReferenceEquals(keyword, this))
                return true;

            if (this.Name == keyword.Name)
                return this.Attributes.IsEqual(keyword.Attributes);

            return false;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
