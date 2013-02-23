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
    public sealed class ConfigurationSectionExternalFileWatcher<T> : ConfigurationSectionFileWatcherBase<T>
        where T : ConfigurationSection, new()
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of a ConfigurationSectionFileWatcher object.
        /// Remember to call the Start method to begin watching the configuration file.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "sectionName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "sectionName" cannot be null.</exception>
        /// <param name="path">Full path to the section configuration file.</param>
        public ConfigurationSectionExternalFileWatcher(string path)
            : base(path)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Refresh and load the configuration section.
        /// </summary>
        protected override void RefreshSection()
        {
            lock (_Lock)
            {
                _IsConfigLoaded = false;
                LoadConfig();
            }
        }

        /// <summary>
        /// Load the configuration section.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        protected override void LoadConfig()
        {
            this.ConfigSection = DeserializeSection(this.FullPath);

            if (this.ConfigSection == null)
                throw new ConfigurationErrorsException(string.Format(Resources.Strings.Exception_ConfigurationSectionNotFound, Path.GetFileName(this.FullPath)));

            _IsConfigLoaded = true;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
