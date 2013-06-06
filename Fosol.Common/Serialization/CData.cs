using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Fosol.Common.Serialization
{
    /// <summary>
    /// Provides the ability for DataContract objects to be serialized with CDATA notation.
    /// By default DataContracts can only serialize and deserialize node attributes.
    /// </summary>
    [Serializable]
    public sealed class CData : IXmlSerializable
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The value within the CData node.
        /// </summary>
        public string Value { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Return null.
        /// </summary>
        /// <returns>null.</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Return "xs:string" as the type in schema generation.
        /// </summary>
        /// <param name="xs"></param>
        /// <returns></returns>
        public static XmlQualifiedName GenerateSchema(XmlSchemaSet xs)
        {
            return XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String).QualifiedName;
        }

        /// <summary>
        /// Reads the values from the following Xml;
        /// <Node/> => ""
        /// <Node></Node> => ""
        /// <Node>value</Node> => "value"
        /// <Node><![CDATA[value]]></Node> => "value"
        /// </summary>
        /// <param name="reader">XmlReader object.</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.IsEmptyElement)
                Value = "";
            else
            {
                reader.Read();

                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        // The value is empty.
                        Value = ""; 
                        break;
                    case XmlNodeType.Text:
                    case XmlNodeType.CDATA:
                        Value = reader.ReadContentAsString();
                        break;
                    default:
                        throw new InvalidOperationException(Resources.Strings.Exception_Invalid_CData);
                }
            }
        }

        /// <summary>
        /// Writes this Xml for the following values;
        /// "" => <Node/>
        /// "value" => <Node><![DATA[value]]></Node>
        /// </summary>
        /// <param name="writer">XmlWriter object.</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                writer.WriteCData(Value);
            }
        }

        /// <summary>
        /// Returns the actual value within the CDATA nodes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value;
        }

        /// <summary>
        /// Converts CData to a string.
        /// </summary>
        /// <param name="value">CData object.</param>
        /// <returns>The value of the CData object.</returns>
        public static implicit operator string(CData value)
        {
            return value == null ? null : value.Value;
        }

        /// <summary>
        /// Converts a string into CData.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns>A new CData object.</returns>
        public static implicit operator CData(string value)
        {
            return value == null ? null : new CData { Value = value };
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
