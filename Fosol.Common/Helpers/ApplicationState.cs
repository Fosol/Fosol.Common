using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// ApplicationState class provides a singleton object to manage application state information.
    /// You can use the ApplicationState class to manage various global variables.
    /// ApplicationState also provides methods to serialize and save state values to the filesystem so that when it is initialize it will first check for the file and reestablish prior state.
    /// To use ApplicationState you must first initialize it with the ApplicationState.Initialize method.
    /// </summary>
    public sealed class ApplicationState
        : IDisposable
    {
        #region Variables
        private static System.Threading.ReaderWriterLockSlim _SlimLock = new System.Threading.ReaderWriterLockSlim();
        private static string _AppName;
        private static SavedState _Items;
        private static ApplicationState _GlobalApplicationState;
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
        /// get - SavedState object containing saved information.
        /// </summary>
        public SavedState Items
        {
            get { return _Items; }
        }

        /// <summary>
        /// get/set - The saved state object for the specified key.
        /// </summary>
        /// <param name="key">Key name to identify saved values.</param>
        /// <returns>SavedState object for the specified key.</returns>
        public object this[string key]
        {
            get { return _Items[key]; }
            set { _Items[key] = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an ApplicationState class.
        /// </summary>
        /// <param name="appName">The name of the application.</param>
        private ApplicationState(string appName)
        {
            Fosol.Common.Validation.Assert.IsValidOperation(String.IsNullOrEmpty(ApplicationState.AppName), "The ApplicationState object has already been initialized, you can only have one instance.");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(appName, "appName");
            _AppName = appName;
            _Items = new SavedState(String.Format("{0}.SavedState.xml", appName));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initializes a global instance of the ApplicationState class.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter 'appName' cannot be empty or whitespace.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter 'appName' cannot be null.</exception>
        /// <exception cref="System.InvalidOperationException">You cannot initialize the ApplicationState more than once.</exception>
        /// <param name="appName">Name of the application.</param>
        public static void Initialize(string appName)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(appName, "appName");
            Fosol.Common.Validation.Assert.IsValidOperation((ApplicationState._GlobalApplicationState == null), String.Format("ApplicationState has already been initialized for '{0}'", appName));

            _SlimLock.EnterUpgradeableReadLock();
            try
            {
                Fosol.Common.Validation.Assert.IsValidOperation((ApplicationState._GlobalApplicationState == null), String.Format("ApplicationState has already been initialized for '{0}'", appName));

                _SlimLock.EnterWriteLock();
                try
                {
                    ApplicationState._GlobalApplicationState = new ApplicationState(appName);
                }
                finally
                {
                    _SlimLock.ExitWriteLock();
                }
            }
            finally
            {
                _SlimLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Returns the current ApplicationState object.
        /// Remember to call the ApplicationState.Initialize() method to set the current value.
        /// </summary>
        /// <returns>ApplicationState object or null if it has not been initialized.</returns>
        public static ApplicationState Current()
        {
            _SlimLock.EnterReadLock();
            try
            {
                if (ApplicationState._GlobalApplicationState != null)
                    return ApplicationState._GlobalApplicationState;

                return null;
            }
            finally
            {
                _SlimLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Add a new key and value to the application state manager.
        /// </summary>
        /// <param name="key">Unique key to identify the value.</param>
        /// <param name="value">SavedState to be saved to application state.</param>
        public void Add(string key, SavedState value)
        {
            _Items.Add(key, value);
        }

        /// <summary>
        /// Remove the value specified by the key name.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>True if the key was removed.</returns>
        public bool Remove(string key)
        {
            return _Items.Remove(key);
        }

        /// <summary>
        /// Clear the application state of all items.
        /// </summary>
        public void Clear()
        {
            _Items.Clear();
        }

        /// <summary>
        /// Dispose this ApplicationState object.
        /// </summary>
        public void Dispose()
        {
            _Items = null;
            _AppName = null;
            _GlobalApplicationState = null;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
