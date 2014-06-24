using Fosol.Common.Extensions.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Exceptions
{
    /// <summary>
    /// Extension methods for Exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        #region  Methods
        /// <summary>
        /// Provides a way to override the ToString method for exceptions and retain the original formatting and information.
        /// An Example Action below would append the value in 'anSqlStatement to the output string.
        /// desc => { desc.AppendFormat(", SqlStatement={0}", anSqlStatement);});
        /// </summary>
        /// <param name="ex">Exception to convert to a string.</param>
        /// <param name="customFieldsFormatterAction">Custom handler for other exception information you want to include in the output.</param>
        /// <returns>A full message that contains all the details of the exception.</returns>
        public static string ExceptionToString(this Exception ex, Action<StringBuilder> customFieldsFormatterAction = null)
        {
            StringBuilder description = new StringBuilder();
            description.AppendFormat("{0}: {1}", ex.GetType().Name, ex.Message);

            if (customFieldsFormatterAction != null)
                customFieldsFormatterAction(description);

            if (ex.InnerException != null)
            {
                description.AppendFormat(" ---> {0}", ex.InnerException);

                if (!string.IsNullOrEmpty(ex.StackTrace))
                    description.AppendFormat(Resources.Multilingual.Value_Exception_ToString, Environment.NewLine);
            }

            description.Append(ex.StackTrace);

            return description.ToString();
        }
        #endregion
    }
}
