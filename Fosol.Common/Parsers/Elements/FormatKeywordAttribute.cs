using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// Defines the keyword name value within the special syntax {[name]}
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FormatKeywordAttribute
        : Attribute
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The name of the keyword.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// get/set - Controls whether this keyword will override a prior keyword with the same name.
        /// </summary>
        public bool Override { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a KeywordAttribute.
        /// </summary>
        /// <param name="name">Name of the keyword.</param>
        /// <param name="overrideKeyword">Whether this keyword should override another with the same name.</param>
        public FormatKeywordAttribute(string name, bool overrideKeyword = false)
        {
            this.Name = name;
            this.Override = overrideKeyword;
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion
    }
}
