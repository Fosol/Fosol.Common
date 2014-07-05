using Fosol.Common.Extensions.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_APP || WINDOWS_PHONE_APP
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
#endif

namespace Fosol.Common.UI.Xaml
{
    /// <summary>
    /// StateApplication class provides a singleton object to manage application state information.
    /// You can use the StateApplication class to manage various global variables.
    /// StateApplication also provides methods to serialize and save state values to the filesystem so that when it is initialize it will first check for the file and reestablish prior state.
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
        private IO.SavedState _State;
        private int _CacheSize = 1;
        private Type _DefaultPageType;

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
        public IO.SavedState State
        {
            get { return _State; }
        }

        public int CacheSize
        {
            get { return _CacheSize; }
            set { _CacheSize = value; }
        }

        /// <summary>
        /// get/set - The type of the default page you want to navigate to within the OnLaunched method.
        /// </summary>
        public Type DefaultPageType
        {
            get { return _DefaultPageType; }
            set 
            {
                Fosol.Common.Validation.Assert.IsNotNull(value, "DefaultPageType");
                _DefaultPageType = value; 
            }
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
            Fosol.Common.Validation.Assert.IsValidOperation(String.IsNullOrEmpty(StateApplication.AppName), "The StateApplication object has already been initialized, you can only have one instance.");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(appName, "appName");
            _AppName = appName;
            _State = new IO.SavedState(String.Format(StateApplication.SavedStateFileNameFormat, appName), false);
            _Current = this;
            base.Suspending += this.OnSuspending;
            base.Resuming += this.OnResuming;
            base.UnhandledException += this.OnUnhandledException;
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
        /// Forces state information to be serialized and saved to the file system.
        /// Fires the SavingState event.
        /// </summary>
        public void SaveState()
        {
            this.OnSavingState(this, new Events.SavingStateEventArgs());
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
            this.OnRetrievingState(this, new Events.RetrievingStateEventArgs(e.PreviousExecutionState));

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

            this.OnRestoringState(this, new Events.RestoringStateEventArgs(e.PreviousExecutionState));
        }

        /// <summary>
        /// The RetrievingState event is fired from the OnRetrievingState method.
        /// </summary>
        public EventHandler<Events.RetrievingStateEventArgs> RetrievingState;

        /// <summary>
        /// OnRetrievingState loads the saved state from a file into memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRetrievingState(object sender, Events.RetrievingStateEventArgs e)
        {
            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                try
                {
                    var task = Task.Run(async () => { await _State.RestoreAsync(); });
                    task.Wait();
                }
                catch (FileNotFoundException)
                {
                    // Ignore error if the file was not found.  Currently there is no way to see if a file exists.
                }
                catch
                {
                    // Something bad happened and the file most likely is in an incorrect saved state.
                }
            }

            if (this.RetrievingState != null)
                this.RetrievingState(sender, e);
        }

        /// <summary>
        /// The RestoringState event is fired from the OnRestoringState method.
        /// </summary>
        public EventHandler<Events.RestoringStateEventArgs> RestoringState;

        /// <summary>
        /// OnRestoringState provides an event that occurs after state has been retrieving and placed into memory.
        /// Fires the RestoringState event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRestoringState(object sender, Events.RestoringStateEventArgs e)
        {
            if (this.RestoringState != null)
                this.RestoringState(sender, e);
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
        private async void OnSavingState(object sender, Events.SavingStateEventArgs e)
        {
            if (this.SavingState != null)
                this.SavingState(sender, e);

            await _State.SaveAsync();
        }

        /// <summary>
        /// The Resuming event is fired from the OnResuming method.
        /// </summary>
        public new EventHandler<object> Resuming;

        /// <summary>
        /// OnResuming is fired when the application resumes.
        /// Fires Resuming evetn.
        /// Calls the OnRestringState method.
        /// </summary>
        /// <param name="sender">Object which sent this event.</param>
        /// <param name="e">Object contain event arguments.</param>
        protected virtual void OnResuming(object sender, object e)
        {
            this.OnRestoringState(sender, new Events.RestoringStateEventArgs(ApplicationExecutionState.Suspended, e));

            if (this.Resuming != null)
                this.Resuming(sender, e);
        }

        /// <summary>
        /// The Suspending event is fired from the OnSuspending method.
        /// </summary>
        public new SuspendingEventHandler Suspending;

        /// <summary>
        /// Save the current application state to a file.
        /// </summary>
        /// <param name="sender">Object which sent this event.</param>
        /// <param name="e">SuspendingEventArgs object.</param>
        protected virtual void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            if (this.Suspending != null)
                this.Suspending(sender, e);

            this.OnSavingState(sender, new Events.SavingStateEventArgs());
            
            deferral.Complete();
        }

        /// <summary>
        /// The UnhandledException event is fired from the OnUnhandledException method.
        /// </summary>
        public new UnhandledExceptionEventHandler UnhandledException;

        /// <summary>
        /// OnUnhandledException is called whenever an exception is thrown but not handled by code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (this.UnhandledException != null)
                this.UnhandledException(sender, e);
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
