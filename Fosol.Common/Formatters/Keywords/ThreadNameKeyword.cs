using Fosol.Common.Formatters.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// Renders the current thread name.
    /// </summary>
    [FormatKeyword("threadName")]
    public sealed class ThreadNameKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ThreadNameKeyword object.
        /// </summary>
        public ThreadNameKeyword()
            : base()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the thread name.
        /// </summary>
        /// <param name="data">Information object containing data for the keyword.</param>
        /// <returns>Thread name.</returns>
        public override string Render(object data)
        {
            return Thread.CurrentThread.Name;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}