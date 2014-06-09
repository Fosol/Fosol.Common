using System;
namespace Fosol.Common.Parsers
{
    /// <summary>
    /// IFormat is a an interface for format objects that conain a collection of elements which represent dynamic string values.
    /// </summary>
    public interface IFormat<T>
        where T : FormatElement
    {
        /// <summary>
        /// get - Collection of FormatElement objects.
        /// </summary>
        global::System.Collections.Generic.List<T> Elements { get; }

        /// <summary>
        /// Generate the dynamic output of this format.
        /// </summary>
        /// <param name="data">Information that can be used when rendering the dynamic elements.</param>
        /// <returns>A dynamic string value.</returns>
        string Render(object data);
    }
}
