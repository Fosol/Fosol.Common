using Fosol.Common.Extensions.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// Provides a way to watch a specific configuration section file for changes.
    /// If a change occurs with the section it will fire the appropriate event.
    /// Remember to call the Start method to begin watching the configuration file.
    /// </summary>
    /// <typeparam name="T">Type of ConfigurationSection object being watched.</typeparam>
    public abstract class ConfigurationSectionFileWatcherBase<T> : IDisposable
        where T : ConfigurationSection, new()
    {
        #region Variables
        protected readonly object _BigLock = new object();
        private readonly System.Threading.ReaderWriterLockSlim _Lock = new System.Threading.ReaderWriterLockSlim();
        private FileSystemWatcher _Watcher;
        private bool _IsConfigLoaded = false;
        private bool _IsWatching = false;
        private T _ConfigurationSection;
        private string _FilePath;
        private bool _ThrowOnError = true;

        /// <summary>
        /// Fires when the configuration fails to load.
        /// </summary>
        public event EventHandler<Events.ConfigurationSectionErrorEventArgs> Error;

        /// <summary>
        /// Fires when the configuration file has changed.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FileChanged;

        /// <summary>
        /// Fires when the configuration file is created.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FileCreated;

        /// <summary>
        /// Fires when the configuration file is deleted.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FileDeleted;

        /// <summary>
        /// Fires when the configuration file is renamed.
        /// </summary>
        public event EventHandler<RenamedEventArgs> FileRenamed;
        #endregion

        #region Properties
        /// <summary>
        /// get - Whether the configuration file has been loaded.
        /// </summary>
        protected bool IsConfigLoaded
        {
            get
            {
                _Lock.EnterReadLock();
                try
                {
                    return _IsConfigLoaded;
                }
                finally
                {
                    _Lock.ExitReadLock();
                }
            }
            set
            {
                _Lock.EnterWriteLock();
                try
                {
                    _IsConfigLoaded = value;
                }
                finally
                {
                    _Lock.ExitWriteLock();
                }
            }
        }
        /// <summary>
        /// get - Application configuration file.
        /// </summary>
        public System.Configuration.Configuration Configuration
        {
            get { return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); }
        }

        /// <summary>
        /// get - Access the ConfigurationSection object.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        public T Section
        {
            get 
            {
                _Lock.EnterReadLock();
                try
                {
                    if (_IsConfigLoaded)
                        return _ConfigurationSection;
                }
                finally
                {
                    _Lock.ExitReadLock();
                }

                return null;
            }
            protected set
            {
                _Lock.EnterWriteLock();
                try
                {
                    _ConfigurationSection = value;

                    if (value != null)
                        _IsConfigLoaded = true;
                    else
                        _IsConfigLoaded = false;
                }
                finally
                {
                    _Lock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// get - Path to configuration section file.
        /// </summary>
        public string FilePath 
        {
            get
            {
                _Lock.EnterReadLock();
                try
                {
                    return _FilePath;
                }
                finally
                {
                    _Lock.ExitReadLock();
                }
            }
            protected set
            {
                _Lock.EnterWriteLock();
                try
                {
                    _FilePath = value;
                }
                finally
                {
                    _Lock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Whether exceptions that occur during the LoadConfig or RefreshSection are thrown as exceptions or whether they fire the ConfigurationError event instead.
        /// By default it will throw the exception as per usual coding standards.
        /// </summary>
        public bool ThrowOnError
        {
            get
            {
                _Lock.EnterReadLock();
                try
                {
                    return _ThrowOnError;
                }
                finally
                {
                    _Lock.ExitReadLock();
                }
            }
            set
            {
                _Lock.EnterWriteLock();
                try
                {
                    _ThrowOnError = value;
                }
                finally
                {
                    _Lock.ExitWriteLock();
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Provides the base abstract implementation for watching Configuration Section files.
        /// </summary>
        public ConfigurationSectionFileWatcherBase()
        {
        }

        /// <summary>
        /// Provides the base abstract implementation for watching Configuration Section files.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "pathToFile" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "pathToFile" cannot be null.</exception>
        /// <param name="pathToFile">Full path to the section configuration file.</param>
        public ConfigurationSectionFileWatcherBase(string pathToFile)
        {
            Validation.Assert.IsNotNullOrEmpty(pathToFile, "pathToFile");

            this.FilePath = pathToFile;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises ConfigurationError event.
        /// Provides a way for inherited classes to receive the ConfigurationError event.
        /// </summary>
        /// <param name="ex">Exception that was thrown.</param>
        protected virtual void OnError(Exception ex)
        {
            if (this.ThrowOnError)
                throw ex;
            else
                this.Error.Raise(this, new Events.ConfigurationSectionErrorEventArgs(ex));
        }

        /// <summary>
        /// Deserialize the section configuration into the ConfigurationSection of object of type T.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "path" cannot be null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Section configuration file must exist.</exception>
        /// <param name="pathToFile">Path to section configuration file.</param>
        /// <returns>ConfigurationSection object of type T.</returns>
        protected static T DeserializeSection(string pathToFile)
        {
            Validation.Assert.IsNotNull(pathToFile, "pathToFile");

            if (!File.Exists(pathToFile))
                throw new System.IO.FileNotFoundException(String.Format(Resources.Strings.Exception_File_Not_Found, Path.GetFileName(pathToFile)), pathToFile);

            using (var stream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    var section = new T();
                    section.GetType().GetMethod("DeserializeSection", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(section, new object[] { reader });
                    return section;
                }
            }
        }

        /// <summary>
        /// If the configuration file hasn't been loaded it will attempt to load it before starting the FileSystemWatcher.
        /// Start the FileSystemWatcher object.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        public void Start()
        {
            if (!this.IsConfigLoaded)
                LoadConfig();

            lock (_BigLock)
            {
                // Only start the watcher if it hasn't already started.
                if (!_IsWatching && _IsConfigLoaded)
                {
                    if (_Watcher == null)
                        _Watcher = new FileSystemWatcher(Path.GetDirectoryName(this.FilePath), Path.GetFileName(this.FilePath));
                    _Watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                    _Watcher.Created += OnFileCreated;
                    _Watcher.Changed += OnFileChanged;
                    _Watcher.Deleted += OnFileDeleted;
                    _Watcher.Renamed += OnFileRenamed;
                    _Watcher.EnableRaisingEvents = true;

                    _IsWatching = true;
                }
            }
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileChanged event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                this._Watcher.EnableRaisingEvents = false;
                RefreshSection();
            }
            catch (Exception ex)
            {
                this.Error.Raise(sender, new Events.ConfigurationSectionErrorEventArgs(ex, e));
            }
            finally
            {
                this.FileChanged.Raise(sender, e);
                this._Watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileCreated event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                this._Watcher.EnableRaisingEvents = false;
                RefreshSection();
            }
            catch (Exception ex)
            {
                this.Error.Raise(sender, new Events.ConfigurationSectionErrorEventArgs(ex, e));
            }
            finally
            {
                this.FileCreated.Raise(sender, e);
                this._Watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileDeleted event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                this._Watcher.EnableRaisingEvents = false;
                RefreshSection();
            }
            catch (Exception ex)
            {
                this.Error.Raise(sender, new Events.ConfigurationSectionErrorEventArgs(ex, e));
            }
            finally
            {
                this.FileDeleted.Raise(sender, e);
                this._Watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileRenamed event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                this._Watcher.EnableRaisingEvents = false;
                RefreshSection();
            }
            catch (Exception ex)
            {
                this.Error.Raise(sender, new Events.ConfigurationSectionErrorEventArgs(ex, e));
            }
            finally
            {
                this.FileRenamed.Raise(sender, e);
                this._Watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// </summary>
        protected abstract void RefreshSection();

        /// <summary>
        /// Load the configuration section.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        protected abstract void LoadConfig();

        /// <summary>
        /// Unhook listeners from the FileSystemWatcher.
        /// </summary>
        public void Stop()
        {
            lock (_BigLock)
            {
                if (_IsWatching && _Watcher != null)
                {
                    _Watcher.Created -= OnFileCreated;
                    _Watcher.Changed -= OnFileChanged;
                    _Watcher.Deleted -= OnFileDeleted;
                    _Watcher.Renamed -= OnFileRenamed;
                    _Watcher.EnableRaisingEvents = false;
                    _IsWatching = false;
                }
            }
        }

        /// <summary>
        /// Dispose of the FileSystemWatcher listener events.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
