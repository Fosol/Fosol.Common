using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters
{
    /// <summary>
    /// A Format provides a way to generate an output
    /// </summary>
    public abstract class Format
    {
        #region Variables
        private readonly IFormatter _Formatter;
        private readonly string _Text;
        private readonly List<FormatPart> _Parts = new List<FormatPart>();
        #endregion

        #region Properties
        protected IFormatter Formatter { get { return _Formatter; } }

        /// <summary>
        /// get - Original string value used to initialize this formatter.
        /// </summary>
        protected string Text { get { return _Text; } }

        /// <summary>
        /// get - Collection of FormatPart objects.
        /// </summary>
        public List<FormatPart> Parts { get { return _Parts; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a Format object.
        /// </summary>
        /// <param name="text">Specially formatted string value.</param>
        public Format(IFormatter formatter, string text)
        {
            Validation.Assert.IsNotNull(formatter, "formatter");
            Validation.Assert.IsNotNullOrEmpty(text, "text");
            _Formatter = formatter;
            _Text = text;

            Parse();
            Compile();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compiles the Format syntax into something that can be used to generate the output string created in the Render() method.
        /// This method is called in the base constructor.
        /// </summary>
        protected abstract void Compile();

        /// <summary>
        /// Parses the specially formatted text and updates the Parts collection.
        /// </summary>
        private void Parse()
        {
            var pos = 0;
            do
            {
                this.Parts.Add(Next(this.Formatter.StartBoundary, this.Formatter.EndBoundary, this.Text, ref pos));
            } while (pos != -1);
        }

        /// <summary>
        /// Find the next FormatPart within the format.
        /// </summary>
        /// <param name="startBoundary"></param>
        /// <param name="endBoundary"></param>
        /// <param name="format"></param>
        /// <param name="indexPosition"></param>
        /// <returns></returns>
        private static FormatPart Next(FormatBoundary startBoundary, FormatBoundary endBoundary, string format, ref int indexPosition)
        {
            var start = startBoundary.IndexOfBoundaryIn(format, indexPosition);

            // There are no keywords found, return the raw text value.
            if (start == -1)
            {
                var part = new FormatPart(format.Substring(indexPosition));
                indexPosition = -1;
                return part;
            }

            // A keyword was found but there is text before it, return the text first.
            if (start > indexPosition)
            {
                var before_start = startBoundary.ShiftLeft(format, start);
                var part = new FormatPart(format.Substring(indexPosition, before_start - indexPosition));
                indexPosition = start;
                return part;
            }

            // If we've got to this part it means the indexPosition starts as a StartBoundary.
            var after_start = startBoundary.ShiftRight(format, start);
            var end = endBoundary.IndexOfBoundaryIn(format, after_start);
            var after_end = endBoundary.ShiftRight(format, end);

            // Update the indexPosition to be after this FormatPart.
            indexPosition = after_end;

            return new FormatPart(format.Substring(after_start, after_end - after_start));
        }

        /// <summary>
        /// Generates a output string based on the format.
        /// </summary>
        /// <param name="data">Information to use when generating the output string.</param>
        /// <returns>A generated string value based on the format syntax.</returns>
        public abstract string Render(object data);

        /// <summary>
        /// Returns the original format syntax use to initialize this Format.
        /// </summary>
        /// <returns>The original format syntax use to initialize this Format.</returns>
        public override string ToString()
        {
            return this.Text;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
