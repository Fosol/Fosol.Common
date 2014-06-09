using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// TypeElement provides a way to include an object type as a configurable option.
    /// </summary>
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
        /// <summary>
        /// get/set - The name of the type.
        /// </summary>
        [ConfigurationProperty(TypeNameKey, IsRequired = true, DefaultValue = "")]
        public virtual string TypeName
        {
            get { return (string)base[TypeNameKey]; }
            set { base[TypeNameKey] = value; }
        }

        /// <summary>
        /// get/set - Initialization data to include when creating a new instance of the specified type.
        /// </summary>
        [ConfigurationProperty(InitDataKey, DefaultValue = "")]
        public string InitData
        {
            get { return (string)base[InitDataKey]; }
            set { base[InitDataKey] = value; }
        }

        /// <summary>
        /// get - Access to the properties in the base ConfigurationElement class.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return base.Properties;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TypeElement object.
        /// </summary>
        /// <param name="baseType">Object type this configuration element is for.</param>
        public TypeElement(Type baseType)
        {
            _Properties = new ConfigurationPropertyCollection();
            _Properties.Add(_TypeNameProperty);
            _Properties.Add(_InitDataProperty);
            _BaseType = baseType;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of the specified type.
        /// </summary>
        /// <returns></returns>
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
