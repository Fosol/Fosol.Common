using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Fosol.Common.Utilities
{
    /// <summary>
    /// Utility methods to help with URL values.
    /// </summary>
    public static class UrlUtility
    {
        #region Methods
        /// <summary>
        /// Determines whether the specified URL is relative.
        /// </summary>
        /// <param name="url">URL value to test.</param>
        /// <returns>True if the URL is relative.</returns>
        public static bool IsRelative(string url)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(url, "url");

            return !UrlUtility.HasSchema(url) && !UrlUtility.IsRooted(url);
        }

        /// <summary>
        /// Determines whether the specified URL has a schema defined.
        /// </summary>
        /// <param name="url">URL value to test.</param>
        /// <returns>True if the URL has a schema.</returns>
        public static bool HasSchema(string url)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(url, "url");

            var colon = url.IndexOf(':');
            if (colon == -1)
                return false;

            int slash = url.IndexOf('/');
            return slash == -1 || colon < slash;
        }

        /// <summary>
        /// Determines whether the specified URL is rooted.
        /// </summary>
        /// <param name="url">URL value to test.</param>
        /// <returns>True if the URL is rooted.</returns>
        public static bool IsRooted(string url)
        {
            return string.IsNullOrEmpty(url) || url[0] == '/' || url[0] == '\\';
        }

        /// <summary>
        /// Converts a relative URL value into an absolute URL value using the current HttpContext schema, host and port.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'url' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter 'url' cannot be null.</exception>
        /// <param name="relative">Relative URL to convert to an absolute URL.</param>
        /// <returns>Absolute URL.</returns>
        public static string ToAbsolute(string relative)
        {
            return UrlUtility.ToAbsolute(HttpContext.Current, relative);
        }

        /// <summary>
        /// Converts a relative URL value into an absolute URL value using the specified HttpContext schema, host and port.
        /// </summary>
        /// <param name="context">HttpContext object.</param>
        /// <param name="relative">Relative URL to convert to an absolute URL.</param>
        /// <returns>Absolute URL.</returns>
        public static string ToAbsolute(HttpContext context, string relative)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(relative, "relative");
            Fosol.Common.Validation.Assert.IsNotNull(context, "context");

            Uri uri;
            if (Uri.TryCreate(relative, UriKind.RelativeOrAbsolute, out uri))
            {
                if (uri.IsAbsoluteUri)
                    return relative;
            }

            if (relative.StartsWith("/"))
                relative = relative.Insert(0, "~");

            if (!relative.StartsWith("~/"))
                relative = relative.Insert(0, "~/");

            var request = context.Request.Url;
            var port = request.Port != 80 ? (":" + request.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}", request.Scheme, request.Host, port, VirtualPathUtility.ToAbsolute(relative));
        }

        /// <summary>
        /// Converts the relative URL value into an absolute URL using the current application domain virtual path.
        /// </summary>
        /// <param name="relative">Relative URL to convert to an absolute URL.</param>
        /// <returns>Absolute URL.</returns>
        public static string ToAbsoluteForAppDomain(string relative)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(relative, "relative");


            var virtual_domain_path = HttpRuntime.AppDomainAppVirtualPath;

            if (relative[0] == '~' && relative.Length == 1)
                return virtual_domain_path;

            // Remove last slash.
            if (relative[relative.Length - 1] == '/')
                relative = relative.Substring(0, relative.Length - 1);

            return virtual_domain_path + "/" + relative;
        }

        /// <summary>
        /// Try to convert a relative URL value into an absolute URL value using the current HttpContext schema, host and port.
        /// </summary>
        /// <param name="relative">Relative URL value to convert.</param>
        /// <param name="absolute">Absolute URL value.</param>
        /// <returns>True if the relative URL can be converted into an absolute URL.</returns>
        public static bool TryToConvertToAbsolute(string relative, out string absolute)
        {
            try
            {
                absolute = UrlUtility.ToAbsolute(relative); 
                return true;
            }
            catch
            {
                absolute = null;
                return false;
            }
        }

        /// <summary>
        /// Replaces all relative URLs within the HTML with absolute URLs based on the current request HttpContext.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceRelativeWithAbsolute(string html) 
        { 
            if (string.IsNullOrEmpty(html))         
                return html; 
            
            const string htmlPattern = "(?<attrib>\\shref|\\ssrc|\\sbackground)\\s*?=\\s*?" 
                + "(?<delim1>[\"'\\\\]{0,2})(?!#|http|ftp|mailto|javascript)" 
                + "/(?<url>[^\"'>\\\\]+)(?<delim2>[\"'\\\\]{0,2})"; 

            var htmlRegex = new Regex(htmlPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            html = htmlRegex.Replace(html, m => 
                UrlUtility.InternalReplaceRelativeWithAbsolute(
                    htmlRegex.Replace(m.Value, "${attrib}=${delim1}" 
                        + ("~/" + m.Groups["url"].Value)) + "${delim2}")
                );
 
            const string cssPattern = "@import\\s+?(url)*['\"(]{1,2}" 
                + "(?!http)\\s*/(?<url>[^\"')]+)['\")]{1,2}"; 

            var cssRegex = new Regex(cssPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            html = cssRegex.Replace(html, m =>
                UrlUtility.InternalReplaceRelativeWithAbsolute(
                    cssRegex.Replace(m.Value, "@import url(" 
                        + ("~/" + m.Groups["url"].Value)) + ")")
                ); 

            return html; 
        }

        /// <summary>
        /// Attempts to replace the relative URL with an absolute URL value.
        /// If it cannot convert the relative URL value into an absolute URL it will return the original relative URL.
        /// </summary>
        /// <param name="relative">Url value to convert into an absolute URL.</param>
        /// <returns>Absolute URL value or the original url value if it cannot be converted.</returns>
        private static string InternalReplaceRelativeWithAbsolute(string relative)
        {
            string absolute_url;
            if (UrlUtility.TryToConvertToAbsolute(relative, out absolute_url))
                return absolute_url;

            return relative;
        }
        #endregion
    }
}
