using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Utility methods to help with URL values.
    /// </summary>
    public static class UrlHelper
    {
        #region Methods
        /// <summary>
        /// Converts a relative URL value into an absolute URL value using the current HttpContext schema, host and port.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'url' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter 'url' cannot be null.</exception>
        /// <param name="url">Relative URL to convert to an absolute URL.</param>
        /// <returns>Absolute URL.</returns>
        public static string ConvertToAbsoluteUrl(string url)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(url, "url");

            Uri uri;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
            {
                if (uri.IsAbsoluteUri)
                    return url;
            }

            if (HttpContext.Current == null)
                return url;

            if (url.StartsWith("/"))
                url = url.Insert(0, "~");

            if (!url.StartsWith("~/"))
                url = url.Insert(0, "~/");

            var request = HttpContext.Current.Request.Url;
            var port = request.Port != 80 ? (":" + request.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}", request.Scheme, request.Host, port, VirtualPathUtility.ToAbsolute(url));
        }

        /// <summary>
        /// Try to convert a relative URL value into an absolute URL value using the current HttpContext schema, host and port.
        /// </summary>
        /// <param name="url">Relative URL value to convert.</param>
        /// <param name="absoluteUrl">Absolute URL value.</param>
        /// <returns>True if the relative URL can be converted into an absolute URL.</returns>
        public static bool TryToConvertToAbsoluteUrl(string url, out string absoluteUrl)
        {
            try
            {
                absoluteUrl = UrlHelper.ConvertToAbsoluteUrl(url); 
                return true;
            }
            catch
            {
                absoluteUrl = null;
                return false;
            }
        }

        /// <summary>
        /// Replaces all relative URLs within the HTML with absolute URLs based on the current request HttpContext.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceRelativeUrlsWithAbsoluteUrls(string html) 
        { 
            if (string.IsNullOrEmpty(html))         
                return html; 
            
            const string htmlPattern = "(?<attrib>\\shref|\\ssrc|\\sbackground)\\s*?=\\s*?" 
                + "(?<delim1>[\"'\\\\]{0,2})(?!#|http|ftp|mailto|javascript)" 
                + "/(?<url>[^\"'>\\\\]+)(?<delim2>[\"'\\\\]{0,2})"; 

            var htmlRegex = new Regex(htmlPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            html = htmlRegex.Replace(html, m => 
                UrlHelper.InternalReplaceRelativeUrlsWithAbsoluteUrls(
                    htmlRegex.Replace(m.Value, "${attrib}=${delim1}" 
                        + ("~/" + m.Groups["url"].Value)) + "${delim2}")
                );
 
            const string cssPattern = "@import\\s+?(url)*['\"(]{1,2}" 
                + "(?!http)\\s*/(?<url>[^\"')]+)['\")]{1,2}"; 

            var cssRegex = new Regex(cssPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            html = cssRegex.Replace(html, m => 
                UrlHelper.InternalReplaceRelativeUrlsWithAbsoluteUrls(
                    cssRegex.Replace(m.Value, "@import url(" 
                        + ("~/" + m.Groups["url"].Value)) + ")")
                ); 

            return html; 
        }

        /// <summary>
        /// Attempts to replace the relative URL with an absolute URL value.
        /// If it cannot convert the relative URL value into an absolute URL it will return the original relative URL.
        /// </summary>
        /// <param name="url">Url value to convert into an absolute URL.</param>
        /// <returns>Absolute URL value or the original url value if it cannot be converted.</returns>
        private static string InternalReplaceRelativeUrlsWithAbsoluteUrls(string url)
        {
            string absolute_url;
            if (UrlHelper.TryToConvertToAbsoluteUrl(url, out absolute_url))
                return absolute_url;

            return url;
        }
        #endregion
    }
}
