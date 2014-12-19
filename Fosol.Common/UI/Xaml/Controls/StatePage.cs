using Fosol.Common.Collections;
using Fosol.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Fosol.Common.UI.Xaml.Controls
{
    /// <summary>
    /// StatePage is an abstract class which provides built in state management for pages.
    /// </summary>
    public abstract class StatePage
        : Page
    {
        #region Variables
        private readonly System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        
        private static readonly DependencyProperty StateKeyProperty
            = DependencyProperty.Register("StateKey", typeof(string), typeof(StatePage), new PropertyMetadata(string.Empty, StateKeyPropertyChanged));
        private static readonly DependencyProperty StateProperty
            = DependencyProperty.Register("State", typeof(Collections.StateDictionary), typeof(StatePage), new PropertyMetadata(new StateDictionary(), StatePropertyChanged));
        #endregion

        #region Properties
        /// <summary>
        /// get - A unique key name to identify this pages state dictionary.
        /// </summary>
        public string StateKey
        {
            get 
            {
                var key = this.GetValue(StatePage.StateKeyProperty) as string;

                if (String.IsNullOrEmpty(key))
                {
                    // Create an unique key for this page when it is navigated to so that it can save it's state.
                    var name = Fosol.Common.Initialization.Assert.IsNotNullOrEmptyOrWhitespace(this.Name, "Fosol.StatePage");
                    key = String.Format("{0}_{1}", name, this.Frame.BackStackDepth);
                    this.StateKey = key;
                }

                return key;
            }
            protected set
            {
                Fosol.Common.Validation.Property.Assert.IsNotNullOrWhiteSpace(value, "StateKey");
                this.SetValue(StatePage.StateKeyProperty, value);
            }
        }

        /// <summary>
        /// get - StateDictionary used to store state information values.
        /// </summary>
        public Collections.StateDictionary State
        {
            get { return this.GetValue(StatePage.StateProperty) as StateDictionary; }
            private set { this.SetValue(StatePage.StateProperty, value); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a StatePage class.
        /// </summary>
        public StatePage()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                var app = StateApplication.Current;
                Fosol.Common.Validation.Value.Assert.IsTrue(app != null, "Before creating an instance of a StatePage you must intialize the StateApplication.");
            }

            // For some reason the State DependencyProperty is being shared with all child classes so this needs to initialize the property so that it isn't shared.
            /// It's ugly and I hate it but sometimes you just have to do what works.
            this.State = new StateDictionary();

            this.Loaded += StatePage_Loaded;
            this.Unloaded += StatePage_Unloaded;
        }
        #endregion

        #region Methods
        /// <summary>
        /// InitializeState provides a way to initialize the specified state keys so that databinding will work.
        /// If you use this method the initial value of the state value will be null.
        /// It's ugly and I hate it but sometimes you just have to do what works.
        /// </summary>
        /// <param name="keys">Key names.</param>
        protected void InitializeState(params string[] keys)
        {
            foreach (var key in keys)
            {
                if (!this.State.ContainsKey(key))
                    this.State[key] = null;
            }
        }

        /// <summary>
        /// InitializeState provides a way to initialize the specified state keys with the specified values so that databinding will work.
        /// It's ugly and I hate it but sometimes you just have to do what works.
        /// </summary>
        /// <param name="items">KeyValuePair objects.</param>
        protected void InitializeState(params KeyValuePair<string, object>[] items)
        {
            foreach (var item in items)
            {
                if (!this.State.ContainsKey(item.Key))
                    this.State[item.Key] = item.Value;
            }
        }

        /// <summary>
        /// Apply page state to the application state so that it can be serialized and saved to a state file.
        /// Calls the OnCachingState event method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CacheState(object sender, Events.CachingStateEventArgs args)
        {
            var app = StateApplication.Current;
            if (app.State.ContainsKey(this.StateKey))
                app.State[this.StateKey] = this.State;
            else
                app.State.Add(this.StateKey, this.State);

            this.OnCachingState(sender, args);
        }

        /// <summary>
        /// Calls the CacheState method which will call the OnCachingState event method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SavingState(object sender, Events.SavingStateEventArgs args)
        {
            this.CacheState(sender, new Events.CachingStateEventArgs());
        }

        /// <summary>
        /// If the StateApplication.Current contains state for this page it will apply it and call the OnRestoringState event method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestoreState(object sender, Events.RestoringStateEventArgs e)
        {
            var app = StateApplication.Current;
            if (app.State.ContainsKey(this.StateKey))
            {
                // Copy each value into the local state.
                this.State = app.State[this.StateKey] as StateDictionary;
                e.HasState = true;
                this.OnRestoringState(sender, e);
            }
            else if (this.State == null)
                this.State = new StateDictionary();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        /// <summary>
        /// Listen to the StateApplication SavingState and RestoringState events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StatePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var app = StateApplication.Current;
            app.SavingState += this.SavingState;
            app.RestoringState += this.RestoreState;

            //var state = this.GetValue(StatePage.SavedStateProperty) as Collections.StateDictionary;
            //Fosol.Common.Validation.Assert.IsValidOperation(state == null, "Control can only register the state dependency property once.");
            //this.SetValue(StatePage.SavedStateProperty, this.State);

            this.RestoreState(sender, new Events.RestoringStateEventArgs(Windows.ApplicationModel.Activation.ApplicationExecutionState.Running));
        }

        /// <summary>
        /// Stop listening to the StateApplication SavingState and RestoringState events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StatePage_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var app = StateApplication.Current;
            app.SavingState -= this.SavingState;
            app.RestoringState -= this.RestoreState;
        }

        /// <summary>
        /// OnNavigatingFrom informs the StateApplication to save state information.
        /// If you override this event in your page you will need to either call the SaveState method or call this method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
                this.CacheState(this, new Events.CachingStateEventArgs());
        }

        /// <summary>
        /// This is where you place values into the StateDictionary so that they can be serialized and saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCachingState(object sender, Events.CachingStateEventArgs e)
        {
        }

        /// <summary>
        /// The StateDictionary has been refreshed and placed into memory.
        /// This is where you will update your page based on the refreshed state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnRestoringState(object sender, Events.RestoringStateEventArgs e)
        {
        }

        private static void StateKeyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void StatePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion
    }
}
