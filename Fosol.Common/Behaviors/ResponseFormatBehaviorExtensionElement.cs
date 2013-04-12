using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Behaviors
{
    /// <summary>
    /// Extension object for the ResponseFormatBehavior object.
    /// </summary>
    /// <example>
    ///     <behaviors>
    ///         <endpointBehaviors>
    ///             <behavior name="ServiceEndpointBehavior">
    ///                 <responseFormatBehavior />
    ///              </behavior>
    ///         </endpointBehaviors>
    ///     </behaviors>
    ///     <extensions>
    ///         <behaviorExtensions>
    ///             <add name="responseFormatBehavior" type="Fosol.Common.Behaviors.ResponseFormatBehaviorExtensionElement, Fosol.Common"/>
    ///         </behaviorExtensions>
    ///     </extensions>
    /// </example>
    public sealed class ResponseFormatBehaviorExtensionElement
        : BehaviorExtensionElement
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The behavior Type.
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(ResponseFormatBehavior); }
        }

        /// <summary>
        /// get/set - The default WebMessageFormat to use if none is requested.
        /// </summary>
        [ConfigurationProperty("defaultFormat", DefaultValue = "Xml", Options = ConfigurationPropertyOptions.None)]
        public WebMessageFormat DefaultFormat { get; set; }

        /// <summary>
        /// get/set - The name of the query string parameter that may contain the requested format.
        /// </summary>
        [ConfigurationProperty("queryParamName", DefaultValue = "format", Options = ConfigurationPropertyOptions.None)]
        public string QueryParamName { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new ResponseFormatBehavior object.
        /// </summary>
        /// <returns>New ResponseFormatBehavior object.</returns>
        protected override object CreateBehavior()
        {
            return new ResponseFormatBehavior(
                (WebMessageFormat)this.ElementInformation.Properties["defaultFormat"].Value, 
                (string)this.ElementInformation.Properties["queryParamName"].Value);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
