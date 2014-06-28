using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Fosol.Common.Managers
{
    /// <summary>
    /// ControlStateManager class provides a way to save state information to a file for a control.
    /// This allows each control to maintain its own state even after an application has been terminated, or the control has be removed from memory.
    /// </summary>
    public class ControlStateManager
    {
        #region Variables
        private readonly System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private string _Key;
        private ControlState _State;
        private Collections.StateDictionary _Items;
        #endregion

        #region Properties
        /// <summary>
        /// get - Current state of the information saved.
        /// </summary>
        public ControlState State
        {
            get { return _State; }
            protected set { _State = value; }
        }

        /// <summary>
        /// get - A unique key name to identify this control in memory.
        /// </summary>
        public string Key
        {
            get { return _Key; }
        }

        /// <summary>
        /// get/set - The value for the specified key.
        /// if the key does not exist it will return null.
        /// if the key does not exist it will add it.
        /// Changes the Status to Altered if the value is new or different than the original.
        /// </summary>
        /// <param name="key">Key name to identify the value.</param>
        /// <returns>Value for the specified key.</returns>
        public object this[string key]
        {
            get 
            { 
                if (_Items.ContainsKey(key))
                    return _Items[key];
                return null;
            }
            set 
            {
                if (_Items.ContainsKey(key))
                {
                    if (!_Items[key].Equals(value))
                    {
                        _SlimLock.EnterWriteLock();
                        try
                        {
                            _Items[key] = value;
                            this.State = ControlState.Altered;
                        }
                        finally
                        {
                            _SlimLock.ExitWriteLock();
                        }
                    }
                }
                else
                {
                    _SlimLock.EnterWriteLock();
                    try
                    {
                        _Items.Add(key, value);
                        this.State = ControlState.Altered;
                    }
                    finally
                    {
                        _SlimLock.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// get - State dictionary containing all values that will be saved.
        /// </summary>
        protected Collections.StateDictionary Items
        {
            get { return _Items; }
        }

        /// <summary>
        /// SavedStateProperty is a dependency property that will hold the state dictionary.
        /// </summary>
        private static DependencyProperty SavedStateProperty
            = DependencyProperty.RegisterAttached("_ControlState", typeof(Collections.StateDictionary), typeof(ControlStateManager), null);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ControlStateManager class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'key' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'key' and 'control' cannot be null.</exception>
        /// <exception cref="System.InvalidOperationException">ApplicationState must be initialized first and the key must be unique.</exception>
        /// <param name="key">Unique key name to identify the control.</param>
        /// <param name="control">Control object that will gain state management.</param>
        public ControlStateManager(string key, Control control)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(key, "key");
            Fosol.Common.Validation.Assert.IsNotNull(control, "control");

            var app_state_manager = Managers.ApplicationStateManager.Current;
            Fosol.Common.Validation.Assert.IsValidOperation(app_state_manager != null, "Before creating an instance of a ControlStateManager you must intialize the ApplicationState.");
            //Fosol.Common.Validation.Assert.IsValidOperation(!app_state_manager.ContainsKey(key), "The key must be unique.  This key is already in use.");
            
            _Key = key;

            if (app_state_manager.SavedState.ContainsKey(key))
            {
                // State has been saved so it now needs to be loaded.
                _Items = app_state_manager.SavedState[key] as Collections.StateDictionary;
            }
            else
            {
                // Register this control state dictionary with the application state manager.
                _Items = new Collections.StateDictionary();
                app_state_manager.SavedState[key] = _Items;
            }
            this.State = ControlState.Unaltered;
            this.RegisterPropertyWithControl(control);

            // Listen to suspend and resume events so that the control can be informed.
            app_state_manager.Launched += this.OnLaunched;
            app_state_manager.Resuming += this.OnResuming;
            app_state_manager.Suspending += this.OnSuspending;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Register the dependecy property with the control.
        /// This will add a dependency property on the control and reference the this.State property.
        /// </summary>
        /// <param name="control">Control object this ControlStateManager will manage state for.</param>
        private void RegisterPropertyWithControl(Control control)
        {
            var control_state = control.GetValue(ControlStateManager.SavedStateProperty) as Collections.StateDictionary;

            Fosol.Common.Validation.Assert.IsValidOperation(control_state == null, "Control can only register the state dependency property once.");

            control.SetValue(ControlStateManager.SavedStateProperty, this.Items);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        public delegate void LaunchedEventHandler(LaunchActivatedEventArgs e);
        public event LaunchedEventHandler Launched;

        private void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (this.Launched != null)
                this.Launched(e);
        }

        public EventHandler<object> Resuming;

        private void OnResuming(object sender, object e)
        {
            if (this.Resuming != null)
                this.Resuming(sender, e);
        }

        public event SuspendingEventHandler Suspending;

        private void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            if (this.Suspending != null)
                this.Suspending(sender, e);
        }
        #endregion
    }
}
