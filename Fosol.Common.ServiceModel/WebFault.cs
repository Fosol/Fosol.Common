using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Web;

namespace Fosol.Common.ServiceModel
{
    /// <summary>
    /// WebFault provides a common DataContract for WebFaultException responses.
    /// </summary>
    [DataContract(Name = "WebFault", Namespace = "www.fosol.ca")]
    [Description("Web service fault DataContract.")]
    public class WebFault
    {
        #region Variables
        private string _Message;
        private HttpStatusCode _StatusCode;
        private Exception _InnerException;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - A description of the fault that was raise.
        /// </summary>
        [DataMember(Name = "Message", IsRequired = true)]
        [Description("A desription of the fault that was raise.")]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        /// <summary>
        /// get/set - The HTTP status code for the response.
        /// </summary>
        [DataMember(Name = "StatusCode", IsRequired = true)]
        [Description("The HTTP status code for the response.")]
        public HttpStatusCode StatusCode
        {
            get { return _StatusCode; }
            set { _StatusCode = value; }
        }

        /// <summary>
        /// get - The inner exception that was the origin of this WebFault.
        /// </summary>
        public Exception InnerException
        {
            get { return _InnerException; }
            private set { _InnerException = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a WebFault object.
        /// </summary>
        /// <param name="message">A description of the fault that was raised.</param>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        public WebFault(string message, HttpStatusCode statusCode)
        {
            Validation.Assert.IsNotNullOrEmpty(message, "message");

            _Message = message;
            _StatusCode = statusCode;
        }

        /// <summary>
        /// Creates a new instance of a WebFault object.
        /// </summary>
        /// <param name="message">A description of the fault that was raised.</param>
        /// <param name="innerException">Origin of the exception that threw this WebFault.</param>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        public WebFault(string message, Exception innerException, HttpStatusCode statusCode)
            : this(message, statusCode)
        {
            _InnerException = innerException;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Throws a WebFaultException for this WebFault object.
        /// </summary>
        /// <exception cref="System.ServiceModel.Web.WebFaultException">Returns this WebFault in the response.</exception>
        public void RaiseFault()
        {
            throw new WebFaultException<WebFault>(this, this.StatusCode);
        }

        /// <summary>
        /// Creates a new instance of a WebFault object and throws a WebFaultException for the WebFault object.
        /// </summary>
        /// <exception cref="System.ServiceModel.Web.WebFaultException">Returns a WebFault in the response.</exception>
        /// <param name="message">Description of the fault that will be raised.</param>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        public static void RaiseFault(string message, HttpStatusCode statusCode)
        {
            throw new WebFaultException<WebFault>(new WebFault(message, statusCode), statusCode);
        }

        /// <summary>
        /// Creates a new instance of a WebFault object and throws a WebFaultException for the WebFault object.
        /// </summary>
        /// <exception cref="System.ServiceModel.Web.WebFaultException">Returns a WebFault in the response.</exception>
        /// <param name="message">Description of the fault that will be raised.</param>
        /// <param name="innerException">Origin of the exception that threw this WebFault.</param>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        public static void RaiseFault(string message, Exception innerException, HttpStatusCode statusCode)
        {
            throw new WebFaultException<WebFault>(new WebFault(message, innerException, statusCode), statusCode);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
