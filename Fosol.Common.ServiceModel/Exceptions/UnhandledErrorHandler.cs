﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.ServiceModel.Exceptions
{
    /// <summary>
    /// UnhandledErrorHandler provides a way to capture and handle unhandled exceptions.
    /// If an Handler method has been provided it will send the exception to the specified method handler, otherwise it will only Debug.WriteLine(Exception).
    /// </summary>
    public sealed class UnhandledErrorHandler
        : IErrorHandler
    {
        #region Variables
        Func<Exception, bool> _ErrorHandler;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an UnhandledErrorHandler object.
        /// </summary>
        internal UnhandledErrorHandler()
        {
        }

        /// <summary>
        /// Creates a new instance of an UnhanldedErrorHandler object.
        /// </summary>
        /// <param name="handler">Method to receive the exception.</param>
        public UnhandledErrorHandler(Func<Exception, bool> handler)
        {
            _ErrorHandler = handler;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends the exception to the specified Action (if provided), otherwise it only Debug.WriteLine(error).
        /// </summary>
        /// <param name="error">Exception to handle.</param>
        /// <returns>True if the error was handled.</returns>
        public bool HandleError(Exception error)
        {
            if (_ErrorHandler != null)
                return _ErrorHandler(error);
            else
                Debug.WriteLine(error);

            return false;
        }

        /// <summary>
        /// Creates a Message that contains a fault contract which will describe the exception that occured.
        /// </summary>
        /// <param name="error">Exception that was not handled.</param>
        /// <param name="version">MessageVersion of the SOAP message.</param>
        /// <param name="fault">Message that will be sent containing a fault contract.</param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            // Any exception that is already a FaultException is assumed to be already handled.
            if (!(error is FaultException))
            {
                var web_fault = (WebFaultException<WebFaultContract>)new WebFaultContract(error);
                fault = Message.CreateMessage(version, web_fault.CreateMessageFault(), "http://team.fosol.ca");

                Helpers.WebOperationContextHelper.SetOutgoingResponseStatusCode(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
