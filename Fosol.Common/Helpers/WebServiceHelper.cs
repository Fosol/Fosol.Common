using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Useful methods for web services.
    /// </summary>
    public static class WebServiceHelper
    {
        #region Methods
        /// <summary>
        /// Sets the WebOperationContext.Current.OutgoingResponse.Format to the specified WebMessageFormat value.
        /// First it will check if the WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters contains a 'format' parameter.
        /// If the query parameter contains a 'format' value it will use it.
        /// If the query parameter does not contain a 'format' value it will use the 'defaultFormat' value.
        /// If the format value specified is invalid it will throw an exception.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "defaultFormat" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Paramter "defaultFormat" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "defaultFormat" is an invalid WebMessageFormat value.</exception>
        /// <param name="defaultFormat">The default WebMessageFormat value if one is not supplied in the query parameters.</param>
        /// <param name="queryParamName">The query string parameter name used to specify the WebMessageFormat (default is 'format').</param>
        /// <returns>The WebMessageFormat that the OutgoingResponse will use.</returns>
        public static WebMessageFormat SetResponseFormat(string defaultFormat = null, string queryParamName = "format")
        {
            // Check if the parameter exists in the query, if it does, use it.
            var query_format = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters[queryParamName];
            if (!string.IsNullOrEmpty(query_format))
                defaultFormat = query_format;

            // If no format has been set, throw exception.
            Validation.Parameter.AssertNotNullOrEmpty(defaultFormat, "defaultFormat");

            WebMessageFormat format = WebMessageFormat.Xml;

            // If the format is invalid throw exception.
            Validation.Parameter.AssertIsValue(Enum.TryParse<WebMessageFormat>(defaultFormat, true, out format), new[] { true }, "defaultFormat");
            WebOperationContext.Current.OutgoingResponse.Format = format;

            return format;
        }
        #endregion
    }
}
