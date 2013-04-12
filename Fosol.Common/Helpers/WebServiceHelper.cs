using Fosol.Common.Extensions.Strings;
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
        /// <exception cref="System.ArgumentNullException">Paramter "defaultFormat" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "defaultFormat" is an invalid WebMessageFormat value.</exception>
        /// <param name="defaultFormat">The default WebMessageFormat value if one is not supplied in the query parameters.</param>
        /// <param name="queryParamName">The query string parameter name used to specify the WebMessageFormat (default is 'format').</param>
        /// <returns>The WebMessageFormat that the OutgoingResponse will use.</returns>
        public static WebMessageFormat SetResponseFormat(WebMessageFormat defaultFormat = WebMessageFormat.Xml, string queryParamName = "format")
        {
            var format = defaultFormat;
            var web_operation_context = WebOperationContext.Current;

            // An Accept header can contain multiple content types.
            var accept_header = web_operation_context.IncomingRequest.Headers["Accept"];
            if (!string.IsNullOrEmpty(accept_header))
            {
                var accepts = accept_header.Split(',');

                // A valid type is [json|xml].
                foreach (var accept in accepts)
                {
                    var type = accept.Trim();
                    if (type.EndsWith("json", StringComparison.InvariantCulture) && type.Length >= 4 && "/+".Contains(type.Substring(type.Length - 5, 1)))
                    {
                        format = WebMessageFormat.Json;
                        break;
                    }
                    else if (type.EndsWith("xml", StringComparison.InvariantCulture) && type.Length >= 3 && "/+".Contains(type.Substring(type.Length - 4, 1)))
                    {
                        format = WebMessageFormat.Xml;
                        break;
                    }
                }
            }

            // Check if the parameter exists in the query, if it does, use it.
            // Valid query format values [Xml|Json].
            var query_format = web_operation_context.IncomingRequest.UriTemplateMatch.QueryParameters[queryParamName];
            // If the format is invalid throw exception.
            if (!string.IsNullOrEmpty(query_format))
                Validation.Assert.IsValue<bool>(Enum.TryParse<WebMessageFormat>(query_format, true, out format), new bool[] { true }, "format");

            web_operation_context.OutgoingResponse.Format = format;
            return format;
        }
        #endregion
    }
}
