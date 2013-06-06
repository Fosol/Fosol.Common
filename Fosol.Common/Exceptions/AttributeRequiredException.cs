using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// Use when an attribute is expected but missing.
    /// </summary>
    public class AttributeRequiredException
        : Exception
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The Attribute Type required.
        /// </summary>
        public Type AttributeType { get; private set; }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new MissingAttributeException object.
        /// </summary>
        public AttributeRequiredException()
            : base()
        {
        }

        /// <summary>
        /// Creates a new MissingAttributeException object.
        /// </summary>
        /// <param name="attributeType">Attribute Type required.</param>
        public AttributeRequiredException(Type attributeType)
            : base(String.Format(Resources.Strings.Exception_Attribute_Required, attributeType.GetType().Name))
        {
            this.AttributeType = attributeType;
        }

        /// <summary>
        /// Creates a new MissingAttributeException object.
        /// </summary>
        /// <param name="message">Description of error.</param>
        public AttributeRequiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new MissingAttributeException object.
        /// </summary>
        /// <param name="attributeType">Attribute Type required.</param>
        /// <param name="innerException">Exception that caused this exception.</param>
        public AttributeRequiredException(Type attributeType, Exception innerException)
            : base(String.Format(Resources.Strings.Exception_Attribute_Required, attributeType.GetType().Name), innerException)
        {
            this.AttributeType = attributeType;
        }

        /// <summary>
        /// Creates a new MissingAttributeException object.
        /// </summary>
        /// <param name="message">Description of error.</param>
        /// <param name="innerException">Exception that caused this exception.</param>
        public AttributeRequiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates a new MissingAttributeException object.
        /// </summary>
        /// <param name="info">The object that holds all the exception details.</param>
        /// <param name="context">Contains contextual information about the source and destination.</param>
        public AttributeRequiredException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
