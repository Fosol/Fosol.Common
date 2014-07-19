using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Fosol.Common.Configuration
{
    public abstract class SpecialConfigurationElementCollection<T>
        : ConfigurationElementCollection<ConfigurationElement>
        where T : SpecialConfigurationElementType, new()
    {
        #region Variables
        private SpecialConfigurationElementType _ElementTypes;
        #endregion

        #region Properties
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        protected override string ElementName
        {
            get
            {
                return String.Empty;
            }
        }
        #endregion

        #region Constructors
        public SpecialConfigurationElementCollection()
        {
            _ElementTypes = Fosol.Common.Utilities.ReflectionUtility.ConstructObject<T>(typeof(T));
        }
        #endregion

        #region Methods
        protected override ConfigurationElement CreateNewElement()
        {
            throw new NotImplementedException();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            var element_type = _ElementTypes[elementName];

            return Fosol.Common.Utilities.ReflectionUtility.ConstructObject<ConfigurationElement>(element_type);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return base.GetElementKey(element);
        }

        protected override bool IsElementName(string elementName)
        {
            return _ElementTypes.IsElementName(elementName);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
