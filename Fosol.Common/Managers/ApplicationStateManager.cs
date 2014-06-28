﻿using Fosol.Common.Extensions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_APP || WINDOWS_PHONE_APP
using Windows.UI.Xaml;
using Windows.ApplicationModel.Activation;
using System.IO;
#endif

namespace Fosol.Common.Managers
{
    /// <summary>
    /// ApplicationState class provides a singleton object to manage application state information.
    /// You can use the ApplicationState class to manage various global variables.
    /// ApplicationState also provides methods to serialize and save state values to the filesystem so that when it is initialize it will first check for the file and reestablish prior state.
    /// To use ApplicationState you must first initialize it with the ApplicationState.Initialize method.
    /// </summary>
    public class ApplicationStateManager
        : Windows.UI.Xaml.Application, IDisposable
    {
        #region Variables
        private static System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private static string _AppName;
        private static ApplicationStateManager _GlobalApplicationState;
        private IO.SavedState _SavedState;
        private const string DefaultAppName = "Fosol.Common.Managers.ApplicationStateManager";
        private const string SavedStateFileNameFormat = "{0}.SavedState.xml";
        #endregion

        #region Properties
        /// <summary>
        /// get - The application name.
        /// </summary>
        public static string AppName
        {
            get { return _AppName; }
        }

        /// <summary>
        /// get - Returns the current ApplicationState object.
        /// Remember to call the ApplicationState.Initialize() method to set the current value.
        /// </summary>
        /// <returns>ApplicationState object or null if it has not been initialized.</returns>
        public new static ApplicationStateManager Current
        {
            get
            {
                _SlimLock.EnterReadLock();
                try
                {
                    if (ApplicationStateManager._GlobalApplicationState != null)
                        return ApplicationStateManager._GlobalApplicationState;

                    return null;
                }
                finally
                {
                    _SlimLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// get - SavedState object.
        /// </summary>
        public IO.SavedState SavedState
        {
            get { return _SavedState; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an ApplicationState class.
        /// </summary>
        public ApplicationStateManager()
#if WINDOWS_APP || WINDOWS_PHONE_APP
            : this(ApplicationStateManager.DefaultAppName)
#else
            : this(System.Reflection.Assembly.GetCallingAssembly().GetName().Name)
#endif
        {
        }

        /// <summary>
        /// Creates a new instance of an ApplicationState class.
        /// </summary>
        /// <param name="appName">The name of the application.</param>
        public ApplicationStateManager(string appName)
        {
            Fosol.Common.Validation.Assert.IsValidOperation(String.IsNullOrEmpty(ApplicationStateManager.AppName), "The ApplicationState object has already been initialized, you can only have one instance.");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(appName, "appName");
            _AppName = appName;
            _SavedState = new IO.SavedState(String.Format(ApplicationStateManager.SavedStateFileNameFormat, appName), false);
            _GlobalApplicationState = this;
            base.Suspending += this.OnSuspending;
            base.Resuming += this.OnResuming;
            base.UnhandledException += this.OnUnhandledException;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Dispose this ApplicationState object.
        /// </summary>
        public virtual void Dispose()
        {
            _AppName = null;
            _GlobalApplicationState = null;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        public delegate void LaunchedEventHandler(LaunchActivatedEventArgs e);
        public event LaunchedEventHandler Launched;

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // If the application is being launched for the first time, or a fresh launch do not restore state from the saved file.
            if (e.Kind == ActivationKind.Launch
                && e.PreviousExecutionState != ApplicationExecutionState.ClosedByUser
                && e.PreviousExecutionState != ApplicationExecutionState.NotRunning
                && e.PreviousExecutionState != ApplicationExecutionState.Terminated)
            {
                try
                {
                    var task = Task.Run(async () => { await _SavedState.RestoreAsync(); });
                    task.Wait();
                }
                catch (FileNotFoundException)
                {
                    // Ignore error if the file was not found.  Currently there is no way to see if a file exists.
                }
                catch
                {
                    // Something bad happened.
                }
            }

            if (this.Launched != null)
                this.Launched(e);

            base.OnLaunched(e);
        }

        public new EventHandler<object> Resuming;

        /// <summary>
        /// Restore the application state by loading the saved file.
        /// </summary>
        /// <param name="sender">Object which sent this event.</param>
        /// <param name="e">Object contain event arguments.</param>
        protected virtual void OnResuming(object sender, object e)
        {
            if (this.Resuming != null)
                this.Resuming(sender, e);
        }

        public new SuspendingEventHandler Suspending;

        /// <summary>
        /// Save the current application state to a file.
        /// </summary>
        /// <param name="sender">Object which sent this event.</param>
        /// <param name="e">SuspendingEventArgs object.</param>
#if WINDOWS_APP || WINDOWS_PHONE_APP
        protected virtual async void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            if (this.Suspending != null)
                this.Suspending(sender, e);
#else
        protected virtual async void OnSuspending(object sender, object e)
        {
#endif

            await _SavedState.SaveAsync();
            
#if WINDOWS_APP || WINDOWS_PHONE_APP
            // TODO: Save application state and stop any background activity
            deferral.Complete();
#endif
        }

        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }
        #endregion
    }
}
