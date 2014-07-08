using Fosol.Common.Collections;
using Fosol.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Fosol.Common.UI.Xaml.Controls
{
    public class StatePage
        : Page
    {
        #region Variables
        private readonly System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private Helpers.SetOnce<string> _StateKey;
        private StateStatus _Status;

        /// <summary>
        /// SavedStateProperty is a dependency property that will hold the state dictionary.
        /// </summary>
        private DependencyProperty SavedStateProperty
            = DependencyProperty.RegisterAttached("_PageState", typeof(Collections.StateDictionary), typeof(StatePage), new PropertyMetadata(new StateDictionary()));

        private DependencyProperty StateKeyProperty
            = DependencyProperty.RegisterAttached("_StateKey", typeof(SetOnce<string>), typeof(StatePage), new PropertyMetadata(new SetOnce<string>()));
        #endregion

        #region Properties
        /// <summary>
        /// get - Current status of the information saved.
        /// </summary>
        public StateStatus Status
        {
            get { return _Status; }
            protected set { _Status = value; }
        }

        /// <summary>
        /// get - A unique key name to identify this pages state dictionary.
        /// </summary>
        public string StateKey
        {
            get 
            {
                var key = this.GetValue(this.StateKeyProperty) as SetOnce<string>;

                if (!key.HasValue)
                {
                    // Create an unique key for this page when it is navigated to so that it can save it's state.
                    var name = Fosol.Common.Initialization.Assert.IsNotDefaultOrEmptyOrWhitespace(this.Name, "Fosol.StatePage");
                    key.Value = key.HasValue ? key.Value : String.Format("{0}_{1}", name, this.Frame.BackStackDepth);
                    this.SetValue(this.StateKeyProperty, key);
                }

                return key.Value;
            }
        }

        /// <summary>
        /// get - StateDictionary used to store state information values.
        /// </summary>
        public Collections.StateDictionary State
        {
            get { return this.GetValue(this.SavedStateProperty) as StateDictionary; }
            private set { this.SetValue(this.SavedStateProperty, value); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a StatePage class.
        /// </summary>
        public StatePage()
        {
            var app = StateApplication.Current;
            Fosol.Common.Validation.Assert.IsValidOperation(app != null, "Before creating an instance of a StatePage you must intialize the StateApplication.");

            _StateKey = new Helpers.SetOnce<string>();

            this.Loaded += StatePage_Loaded;
            this.Unloaded += StatePage_Unloaded;
        }
        #endregion

        #region Methods
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
        private void RestoringState(object sender, Events.RestoringStateEventArgs e)
        {
            var app = StateApplication.Current;
            if (app.State.ContainsKey(this.StateKey))
            {
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
            app.RestoringState += this.RestoringState;

            //var state = this.GetValue(StatePage.SavedStateProperty) as Collections.StateDictionary;
            //Fosol.Common.Validation.Assert.IsValidOperation(state == null, "Control can only register the state dependency property once.");
            //this.SetValue(StatePage.SavedStateProperty, this.State);

            this.RestoringState(sender, new Events.RestoringStateEventArgs(Windows.ApplicationModel.Activation.ApplicationExecutionState.Running));
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
            app.RestoringState -= this.RestoringState;
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
        #endregion
    }
}
