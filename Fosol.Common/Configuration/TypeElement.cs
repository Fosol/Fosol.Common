using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    public class TypeElement
        : ConfigurationElement
    {
        #region Variables
        protected const string TypeNameKey = "type";
        protected const string InitDataKey = "initializeData";
        private static readonly ConfigurationProperty _TypeNameProperty = new ConfigurationProperty(TypeNameKey, typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);
        private static readonly ConfigurationProperty _InitDataProperty = new ConfigurationProperty(InitDataKey, typeof(string), string.Empty, ConfigurationPropertyOptions.None);
        protected Type _BaseType;
        private ConfigurationPropertyCollection _Properties;
        protected object _RuntimeObject;
        #endregion

        #region Properties
        [ConfigurationProperty(TypeNameKey, IsRequired = true, DefaultValue = "")]
        public virtual string TypeName
        {
            get { return (string)base[TypeNameKey]; }
            set { base[TypeNameKey] = value; }
        }

        [ConfigurationProperty(InitDataKey, DefaultValue = "")]
        public string InitData
        {
            get { return (string)base[InitDataKey]; }
            set { base[InitDataKey] = value; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return base.Properties;
            }
        }
        #endregion

        #region Constructors
        public TypeElement(Type baseType)
        {
            _Properties = new ConfigurationPropertyCollection();
            _Properties.Add(_TypeNameProperty);
            _Properties.Add(_InitDataProperty);
            _BaseType = baseType;
        }
        #endregion

        #region Methods
        protected object BaseConstructObject()
        {
            if (_RuntimeObject != null)
                _RuntimeObject = Helpers.ReflectionHelper.ConstructObject<object>(this.TypeName, _BaseType, this.InitData);

            return _RuntimeObject;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
