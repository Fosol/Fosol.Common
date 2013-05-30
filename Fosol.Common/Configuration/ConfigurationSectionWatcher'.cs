using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    public sealed class ConfigurationSectionWatcher<T> : ConfigurationSectionFileWatcherBase<T>
        where T : ConfigurationSection, new()
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The name of the configuration section.
        /// </summary>
        public string SectionName { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of a ConfigurationSectionWatcher object.
        /// Call the Start method to begin watching the configuration file.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "sectionName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "sectionName" cannot be null.</exception>
        /// <param name="sectionName">Name of the configuration section.</param>
        public ConfigurationSectionWatcher(string sectionName)
            : base()
        {
            Validation.Assert.IsNotNullOrEmpty(sectionName, "sectionName");

            this.SectionName = sectionName;
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

            // Set the FilePath if it hasn't already been set.
            if (string.IsNullOrEmpty(this.FilePath))
            {
                if (!string.IsNullOrEmpty(this.ConfigSection.SectionInformation.ConfigSource))
                    this.FilePath = this.ConfigSection.SectionInformation.ConfigSource;
                else
                    this.FilePath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            }
            _IsConfigLoaded = true;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
