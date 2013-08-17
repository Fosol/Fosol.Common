using System;
namespace Fosol.Common.Formatters
{
    /// <summary>
    /// Interface for Formatter objects.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// get - Syntax that identifies the start of a format part.
        /// </summary>
        FormatBoundary StartBoundary { get; }

        /// <summary>
        /// get - Syntax that identifies the end of a format part.
        /// </summary>
        FormatBoundary EndBoundary { get; }

        /// <summary>
        /// get - Syntax that identifies the separation of attributes within a format part.
        /// </summary>
        FormatBoundary AttributeBoundary { get; }
    }
}
