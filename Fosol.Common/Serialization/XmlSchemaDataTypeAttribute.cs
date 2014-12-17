using Fosol.Common.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Serialization
{
    /// <summary>
    /// DataTypeAttribute provides a way to provide the XML schema datatype name value in the correct case.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class XmlSchemaDataTypeAttribute
        : Attribute
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The name of the datatype in the correct case.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// get - Uri to this XML schema datatype.
        /// </summary>
        public Uri Uri { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a XmlSchemaDataTypeAttribute.
        /// </summary>
        /// <param name="name">The name of the datatype in the correct case.</param>
        internal XmlSchemaDataTypeAttribute(string name)
            : this(new Uri("http://www.w3.org/2001/XMLSchema#" + name), name)
        {

        }

        /// <summary>
        /// Creates a new instances of a XmlSchemaDataTypeAttribute.
        /// </summary>
        /// <param name="uri">URI to the XML schema datatype.</param>
        /// <param name="name">The name of the datatype in the correct case.</param>
        public XmlSchemaDataTypeAttribute(Uri uri, string name)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNull(uri, "uri");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(name, "name");
            this.Uri = uri;
            this.Name = name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the DataTypeAttribute for the specified DataType enum value.
        /// </summary>
        /// <param name="dataType">DataType enum value.</param>
        /// <returns>DataTypeAttribute object.</returns>
        public static XmlSchemaDataTypeAttribute Get(XmlSchemaDataType dataType)
        {
            return dataType.GetCustomAttribute<XmlSchemaDataTypeAttribute>(false);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
