using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Fosol.Common.Managers
{
    public sealed class ControlStateManager
    {
        #region Variables
        private Control _Control;
        private Collections.StateDictionary _State;
        #endregion

        #region Properties
        public Collections.StateDictionary State
        {
            get { return _State; }
        }

        private static DependencyProperty ControlSavedStateProperty
            = DependencyProperty.RegisterAttached("_ControlSavedState", typeof(Collections.StateDictionary), typeof(ControlStateManager), null);
        #endregion

        #region Constructors
        public ControlStateManager(Control frame)
        {
            Fosol.Common.Validation.Assert.IsNotNull(frame, "frame");
            Fosol.Common.Validation.Assert.IsValidOperation((Helpers.ApplicationState.Current() == null), "Before creating an instance of a ControlStateManager you must intialize the ApplicationState.");


            _State = new Collections.StateDictionary();
        }
        #endregion

        #region Methods
        public static void RegisterControl(Control control)
        {
            var saved_state = control.GetValue(ControlStateManager.ControlSavedStateProperty) as Collections.StateDictionary;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
