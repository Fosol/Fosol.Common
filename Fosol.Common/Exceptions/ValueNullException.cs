﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// ValueNullException class provides a way to identify an exception that occurs due to an null value.
    /// </summary>
    public class ValueNullException
        : ValueException
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ValueNullException()
            : base()
        {

        }

        public ValueNullException(string message)
            : base(message)
        {
        }

        public ValueNullException(string message, Exception innerException)
            : base(message, innerException)
        {
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
