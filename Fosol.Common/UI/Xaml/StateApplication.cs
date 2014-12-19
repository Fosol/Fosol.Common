using Fosol.Common.Extensions.Events;
using Fosol.Common.UI.Xaml.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Fosol.Common.UI.Xaml
{
    /// <summary>
    /// StateApplication class provides a singleton object to manage application state information.
    /// StateApplication provides a way to serialize and save state values to the filesystem.
    /// By default StateApplication will only save the specified number (CacheSize) of values into state cache.
    /// </summary>
    public class StateApplication
        : Windows.UI.Xaml.Application, IDisposable
    {
        #region Variables
        private static System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private const string DefaultAppName = "Fosol.StateApplication";
        private const string SavedStateFileNameFormat = "{0}.SavedState.xml";
        private static string _AppName;
        private static StateApplication _Current;
        private IO.ApplicationState _State;
        private Type _DefaultPageType;
        private StateRestoreOption _StateRestoreOption;

#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif
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
        /// get - Returns the current StateApplication object.
        /// Remember to call the StateApplication.Initialize() method to set the current value.
        /// </summary>
        /// <returns>StateApplication object or null if it has not been initialized.</returns>
        public new static StateApplication Current
        {
            get
            {
                _SlimLock.EnterReadLock();
                try
                {
                    if (StateApplication._Current != null)
                        return StateApplication._Current;

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
        public IO.ApplicationState State
        {
            get { return _State; }
        }

        /// <summary>
        /// get/set - The number of pages that can have state saved.
        /// </summary>
        public int CacheSize
        {
            get { return this.State.CacheSize; }
            set { this.State.CacheSize = value; }
        }

        /// <summary>
        /// get/set - The type of the default page you want to navigate to within the OnLaunched method.
        /// </summary>
        public Type DefaultPageType
        {
            get { return _DefaultPageType; }
            set 
            {
                Fosol.Common.Validation.Property.Assert.IsNotNull(value, "DefaultPageType");
                _DefaultPageType = value; 
            }
        }

        /// <summary>
        /// get/set - Under what conditions will state be restored.
        /// </summary>
        public StateRestoreOption StateRestoreOption
        {
            get { return _StateRestoreOption; }
            set { _StateRestoreOption = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an StateApplication class.
        /// </summary>
        public StateApplication()
#if WINDOWS_APP || WINDOWS_PHONE_APP
            : this(StateApplication.DefaultAppName)
#else
            : this(System.Reflection.Assembly.GetCallingAssembly().GetName().Name)
#endif
        {
        }

        /// <summary>
        /// Creates a new instance of an StateApplication class.
        /// </summary>
        /// <param name="appName">The name of the application.</param>
        public StateApplication(string appName)
        {
            Fosol.Common.Validation.Value.Assert.IsNotNullOrWhiteSpace(StateApplication.AppName, "The StateApplication object has already been initialized, you can only have one instance.");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(appName, "appName");
            _AppName = appName;
            _State = new IO.ApplicationState(String.Format(StateApplication.SavedStateFileNameFormat, appName), false);
            _Current = this;
            _StateRestoreOption = StateRestoreOption.Terminated;
            base.Suspending += this._OnSuspending;
            base.Resuming += this._OnResuming;
            base.UnhandledException += this._OnUnhandledException;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Dispose this StateApplication object.
        /// </summary>
        public virtual void Dispose()
        {
            _AppName = null;
            _Current = null;
        }

        /// <summary>
        /// Call the OnRetrievingState event method.
        /// Fire the RetrievingState event.
        /// If RetrievingStateEventArgs.Cancel property is false then attempt to load and deserialize the saved state file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetrieveState(object sender, Events.RetrievingStateEventArgs e)
        {
            this.OnRetrievingState(sender, e);

            if (this.RetrievingState != null)
                this.RetrievingState(sender, e);

            // Only restore state if the specified StateRestoreOption contains the pervious execution state.
            if (this.StateRestoreOption.Contains(e.PreviousExecutionState))
            {
                try
                {
                    var task = Task.Run(async () => { await _State.RestoreAsync(); });
                    task.Wait();
                    e.HasState = true;
                }
                catch (FileNotFoundException ex)
                {
                    // Ignore error if the file was not found.  Currently there is no way to see if a file exists.
                    e.Exception = ex;
                }
                catch (Exception ex)
                {
                    // Something bad happened and the file most likely is in an incorrect saved state.
                    e.Exception = ex;
                }
            }
        }

        /// <summary>
        /// Call the OnRestoringState event method.
        /// Fire the RestoringState event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RestoreState(object sender, Events.RestoringStateEventArgs args)
        {
            this.OnRestoringState(sender, args);

            if (!args.Cancel 
                && this.RestoringState != null)
                this.RestoringState(sender, args);
        }

        /// <summary>
        /// Forces state information to be serialized and saved to the file system.
        /// Fires the SavingState event.
        /// If you don't want to save the state file set the SavingStateEventArgs.Cancel to 'true'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SaveState(object sender, Events.SavingStateEventArgs args)
        {
            this.OnSavingState(this, args);

            if (this.SavingState != null)
                this.SavingState(this, args);

            if (!args.Cancel)
            {
                var task = Task.Run(async () => { await _State.SaveAsync(); });
                task.Wait();
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events

        public delegate void LaunchedEventHandler(LaunchActivatedEventArgs e);
        /// <summary>
        /// The Launched event is fired from the OnLaunched method.
        /// </summary>
        public event LaunchedEventHandler Launched;

        /// <summary>
        /// OnLaunched is fired when the application launches.
        /// Fires the launched event.
        /// Calls the OnRetrievingState method.
        /// Calls the OnRestoringState method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            if (this.Launched != null)
                this.Launched(e);

            base.OnLaunched(e);

            // Deserialize the saved state and place it into memory.
            var retrieving_state_event_args = new Events.RetrievingStateEventArgs(e.PreviousExecutionState);
            this.RetrieveState(this, retrieving_state_event_args);

            var root_frame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (root_frame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                root_frame = new Frame();
                root_frame.CacheSize = this.CacheSize;

                // Place the frame in the current Window
                Window.Current.Content = root_frame;
            }

            if (root_frame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (root_frame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in root_frame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                root_frame.ContentTransitions = null;
                root_frame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (this.DefaultPageType != null && !root_frame.Navigate(this.DefaultPageType, e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

            this.RestoreState(this, new Events.RestoringStateEventArgs(retrieving_state_event_args));
        }

        /// <summary>
        /// The RetrievingState event is fired from the OnRetrievingState method.
        /// </summary>
        public EventHandler<Events.RetrievingStateEventArgs> RetrievingState;

        /// <summary>
        /// OnRetrievingState is called before an attempt to load and deserialize the saved state file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">RetrievingStateEventArgs object.</param>
        protected virtual void OnRetrievingState(object sender, Events.RetrievingStateEventArgs e)
        {
        }

        /// <summary>
        /// The RestoringState event is fired from the OnRestoringState method.
        /// </summary>
        public EventHandler<Events.RestoringStateEventArgs> RestoringState;

        /// <summary>
        /// OnRestoringState is called before any other restoring event is fired.
        /// If you want to stop the RestoringState event from being fired change the RestoringStateEventArgs.Cancel to 'true'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnRestoringState(object sender, Events.RestoringStateEventArgs e)
        {
        }

        /// <summary>
        /// The SavingState event is fired from the OnSavingState method.
        /// </summary>
        public EventHandler<Events.SavingStateEventArgs> SavingState;

        /// <summary>
        /// OnSavingState serializes state information and saves it as a file.
        /// Fires the SavingState event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSavingState(object sender, Events.SavingStateEventArgs e)
        {
        }

        /// <summary>
        /// The Resuming event is fired from the OnResuming method.
        /// The event hides the base event because we want to ensure state is restored before informing anything else that we are resuming.
        /// </summary>
        public new EventHandler<object> Resuming;

        /// <summary>
        /// OnResuming is fired when the application resumes.
        /// Fires Resuming evetn.
        /// Calls the OnRestringState method.
        /// </summary>
        /// <param name="sender">Object which sent this event.</param>
        /// <param name="e">Object contain event arguments.</param>
        private void _OnResuming(object sender, object e)
        {
            var restoring_state_event_args = new Events.RestoringStateEventArgs(ApplicationExecutionState.Suspended);
            this.RestoreState(sender, restoring_state_event_args);

            this.OnResuming(sender, e);

            if (this.Resuming != null)
                this.Resuming(sender, e);
        }

        /// <summary>
        /// OnResuming event method is called before the Resuming event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnResuming(object sender, object e)
        {

        }

        /// <summary>
        /// The Suspending event is fired from the OnSuspending method.
        /// This event hides the base class Suspending event because we need to ensure state is saved before suspending the application.
        /// </summary>
        public new SuspendingEventHandler Suspending;

        /// <summary>
        /// Save the current application state to a file.
        /// </summary>
        /// <param name="sender">Object which sent this event.</param>
        /// <param name="e">SuspendingEventArgs object.</param>
        private void _OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            this.OnSuspending(sender, e);

            if (this.Suspending != null)
                this.Suspending(sender, e);

            this.SaveState(sender, new Events.SavingStateEventArgs(e));
            
            deferral.Complete();
        }

        /// <summary>
        /// OnSuspending event method is called before the Suspending event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {

        }

        /// <summary>
        /// The UnhandledException event is fired from the OnUnhandledException method.
        /// This event hides the base event because we want to call the OnUnhandledException event method.
        /// </summary>
        public new UnhandledExceptionEventHandler UnhandledException;

        /// <summary>
        /// OnUnhandledException is called whenever an exception is thrown but not handled by code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.OnUnhandledException(sender, e);

            if (this.UnhandledException != null)
                this.UnhandledException(sender, e);
        }

        /// <summary>
        /// OnUnhandledException event method is called before the UnhandledException event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif
        #endregion
    }
}
