using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Configuration.HttpHeader
{
    /// <summary>
    /// Configuration to ensure this endpoint contains the following HTTP headers.
    /// </summary>
    public sealed class EndpointElement : ConfigurationElement
    {
        #region Properties
        /// <summary>
        /// get/set - The name of the endpoint.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// get/set - 
        /// </summary>
        [ConfigurationProperty("headers", IsRequired = false, IsKey = false)]
        public HeaderCollection Headers
        {
            get { return (HeaderCollection)this["headers"]; }
            set { this["headers"] = value; }
        }
        #endregion
    }
}
