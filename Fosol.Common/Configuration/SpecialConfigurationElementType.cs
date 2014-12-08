using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    public abstract class SpecialConfigurationElementType
    {
        #region Variables
        private readonly Dictionary<string, Type> _Elements;
        #endregion

        #region Properties
        public Type this[string name]
        {
            get { return _Elements[name]; }
        }
        #endregion

        #region Constructors
        public SpecialConfigurationElementType(params Type[] configurationElementType)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(configurationElementType, "configurationElementType");

            _Elements = new Dictionary<string,Type>();

            foreach (var cet in configurationElementType)
            {
                // Make sure that each type is a ConfigurationElement.
                Fosol.Common.Validation.Assert.IsAssignable(cet, typeof(ConfigurationElement), "configurationElementType");

                // Check if they have a ConfigurationElementAttribute.
                var attr = cet.GetCustomAttributes(typeof(ConfigurationElementAttribute), false).FirstOrDefault() as ConfigurationElementAttribute;

                if (attr != null)
                    _Elements.Add(attr.AddName, cet);
                else
                    _Elements.Add(cet.Name, cet);
            }
        }
        #endregion

        #region Methods
        public bool IsElementName(string elementName)
        {
            return _Elements.ContainsKey(elementName);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
