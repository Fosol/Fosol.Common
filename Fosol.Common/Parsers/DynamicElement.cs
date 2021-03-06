﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// A DynamicElement is generated every time it is called.
    /// </summary>
    public abstract class DynamicElement
        : FormatElement
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a DynamicElement object.
        /// </summary>
        /// <param name="attributes">StringDictionary of attributes to include with this keyword.</param>
        public DynamicElement(StringDictionary attributes = null)
            : base(attributes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the dynamic value of the keyword.
        /// </summary>
        /// <param name="data">Object containing data for dynamic keywords.</param>
        /// <returns>The dynamic value of the keyword.</returns>
        public abstract string Render(object data);
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
