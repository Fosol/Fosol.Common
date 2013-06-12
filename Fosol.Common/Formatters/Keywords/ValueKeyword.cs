﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// A ValueKeyword provides a way to dynamically format the passed data and convert it into a string value.
    /// </summary>
    [FormatKeyword("value")]
    public sealed class ValueKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The formatting for the data.
        /// </summary>
        [FormatKeywordProperty("format", new string[] { "f" })]
        public string Format { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ValueKeyword.
        /// </summary>
        /// <param name="attributes">Configuration for this keyword.</param>
        public ValueKeyword(StringDictionary attributes)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Return a formatted string value.
        /// </summary>
        /// <param name="data">Data to use when generating the string result.</param>
        /// <returns>Formatted string value.</returns>
        public override string Render(object data)
        {
            if (string.IsNullOrEmpty(this.Format))
                return string.Format("{0}", data);
            else
                return string.Format("{0:" + this.Format + "}", data);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
