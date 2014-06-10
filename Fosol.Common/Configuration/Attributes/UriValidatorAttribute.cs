﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration.Attributes
{
    /// <summary>
    /// UriValidatorAttribute provides a way to validate URI values passed to properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class UriValidatorAttribute
        : ConfigurationValidatorAttribute
    {
        #region Variables
        private UriKind _UriKind;
        #endregion

        #region Properties
        /// <summary>
        /// get - The URI kind that is allowed.
        /// </summary>
        public UriKind UriKind
        {
            get { return _UriKind; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a UriValidatorAttribute class.
        /// </summary>
        /// <param name="uriKind">The URI kind that is allowed.</param>
        public UriValidatorAttribute(UriKind uriKind = UriKind.RelativeOrAbsolute)
        {
            _UriKind = uriKind;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of a UriValidator class.
        /// </summary>
        public override ConfigurationValidatorBase ValidatorInstance
        {
            get
            {
                return new Validation.UriValidator(_UriKind);
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
