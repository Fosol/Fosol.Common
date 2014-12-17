using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// Parser is an abstract class for all text parsers that convert text into objects.
    /// </summary>
    /// <typeparam name="T">The Format type this formatter will create.</typeparam>
    public abstract class Parser<T, E> 
        where T : IFormat<E>
        where E : FormatElement
    {
        #region Variables
        private readonly ElementBoundary _StartBoundary;
        private readonly ElementBoundary _EndBoundary;
        private readonly ElementBoundary _AttributeBoundary;
        #endregion

        #region Properties
        /// <summary>
        /// get - The syntax that represents the start of a format part boundary.
        /// </summary>
        public ElementBoundary StartBoundary { get { return _StartBoundary; } }

        /// <summary>
        /// get - The syntax that represents the end of a format part boundary.
        /// </summary>
        public ElementBoundary EndBoundary { get { return _EndBoundary; } }

        /// <summary>
        /// get - The syntax that represents the separation of value and attributes within a format part.
        /// </summary>
        public ElementBoundary AttributeBoundary { get { return _AttributeBoundary; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a Parser.
        /// </summary>
        /// <param name="startBoundary"></param>
        /// <param name="endBoundary"></param>
        /// <param name="attributeBoundary"></param>
        public Parser(ElementBoundary startBoundary, ElementBoundary endBoundary, ElementBoundary attributeBoundary)
        {
            Validation.Argument.Assert.IsNotNull(startBoundary, "startBoundary");
            Validation.Argument.Assert.IsNotNull(endBoundary, "endBoundary");
            Validation.Argument.Assert.IsNotNull(attributeBoundary, "attributeBoundary");
            _StartBoundary = startBoundary;
            _EndBoundary = endBoundary;
            _AttributeBoundary = attributeBoundary;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parses a string format value and creates an associated Format object.
        /// </summary>
        /// <param name="format">String format with special syntax.</param>
        /// <returns>New instance of a Format object of type T.</returns>
        public abstract T Parse(string format);

        /// <summary>
        /// Generates a string that represents the specified Format.
        /// </summary>
        /// <param name="format">Format object to render into a string.</param>
        /// <returns>String value.</returns>
        public abstract string Render(T format);
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
