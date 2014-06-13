using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fosol.Common.Web
{
    public sealed class QueryPath
        : IEnumerable<QueryPathSegment>
    {
        #region Variables
        private List<QueryPathSegment> _Segments;
        #endregion

        #region Properties
        public QueryPathSegment this[int index]
        {
            get { return _Segments[index]; }
            set { _Segments[index] = value; }
        }

        public int Count
        {
            get { return _Segments.Count; }
        }

        public bool Readonly
        {
            get { return false; }
        }
        #endregion

        #region Constructors
        public QueryPath()
        {
            _Segments = new List<QueryPathSegment>();
        }

        public QueryPath(string path)
            : this()
        {
            if (String.IsNullOrEmpty(path)
                || path.Equals("/"))
                return;

            if (path.StartsWith("//"))
                throw new UriFormatException("Path has invalid characters.");

            // Replace Whitespace.
            UriBuilder.ReplaceWhitespaces(ref path);

            if (path.StartsWith("/"))
                path = path.Substring(1);

            var split_path = path.Split('/');

            foreach (var path_segment in split_path)
            {
                _Segments.Add(new QueryPathSegment(path_segment));
            }
        }
        #endregion

        #region Methods

        public IEnumerator<QueryPathSegment> GetEnumerator()
        {
            return _Segments.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Segments.GetEnumerator();
        }

        public void Add(QueryPathSegment segment)
        {
            Fosol.Common.Validation.Assert.IsNotNull(segment, "segment");

            _Segments.Add(segment);
        }

        public void Add(string segment)
        {
            _Segments.Add(new QueryPathSegment(segment));
        }

        public void RemoveAt(int segmentIndex)
        {
            _Segments.RemoveAt(segmentIndex);
        }

        public void Clear()
        {
            _Segments.Clear();
        }

        public override string ToString()
        {
            if (_Segments.Count == 0)
                return "/";

            return _Segments.Select(s => s.Value).Aggregate((a, b) => a + "/" + b);
        }
        #endregion

        #region Operators
        public static implicit operator string(QueryPath obj)
        {
            return obj.ToString();
        }

        public static explicit operator QueryPath(string value)
        {
            return new QueryPath(value);
        }
        #endregion

        #region Events
        #endregion
    }
}
