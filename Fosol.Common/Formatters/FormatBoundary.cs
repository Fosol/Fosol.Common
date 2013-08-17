using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    /// <summary>
    /// Provides a way to control the boundary syntax for keywords.
    /// </summary>
    public class FormatBoundary
    {
        #region Variables
        private readonly string _Boundary;
        private readonly string _EscapeIdentifier;
        private readonly EscapePosition _EscapePosition;
        private readonly StringComparison _StringComparison;

        public enum EscapePosition
        {
            Before,
            After,
            None
        }
        #endregion

        #region Properties
        /// <summary>
        /// get - The string boundary.
        /// </summary>
        public string Boundary { get { return _Boundary; } }

        /// <summary>
        /// get - The length of the boundary.
        /// </summary>
        public int Length { get { return _Boundary.Length; } }

        /// <summary>
        /// get - The length of the boundary and escape together.
        /// </summary>
        public int EscapeLength { get { return _Boundary.Length + _EscapeIdentifier.Length; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a KeywordBoundary object.
        /// </summary>
        /// <param name="boundary"></param>
        /// <param name="stringComparison"></param>
        public FormatBoundary(string boundary, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
            : this(boundary, string.Empty, EscapePosition.None, stringComparison)
        {
        }

        /// <summary>
        /// Creates a new instance of a KeywordBoundary object.
        /// </summary>
        /// <param name="boundary"></param>
        /// <param name="escapeIdentifier"></param>
        /// <param name="escapePosition"></param>
        /// <param name="stringComparison"></param>
        public FormatBoundary(string boundary, string escapeIdentifier, EscapePosition escapePosition, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            Validation.Assert.IsNotNullOrEmpty(boundary, "boundary");
            Validation.Assert.IsNotNull(escapeIdentifier, "escapeIdentifier");
            Validation.Assert.IsTrue((escapePosition == EscapePosition.None && string.IsNullOrEmpty(escapeIdentifier)), "escapeIdentifier");
            Validation.Assert.IsTrue((escapePosition != EscapePosition.None && !string.IsNullOrEmpty(escapeIdentifier)), "escapePosition");
            _Boundary = boundary;
            _EscapeIdentifier = escapeIdentifier;
            _EscapePosition = escapePosition;
            _StringComparison = stringComparison;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Find the first index position of the boundary
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        public int IndexOfBoundaryIn(string text, int startPosition)
        {
            if (startPosition >= text.Length)
                return -1;

            var pos = text.IndexOf(_Boundary, startPosition);

            // Check if the boundary has been escaped.
            // If it's escaped continue looking or return -1.
            if (pos != -1
                && IsEscaped(text, pos))
            {
                pos = ShiftRight(text, pos, true);
                if (pos != -1)
                    return IndexOfBoundaryIn(text, pos);
            }

            return pos;
        }

        /// <summary>
        /// Move the index past the boundary position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="boundaryStartPosition"></param>
        /// <param name="isEscaped"></param>
        /// <returns></returns>
        private int ShiftRight(string text, int boundaryStartPosition, bool isEscaped)
        {
            if (isEscaped
                && _EscapePosition == EscapePosition.After)
            {
                var pos = boundaryStartPosition + _Boundary.Length + _EscapeIdentifier.Length;

                if (pos < text.Length)
                    return pos;

                return -1;
            }

            return ShiftRight(text, boundaryStartPosition);;
        }

        /// <summary>
        /// Move the index past the boundary position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="boundaryPosition"></param>
        /// <returns></returns>
        public int ShiftRight(string text, int boundaryStartPosition)
        {
            var pos = boundaryStartPosition + _Boundary.Length;

            if (pos < text.Length)
                return pos;

            return -1;
        }

        /// <summary>
        /// Move the index position to before the boundary position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="boundaryStartPosition"></param>
        /// <param name="isEscaped"></param>
        /// <returns></returns>
        private int ShiftLeft(string text, int boundaryStartPosition, bool isEscaped)
        {
            if (isEscaped
                && _EscapePosition == EscapePosition.Before)
            {
                var pos = boundaryStartPosition - _EscapeIdentifier.Length - 1;

                if (pos >= 0)
                    return pos;

                return -1;
            }

            return ShiftLeft(text, boundaryStartPosition);;
        }

        /// <summary>
        /// Move the index position to before the boundary position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="boundaryPosition"></param>
        /// <returns></returns>
        public int ShiftLeft(string text, int boundaryStartPosition)
        {
            var pos = boundaryStartPosition - 1;

            if (pos >= 0)
                return pos;

            return -1;
        }

        /// <summary>
        /// Check if the current boundary has been escaped.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="boundaryPosition"></param>
        /// <returns></returns>
        private bool IsEscaped(string text, int boundaryPosition)
        {
            if (boundaryPosition >= text.Length)
                return false;

            if (_EscapePosition == EscapePosition.Before)
            {
                // If we're at the beginning of the string return true.
                if (boundaryPosition - _EscapeIdentifier.Length < 0)
                    return false;
                else if (text.Substring(boundaryPosition - _EscapeIdentifier.Length, _EscapeIdentifier.Length).Equals(_EscapeIdentifier, _StringComparison))
                    return true;
            }
            else if (_EscapePosition == EscapePosition.After)
            {
                // If we've moved past the end of the string return false.
                if (text.Length <= boundaryPosition + _Boundary.Length + _EscapeIdentifier.Length)
                    return false;
                else if (text.Substring(boundaryPosition + _Boundary.Length, _EscapeIdentifier.Length).Equals(_EscapeIdentifier, _StringComparison))
                    return true;
            }

            return false;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
