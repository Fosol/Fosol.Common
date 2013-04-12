using Fosol.Common.Behaviors.Configuration.HttpHeader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Behaviors
{
    /// <summary>
    /// Provides a way to automatically include Http Headers in WCF responses based on a configuration file.
    /// By default the configuration section name is "httpHeaders".  You can override this.
    /// </summary>
    public class HttpHeaderBehavior
        : IEndpointBehavior, IDispatchMessageInspector
    {
        #region Variables
        private const string ConfigSectionName = "httpHeaders";
        private const string ServiceName = "Service";
        private const string EndpointName = "Endpoint";
        private const string RequestUriRegex = @"/(?<Service>\w+\.svc)/(?<Endpoint>\w+)/?";
        private static HttpHeaderSection _HttpHeaderConfiguration;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a HttpHeaderBehavior object.
        /// </summary>
        public HttpHeaderBehavior()
            : this(ConfigSectionName)
        {
        }

        /// <summary>
        /// Creates a new instance of a HttpHeaderBehavior object.
        /// </summary>
        /// <param name="configSectionName">The configuration section name.</param>
        public HttpHeaderBehavior(string configSectionName)
        {
            // Only initialize once.
            if (_HttpHeaderConfiguration == null)
                _HttpHeaderConfiguration = (HttpHeaderSection)ConfigurationManager.GetSection(ConfigSectionName);
        }
        #endregion

        #region IEndpointBehavior Methods
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        /// <summary>
        /// Add the HttpHeader MessageInspector to the endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
        #endregion

        #region IDispatchMessageInspector
        /// <summary>
        /// Extract which service and endpoint this particular request is for.
        /// This information will be used to add Http Headers to the response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            var uri = request.Headers.To;
            Match match = Regex.Match(uri.AbsolutePath, RequestUriRegex, RegexOptions.IgnoreCase);
            var state = new CorrelationState();
            var endpoint = request.Properties["HttpOperationName"] as string;

            // Request contains Service and Endpoint information.  This information must be passed to the BeforeSendReply event.
            if (match.Success)
            {
                state.Properties[ServiceName] = match.Groups[ServiceName].Value;
                state.Properties[EndpointName] = endpoint;
            }

            return state;
        }

        /// <summary>
        /// Apply Http Headers to the response if the configuration has headers for the specified request.
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            HttpResponseMessageProperty httpResponseMessage;
            object httpResponseMessageObject;
            var state = correlationState as CorrelationState;

            // If the reply message already contains the HttpResponse property, add the following headers.  Otherwise add the HttpResponse to the Message Properties.
            if (reply.Properties.TryGetValue(HttpResponseMessageProperty.Name, out httpResponseMessageObject))
            {
                httpResponseMessage = httpResponseMessageObject as HttpResponseMessageProperty;

                // Only add the header if it does not exist, or is null or empty.
                if (state.Properties.ContainsKey(ServiceName) && state.Properties.ContainsKey(EndpointName))
                    foreach (HeaderElement header in _HttpHeaderConfiguration.GetHeaders(state.Properties[ServiceName] as string, state.Properties[EndpointName] as string))
                        if (string.IsNullOrEmpty(httpResponseMessage.Headers[header.Name]))
                            httpResponseMessage.Headers[header.Name] = header.Value;
            }
            else
            {
                httpResponseMessage = new HttpResponseMessageProperty();

                // Add all the headers to the message.
                if (state.Properties.ContainsKey(ServiceName) && state.Properties.ContainsKey(EndpointName))
                    foreach (HeaderElement header in _HttpHeaderConfiguration.GetHeaders(state.Properties[ServiceName] as string, state.Properties[EndpointName] as string))
                        httpResponseMessage.Headers.Add(header.Name, header.Value);

                reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessage);
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
