using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Web
{
    public sealed class QueryPathSegment
    {
        #region Variables
        private const string _FormatBoundary = @"\A({0})\Z";
        private static readonly Regex _PathSegmentRegex = new Regex(String.Format(_FormatBoundary, UriBuilder.PathSegmentRegex), RegexOptions.Compiled);
        private string _Value;
        #endregion

        #region Properties
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
        public QueryPathSegment()
        {
        }

        public QueryPathSegment(string value)
        {
            this.Value = value;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return _Value;
        }
        #endregion

        #region Operators
        public static implicit operator string(QueryPathSegment obj)
        {
            return obj.ToString();
        }

        public static explicit operator QueryPathSegment(string obj)
        {
            return new QueryPathSegment(obj);
        }
        #endregion

        #region Events
        #endregion
    }
}
