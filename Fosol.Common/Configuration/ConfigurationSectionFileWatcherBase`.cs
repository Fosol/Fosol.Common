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
        protected readonly object _Lock = new object();
        private FileSystemWatcher _Watcher;
        protected bool _IsConfigLoaded = false;
        protected bool _IsWatching = false;
        private T _ConfigurationSection;

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
        /// get - Access the ConfigurationSection object.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        public T ConfigSection
        {
            get 
            {
                lock (_Lock)
                {
                    if (!_IsConfigLoaded)
                        LoadConfig();

                    return _ConfigurationSection;
                }
            }
            protected set
            {
                lock (_Lock)
                {
                    _ConfigurationSection = value;
                    _IsConfigLoaded = true;
                }
            }
        }

        /// <summary>
        /// get - Path to configuration section file.
        /// </summary>
        public string PathToFile { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of a ConfigurationSectionFileWatcher object.
        /// Remember to call the Start method to begin watching the configuration file.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "sectionName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "sectionName" cannot be null.</exception>
        /// <param name="path">Full path to the section configuration file.</param>
        public ConfigurationSectionFileWatcherBase(string path)
        {
            Validation.Assert.IsNotNullOrEmpty(path, "path");

            this.PathToFile = path;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Deserialize the section configuration into the ConfigurationSection of object of type T.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "path" cannot be null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Section configuration file must exist.</exception>
        /// <param name="path">Path to section configuration file.</param>
        /// <returns>ConfigurationSection object of type T.</returns>
        protected static T DeserializeSection(string path)
        {
            Validation.Assert.IsNotNull(path, "path");

            if (!File.Exists(path))
                throw new System.IO.FileNotFoundException(String.Format(Resources.Strings.Exception_FileNotFound, Path.GetFileName(path)), path);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
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
            lock (_Lock)
            {
                // Only start the watcher if it hasn't already started.
                if (!_IsWatching)
                {
                    if (!_IsConfigLoaded)
                        LoadConfig();

                    if (_Watcher == null)
                        _Watcher = new FileSystemWatcher(this.PathToFile);
                    _Watcher.Filter = Path.GetFileName(this.PathToFile);
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
            RefreshSection();
            this.FileChanged.Raise(sender, e);
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileCreated event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            RefreshSection();
            this.FileCreated.Raise(sender, e);
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileDeleted event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            RefreshSection();
            this.FileDeleted.Raise(sender, e);
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// Raise FileRenamed event.
        /// </summary>
        /// <param name="sender">Object sending event.</param>
        /// <param name="e">Event argument to include with event.</param>
        protected void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            RefreshSection();
            this.FileRenamed.Raise(sender, e);
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
            lock (_Lock)
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
