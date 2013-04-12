using Fosol.Common.Extensions.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Exceptions
{
    /// <summary>
    /// Extension methods for Exceptions.
    /// </summary>
    public static class ExceptionExtensions
    {
        #region  Methods
        /// <summary>
        /// Provides a way to override the ToString method for exceptions and retain the original formatting and information.
        /// An Example Action below would append the value in 'anSqlStatement to the output string.
        /// desc => { desc.AppendFormat(", SqlStatement={0}", anSqlStatement);});
        /// </summary>
        /// <param name="ex">Exception to convert to a string.</param>
        /// <param name="customFieldsFormatterAction">Custom handler for other exception information you want to include in the output.</param>
        /// <returns>A full message that contains all the details of the exception.</returns>
        public static string ExceptionToString(this Exception ex, Action<StringBuilder> customFieldsFormatterAction = null)
        {
            StringBuilder description = new StringBuilder();
            description.AppendFormat("{0}: {1}", ex.GetType().Name, ex.Message);

            if (customFieldsFormatterAction != null)
                customFieldsFormatterAction(description);

            if (ex.InnerException != null)
            {
                description.AppendFormat(" ---> {0}", ex.InnerException);

                if (!string.IsNullOrEmpty(ex.StackTrace))
                    description.AppendFormat(Resources.Strings.Value_ExceptionToString, Environment.NewLine);
            }

            description.Append(ex.StackTrace);

            return description.ToString();
        }

        /// <summary>
        /// This method provides a way to resolve a very unique and annoying issue.
        /// When you create a Client application that directly references a WCF library project any FaultExceptions will be thrown as ProtocolExceptions.
        /// This is because the Client is expecting to deserialize a specific type, but the FaultException is a different DataContract.
        /// You can entirely ignore these odd issues by using a Service Reference within your project, but this requires a WSDL connection option.
        /// If the original exception in the response is deserialized it will be rethrown as the appropraite WebFaultException.
        /// If the original exception in the response is unworkable it will be rethrown inside a WebFaultException.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Exception parameter must not be null.</exception>
        /// <exception cref="System.ServiceModel.Web.WebFaultException<TServiceFault>">If the original exception response is deserialized it will be rethrown as the appropraite WebFaultException.</exception>
        /// <exception cref="System.ServiceModel.Web.WebFaultException<System.Runtime.Serialization.SerializationException>">If unable to parse the response into TServiceFault.</exception>
        /// <exception cref="System.ServiceModel.Web.WebFaultException">If unable to do anything with the exception it will simply rethrow it as a WebFaultException.</exception>
        /// <typeparam name="TServiceFault">Type of Fault object the response should be deserialized into.</typeparam>
        /// <param name="exception">The original WCF exception that threw a ProtocolException.</param>
        public static void HandleWCFException<TServiceFault>(this Exception exception)
            where TServiceFault : class
        {
            Validation.Assert.IsNotNull(exception, "exception");

            TServiceFault fault;
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            if (exception.InnerException != null)
            {
                var web_exception = exception.InnerException as WebException;
                if (web_exception != null)
                {
                    if (web_exception.Response is HttpWebResponse)
                        status = ((HttpWebResponse)web_exception.Response).StatusCode;

                    // REST uses the HTTP procol status codes to communicate errors that happens on the service side. 
                    // This means if we have a teller service and you need to supply username and password to login 
                    // and you do not supply the password, a possible scenario is that you get a 400 - Bad request. 
                    // However it is still possible that the expected type is returned so it would have been possible  
                    // to process the response - instead it will manifest as a ProtocolException on the client side. 
                    if (exception is ProtocolException)
                    {
                        var response_stream = web_exception.Response.GetResponseStream();
                        if (response_stream != null && response_stream.CanRead && response_stream.Length > 0)
                        {
                            try
                            {
                                if (web_exception.Response.ContentType.Contains("application/json"))
                                    fault = (TServiceFault)Serialization.DataContractJsonHelper.GetSerializer(typeof(TServiceFault)).ReadObject(response_stream);
                                else
                                    fault = (TServiceFault)Serialization.DataContractHelper.GetSerializer(typeof(TServiceFault)).ReadObject(response_stream);

                                if (fault != null)
                                    throw new WebFaultException<TServiceFault>(fault, status);
                            }
                            catch (SerializationException ex)
                            {
                                throw new WebFaultException<SerializationException>(ex, status);
                            }
                        }
                    }
                }
            }
            throw new WebFaultException<Exception>(exception, status);
        }

        /// <summary>
        /// Handle FaultExceptions when they are thrown as ProtocolExceptions (due to framework issues).
        /// This method passes the FaultException to the serviceResultHandler action.
        /// 
        /// This method provides a way to resolve a very unique and annoying issue.
        /// When you create a Client application that directly references a WCF library project any FaultExceptions will be thrown as ProtocolExceptions.
        /// This is because the Client is expecting to deserialize a specific type, but the FaultException is a different DataContract.
        /// You can entirely ignore these odd issues by using a Service Reference within your project, but this requires a WSDL connection option.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Exception parameter must not be null.</exception>
        /// <exception cref="System.ServiceModel.Web.WebFaultException<TServiceFault>">If the original exception response is deserialized it will be rethrown as the appropraite WebFaultException.</exception>
        /// <exception cref="System.ServiceModel.Web.WebFaultException<System.Runtime.Serialization.SerializationException>">If unable to parse the response into TServiceFault.</exception>
        /// <exception cref="System.ServiceModel.Web.WebFaultException">If unable to do anything with the exception it will simply rethrow it as a WebFaultException.</exception>
        /// <typeparam name="TServiceResult"></typeparam>
        /// <typeparam name="TServiceFault">Type of Fault object the response should be deserialized into.</typeparam>
        /// <param name="exception">The original WCF exception that threw a ProtocolException.</param>
        /// <param name="serviceResultHandler"></param>
        /// <param name="serviceFaultHandler"></param>
        /// <param name="exceptionHandler"></param>
        public static void HandleWCFException<TServiceResult, TServiceFault>(this Exception exception,
            Action<TServiceResult> serviceResultHandler,
            Action<TServiceFault> serviceFaultHandler = null,
            Action<Exception> exceptionHandler = null)
            where TServiceFault : class
        {
            var serviceResultOrServiceFaultHandled = false;

            Validation.Assert.IsNotNull(exception, "exception");
            Validation.Assert.IsNotNull(serviceResultHandler, "serviceResultHandler");

            // REST uses the HTTP procol status codes to communicate errors that happens on the service side. 
            // This means if we have a teller service and you need to supply username and password to login 
            // and you do not supply the password, a possible scenario is that you get a 400 - Bad request. 
            // However it is still possible that the expected type is returned so it would have been possible  
            // to process the response - instead it will manifest as a ProtocolException on the client side. 
            var protocol_exception = exception as ProtocolException;
            if (protocol_exception != null)
            {
                var web_exception = protocol_exception.InnerException as WebException;
                if (web_exception != null)
                {
                    var response_stream = web_exception.Response.GetResponseStream();
                    if (response_stream != null && response_stream.CanRead && response_stream.Length > 0)
                    {
                        try
                        {
                            // Debugging code to be able to see the response in clear text 
                            //System.Diagnostics.Debug.Write(responseStream.WriteToString(Encoding.UTF8));

                            // Try to deserialize the returned XML to the expected result type (TServiceResult) 
                            TServiceResult response;
                            if (web_exception.Response.ContentType.Contains("application/json"))
                                response = (TServiceResult)Serialization.DataContractJsonHelper.GetSerializer(typeof(TServiceResult)).ReadObject(response_stream);
                            else
                                response = (TServiceResult)Serialization.DataContractHelper.GetSerializer(typeof(TServiceResult)).ReadObject(response_stream);
                            serviceResultHandler(response);
                            serviceResultOrServiceFaultHandled = true;
                        }
                        catch (SerializationException)
                        {
                            // This happens if we try to deserialize the responseStream to type TServiceResult 
                            // when an error occured on the service side. An service side error serialized object  
                            // is not deserializable into a TServiceResult 

                            // Reset responseStream to beginning and deserialize to a TServiceError instead 
                            response_stream.Seek(0, SeekOrigin.Begin);

                            TServiceFault serviceFault;
                            if (web_exception.Response.ContentType.Contains("application/json"))
                                serviceFault = (TServiceFault)Serialization.DataContractJsonHelper.GetSerializer(typeof(TServiceFault)).ReadObject(response_stream);
                            else
                                serviceFault = (TServiceFault)Serialization.DataContractHelper.GetSerializer(typeof(TServiceFault)).ReadObject(response_stream);

                            if (serviceFaultHandler != null && serviceFault != null)
                            {
                                serviceFaultHandler(serviceFault);
                                serviceResultOrServiceFaultHandled = true;
                            }
                            else if (serviceFaultHandler == null && serviceFault != null)
                            {
                                throw new ServiceModel.ServiceException<TServiceFault>(serviceFault);
                            }
                        }
                    }
                }
            }

            // If we have not handled the serviceResult or the serviceFault then we have to pass it on to the exceptionHandler delegate 
            if (!serviceResultOrServiceFaultHandled && exceptionHandler != null)
            {
                exceptionHandler(exception);
            }
            else if (!serviceResultOrServiceFaultHandled && exceptionHandler == null)
            {
                // Unable to handle and no exceptionHandler passed in throw exception to be handled at a higher level 
                throw exception;
            }
        }
        #endregion
    }
}
