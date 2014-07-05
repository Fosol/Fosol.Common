using Fosol.Common.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        private static DependencyProperty SavedStateProperty
            = DependencyProperty.RegisterAttached("_PageState", typeof(Collections.StateDictionary), typeof(StatePage), null);
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
                if (!_StateKey.HasValue)
                {
                    // Create an unique key for this page when it is navigated to so that it can save it's state.
                    var name = Fosol.Common.Initialization.Assert.IsNotDefaultOrEmptyOrWhitespace(this.Name, "Fosol.StatePage");
                    _StateKey.Value = _StateKey.HasValue ? this.StateKey : String.Format("{0}_{1}", name, this.Frame.BackStackDepth);
                }

                return _StateKey.Value;
            }
        }

        /// <summary>
        /// get - StateDictionary used to store state information values.
        /// </summary>
        public Collections.StateDictionary State
        {
            get { return this.GetValue(StatePage.SavedStateProperty) as StateDictionary; }
            private set { this.SetValue(StatePage.SavedStateProperty, value); }
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
            this.State = new Collections.StateDictionary();

            this.Loaded += StatePage_Loaded;
            this.Unloaded += StatePage_Unloaded;
        }
        #endregion

        #region Methods

        private void InitializePageState()
        {
            var app = StateApplication.Current;
            if (app.State.ContainsKey(this.StateKey))
                this.State = app.State[this.StateKey] as StateDictionary;
            else
                app.State.Add(this.StateKey, this.State);

            //var state = this.GetValue(StatePage.SavedStateProperty) as Collections.StateDictionary;
            //Fosol.Common.Validation.Assert.IsValidOperation(state == null, "Control can only register the state dependency property once.");
            //this.SetValue(StatePage.SavedStateProperty, this.State);
        }

        /// <summary>
        /// Save the state information to the file system.
        /// </summary>
        public void SaveState()
        {
            var app = StateApplication.Current;
            app.SaveState();
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
            app.SavingState += this.OnSavingState;
            app.RestoringState += this.OnRestoringState;

            InitializePageState();
            this.OnRestoringState(this, new Events.RestoringStateEventArgs(Windows.ApplicationModel.Activation.ApplicationExecutionState.Running));
        }

        /// <summary>
        /// Stop listening to the StateApplication SavingState and RestoringState events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StatePage_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var app = StateApplication.Current;
            app.SavingState -= this.OnSavingState;
            app.RestoringState -= this.OnRestoringState;
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// OnNavigatingFrom informs the StateApplication to save state information.
        /// If you override this event in your page you will need to either call the SaveState method or call this method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState();
            base.OnNavigatingFrom(e);
        }

        /// <summary>
        /// This is where you place values into the StateDictionary so that they can be serialized and saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSavingState(object sender, Events.SavingStateEventArgs e)
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
