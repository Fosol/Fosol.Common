﻿using Fosol.Common.Extensions.Dictionaries;
using Fosol.Common.Extensions.NameValueCollections;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// ElementParser provides a consistant way to generate ElementFormat objects that use the same boundaries.
    /// </summary>
    public sealed class ElementParser
        : Parser<Format, FormatElement>
    {
        #region Variables
        private readonly static ElementBoundary _DefaultStartBoundary = new ElementBoundary("{");
        private readonly static ElementBoundary _DefaultEndBoundary = new ElementBoundary("}");
        private readonly static ElementBoundary _DefaultAttributeBoundary = new ElementBoundary("?");
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new ElementParser object with default boundaries.
        /// </summary>
        /// <example>
        /// {name?param1=value1&param2=value2}
        /// </example>
        public ElementParser()
            : base(_DefaultStartBoundary, _DefaultEndBoundary, _DefaultAttributeBoundary)
        {
        }

        /// <summary>
        /// Creates a new ElementParser object with the specified boundaries.
        /// </summary>
        /// <param name="startBoundary"></param>
        /// <param name="endBoundary"></param>
        /// <param name="attributeBoundary"></param>
        public ElementParser(ElementBoundary startBoundary, ElementBoundary endBoundary, ElementBoundary attributeBoundary)
            : base(startBoundary, endBoundary, attributeBoundary)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parses the specially formatted text and updates the Parts collection.
        /// </summary>
        /// <param name="format">A format string with special boundary syntax that represents Element objects.</param>
        /// <returns>A new ElementFormat object from the format string.</returns>
        public override Format Parse(string format)
        {
            var elements = new List<FormatElement>();
            var pos = 0;
            do
            {
                elements.Add(NextElement(format, ref pos));
            } while (pos != -1);
            return new Format(elements);
        }

        /// <summary>
        /// Find the next FormatPart within the format.
        /// </summary>
        /// <param name="format">A format string with special boundary syntax that represents Element objects.</param>
        /// <param name="indexPosition">Index position to begin searching for elements within the format string.</param>
        /// <returns>A new Element object.</returns>
        private FormatElement NextElement(string format, ref int indexPosition)
        {
            var start = this.StartBoundary.IndexOfBoundaryIn(format, indexPosition);

            // There are no keywords found, return the raw text value.
            if (start == -1)
            {
                var element = CreateElement(format.Substring(indexPosition));
                indexPosition = -1;
                return element;
            }

            // A keyword was found but there is text before it, return the text first.
            if (start > indexPosition)
            {
                var element = CreateElement(format.Substring(indexPosition, start - indexPosition));
                indexPosition = start;
                return element;
            }

            // If we've got to this part it means the indexPosition points to a StartBoundary.
            var after_start = this.StartBoundary.ShiftRight(format, start);
            var end = this.EndBoundary.IndexOfBoundaryIn(format, after_start);

            // End boundary was not found, return the text value.
            if (end == -1)
                return CreateElement(format.Substring(start));

            var after_end = this.EndBoundary.ShiftRight(format, end);

            // Check if this end belongs to an inner start boundary (i.e. {element?value={value}}}).
            var inner_start = this.StartBoundary.IndexOfBoundaryIn(format, after_start, end - start);
            if (inner_start != -1)
            {
                end = this.EndBoundary.IndexOfBoundaryIn(format, after_end);

                // End boundary was not found, return the text value.
                if (end == -1)
                    return CreateElement(format.Substring(start));

                after_end = this.EndBoundary.ShiftRight(format, end);
            }

            // Update the indexPosition to be after this FormatPart.
            indexPosition = after_end;

            return CreateElement(format.Substring(after_start, end - after_start));
        }

        /// <summary>
        /// Creates a new Element for the specified format.
        /// </summary>
        /// <param name="format">Special syntax string that represents an Element object.</param>
        /// <returns>A new Element object.</returns>
        private FormatElement CreateElement(string format)
        {
            string name;
            NameValueCollection query = new NameValueCollection();
            var attribute_pos = this.AttributeBoundary.IndexOfBoundaryIn(format, this.StartBoundary.Length - 1);
            var is_param = format.StartsWith("@");

            // If there are no attributes, the whole part.Value becomes the name.
            if (attribute_pos == -1)
            {
                // It's using the parameter shortcut syntax.
                if (is_param)
                {
                    name = "parameter";
                    var pos = format.IndexOf("=");

                    // Check for shortcut syntax within (i.e. {@param=value}).
                    if (pos != -1)
                    {
                        query.Add("value", format.Substring(pos + 1));
                    }

                    query.Add("name", format);
                }
                else
                    name = format;
            }
            // If there are attributes, extract them and the name.
            else
            {
                query = System.Web.HttpUtility.ParseQueryString(format.Substring(attribute_pos + 1));

                // It's using the parameter shortcut syntax.
                if (is_param)
                {
                    name = "parameter";
                    query.Add("name", format.Substring(0, attribute_pos));
                }
                else
                {
                    name = format.Substring(0, attribute_pos);
                }
            }

            // Clean up all the attribute values by replacing 
            foreach (var key in query.AllKeys)
            {
                query[key] = Decode(query[key]);
            }

            return FormatElement.CreateNew(Decode(name), query);
        }

        /// <summary>
        /// Remove escaped boundaries from the text.
        /// </summary>
        /// <param name="text">Text to remove escaped boundaries from.</param>
        /// <returns>Text value without escaped boundaries</returns>
        private string Decode(string text)
        {
            return text.Replace(this.StartBoundary + this.StartBoundary, this.StartBoundary)
                .Replace(this.EndBoundary + this.EndBoundary, this.EndBoundary)
                .Replace(this.AttributeBoundary + this.AttributeBoundary, this.AttributeBoundary);
        }

        /// <summary>
        /// Generate the format string with boundary syntax for the specified ElementFormat object.
        /// </summary>
        /// <param name="format">ElementFormat object to render.</param>
        /// <returns>String value with boundary formats.</returns>
        public override string Render(Format format)
        {
            var result = new StringBuilder();
            foreach (var element in format.Elements)
            {
                // If it has attributes make sure to include them.
                if (element.Attributes.Count > 0)
                    result.Append(this.StartBoundary + element.Name + this.AttributeBoundary + element.Attributes.ToQueryString() + this.EndBoundary);
                else
                {
                    var text = element as StaticElement;
                    // If it's a StaticElement without attributes it can be rendered as a simple string without boundaries.
                    if (text != null)
                        result.Append(text.Text);
                    else
                        result.Append(this.StartBoundary + element.Name + this.EndBoundary);
                }
            }

            return result.ToString();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
