using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Behaviors
{
    public sealed class HttpHeaderBehaviorExtensionElement
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
            get { return typeof(HttpHeaderBehavior); }
        }

        /// <summary>
        /// get/set - The configuration section name for the behavior.
        /// </summary>
        [ConfigurationProperty("configSectionName", DefaultValue = "httpHeaders", Options = ConfigurationPropertyOptions.None)]
        public string ConfigSectionName { get; set; }
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
            var section_name = (string)this.ElementInformation.Properties["configSectionName"].Value;
            if (!string.IsNullOrEmpty(section_name))
                return new HttpHeaderBehavior(section_name);
            else
                return new HttpHeaderBehavior();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
