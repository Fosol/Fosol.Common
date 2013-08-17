using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    /// <summary>
    /// A single part of a format which should not include format boundaries.
    /// </summary>
    public class FormatPart
    {
        #region Variables
        private readonly string _Value;
        #endregion

        #region Properties
        /// <summary>
        /// get - The text value.
        /// </summary>
        public string Value { get { return _Value; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a FormatPart object.
        /// </summary>
        /// <param name="value">Text value of the format without the boundaries.</param>
        public FormatPart(string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            _Value = value;
        }
        #endregion

        #region Methods

        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
