using Fosol.Common.Extensions.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// Provides a way to watch a specific configuration section file for changes.
    /// If a change occurs with the section it will fire the appropriate event.
    /// Remember to call the Start method to begin watching the configuration file.
    /// </summary>
    /// <typeparam name="T">Type of ConfigurationSection object being watched.</typeparam>
    public sealed class ConfigurationSectionFileWatcher<T> : ConfigurationSectionFileWatcherBase<T>
        where T : ConfigurationSection, new()
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The name of the configuration section.
        /// </summary>
        public string SectionName { get; private set; }

        /// <summary>
        /// get - Application configuration file.
        /// </summary>
        private System.Configuration.Configuration Configuration
        {
            get { return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of a ConfigurationSectionFileWatcher object.
        /// Remember to call the Start method to begin watching the configuration file.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "sectionName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "sectionName" cannot be null.</exception>
        /// <param name="sectionName">Name of the section within the configuration file.</param>
        public ConfigurationSectionFileWatcher(string sectionName)
            : base(GetPathToFile(sectionName))
        {
            Validation.Assert.IsNotNullOrEmpty(sectionName, "sectionName");

            this.SectionName = sectionName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the path to the section configuration file.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Section configuration file not found.</exception>
        /// <param name="sectionName">Name of the section used in the configuration.</param>
        /// <returns>Path to configuration file.</returns>
        private static string GetPathToFile(string sectionName)
        {
            // Just in case the <configSections> is not listed first it is important to attempt to fetch before casting.
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection(sectionName);
            var section = config as T;

            if (section == null)
                throw new ConfigurationErrorsException(string.Format(Resources.Strings.Exception_ConfigurationSectionNotFound, sectionName));

            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            if (!string.IsNullOrEmpty(section.SectionInformation.ConfigSource))
                return Path.Combine(path, section.SectionInformation.ConfigSource);
            else
                return path;
        }

        /// <summary>
        /// Refresh and load the configuration section.
        /// </summary>
        protected override void RefreshSection()
        {
            lock (_Lock)
            {
                ConfigurationManager.RefreshSection(this.SectionName);
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
            this.ConfigSection = (T)Configuration.GetSection(this.SectionName);

            if (this.ConfigSection == null)
                throw new ConfigurationErrorsException(string.Format(Resources.Strings.Exception_ConfigurationSectionNotFound, this.SectionName));

            _IsConfigLoaded = true;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
