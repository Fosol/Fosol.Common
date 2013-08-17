using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    /// <summary>
    /// KeywordFormatter provides a consistant way to generate KeywordFormat objects that use the same boundaries.
    /// </summary>
    public sealed class KeywordFormatter
        : Formatter<KeywordFormat>
    {
        #region Variables
        private readonly static FormatBoundary _DefaultStartBoundary = new FormatBoundary("{", "{", FormatBoundary.EscapePosition.Before);
        private readonly static FormatBoundary _DefaultEndBoundary = new FormatBoundary("}", "}", FormatBoundary.EscapePosition.After);
        private readonly static FormatBoundary _DefaultAttributeBoundary = new FormatBoundary("?", "?", FormatBoundary.EscapePosition.After);
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new KeywordFormatter object with default boundaries.
        /// </summary>
        /// <example>
        /// {name?param1=value1&param2=value2}
        /// </example>
        public KeywordFormatter()
            : base(_DefaultStartBoundary, _DefaultEndBoundary, _DefaultAttributeBoundary)
        {
        }

        /// <summary>
        /// Creates a new KeywordFormatter object with the specified boundaries.
        /// </summary>
        /// <param name="startBoundary"></param>
        /// <param name="endBoundary"></param>
        /// <param name="attributeBoundary"></param>
        public KeywordFormatter(FormatBoundary startBoundary, FormatBoundary endBoundary, FormatBoundary attributeBoundary)
            : base(startBoundary, endBoundary, attributeBoundary)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a new instance of a KeywordFormat with the specified format syntax.
        /// </summary>
        /// <param name="format">Special syntax to create keywords.</param>
        /// <returns>A new instance of a KeywordFormat.</returns>
        public override KeywordFormat Generate(string format)
        {
            return new KeywordFormat(this, format);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
