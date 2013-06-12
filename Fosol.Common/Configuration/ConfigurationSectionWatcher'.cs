using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// The ConfigurationSectionWather will watch the specified section within the configuration to detect if it changes.
    /// </summary>
    /// <typeparam name="T">Type of the ConfigurationSection being watched.</typeparam>
    public sealed class ConfigurationSectionWatcher<T> 
        : ConfigurationSectionFileWatcherBase<T>
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
            lock (_BigLock)
            {
                ConfigurationManager.RefreshSection(this.SectionName);
                this.Section = null;
            }
            LoadConfig();
        }

        /// <summary>
        /// Load the configuration section.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        protected override void LoadConfig()
        {
            lock (_BigLock)
            {
                try
                {
                    this.Section = (T)Configuration.GetSection(this.SectionName);

                    if (this.Section == null)
                        throw new ConfigurationErrorsException(string.Format(Resources.Strings.Exception_Configuration_Section_Not_Found, this.SectionName));

                    // Set the FilePath if it hasn't already been set.
                    if (string.IsNullOrEmpty(this.FilePath))
                    {
                        if (!string.IsNullOrEmpty(this.Section.SectionInformation.ConfigSource))
                            this.FilePath = this.Section.SectionInformation.ConfigSource;
                        else
                            this.FilePath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                    }
                    this.IsConfigLoaded = true;
                }
                catch (Exception ex)
                {
                    base.OnConfigurationError(ex);
                }
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
