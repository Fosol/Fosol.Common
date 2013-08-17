using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    /// <summary>
    /// Base abstract class for formatters.
    /// </summary>
    /// <typeparam name="T">The Format type this formatter will create.</typeparam>
    public abstract class Formatter<T> 
        : Fosol.Common.Formatters.IFormatter
        where T : Format
    {
        #region Variables
        private readonly FormatBoundary _StartBoundary;
        private readonly FormatBoundary _EndBoundary;
        private readonly FormatBoundary _AttributeBoundary;
        #endregion

        #region Properties
        /// <summary>
        /// get - The syntax that represents the start of a format part boundary.
        /// </summary>
        public FormatBoundary StartBoundary { get { return _StartBoundary; } }

        /// <summary>
        /// get - The syntax that represents the end of a format part boundary.
        /// </summary>
        public FormatBoundary EndBoundary { get { return _EndBoundary; } }

        /// <summary>
        /// get - The syntax that represents the separation of value and attributes within a format part.
        /// </summary>
        public FormatBoundary AttributeBoundary { get { return _AttributeBoundary; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a Formatter.
        /// </summary>
        /// <param name="startBoundary"></param>
        /// <param name="endBoundary"></param>
        /// <param name="attributeBoundary"></param>
        public Formatter(FormatBoundary startBoundary, FormatBoundary endBoundary, FormatBoundary attributeBoundary)
        {
            Validation.Assert.IsNotNull(startBoundary, "startBoundary");
            Validation.Assert.IsNotNull(endBoundary, "endBoundary");
            Validation.Assert.IsNotNull(attributeBoundary, "attributeBoundary");
            _StartBoundary = startBoundary;
            _EndBoundary = endBoundary;
            _AttributeBoundary = attributeBoundary;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new Format of the specified type.
        /// </summary>
        /// <param name="format">Specially formatted string value.</param>
        /// <returns>A new Format based on type 'T'.</returns>
        public abstract T Generate(string format);
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
