﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Web
{
    /// <summary>
    /// UriPathSegment provides a way to manage a single path segment within a Uri.
    /// </summary>
    public sealed class UriPathSegment
    {
        #region Variables
        private const string _FormatBoundary = @"\A({0})\Z";
        private static readonly Regex _PathSegmentRegex = new Regex(String.Format(_FormatBoundary, UriBuilder.PathSegmentRegex), RegexOptions.Compiled);
        private string _Value;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The path segment value.
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set 
            {
                if (String.IsNullOrEmpty(value))
                {
                    _Value = String.Empty;
                    return;
                }

                var match = _PathSegmentRegex.Match(value);
                if (!match.Success)
                    throw new UriFormatException("Path segment value has invalid characters.");

                _Value = value; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a UriPathSegment class.
        /// </summary>
        public UriPathSegment()
        {
        }

        /// <summary>
        /// Creates a new instance of a UriPathSegment class.
        /// </summary>
        /// <param name="value">Initial path segment value.</param>
        public UriPathSegment(string value)
        {
            this.Value = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the path segment value.
        /// </summary>
        /// <returns>The path segment value.</returns>
        public override string ToString()
        {
            return _Value;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Convert the UriPathSegment into a string.
        /// </summary>
        /// <param name="obj">UriPathSegment object.</param>
        /// <returns>Uri path segment value.</returns>
        public static implicit operator string(UriPathSegment obj)
        {
            return obj.ToString();
        }

        /// <summary>
        /// Convert the string into a UriPathSegment object.
        /// </summary>
        /// <param name="obj">String value.</param>
        /// <returns>A new instance of a UriPathSegment class initialized with the specified string value.</returns>
        public static explicit operator UriPathSegment(string obj)
        {
            return new UriPathSegment(obj);
        }
        #endregion

        #region Events
        #endregion
    }
}