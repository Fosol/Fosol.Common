﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Faults
{
    [DataContract(Name = "ParameterFormatFault", Namespace = "http://team.fosol.ca")]
    public sealed class ParameterFormatFault
        : ParameterFault
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ParameterFormatFault(string paramName, string message)
            : base(paramName, message)
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
