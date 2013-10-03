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
    public sealed class ConfigurationSectionFileWatcher<T> 
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
        /// Create a new instance of a ConfigurationSectionFileWatcher object.
        /// Call the Start method to begin watching the configuration file.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "sectionNameOrFilePath" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "sectionNameOrFilePath" cannot be null.</exception>
        /// <param name="sectionNameOrFilePath">Full path to the section configuration file, or the section name of the configuration.</param>
        public ConfigurationSectionFileWatcher(string sectionNameOrFilePath)
            : base()
        {
            Validation.Assert.IsNotNullOrEmpty(sectionNameOrFilePath, "sectionNameOrFilePath");

            // Check to see if the file exists at the specified path.  If it doesn't assume this is a section name instead.
            if (System.IO.File.Exists(sectionNameOrFilePath))
                this.FilePath = sectionNameOrFilePath;
            else
                this.SectionName = sectionNameOrFilePath;
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
                if (this.IsConfigLoaded)
                {
                    this.IsConfigLoaded = false;
                    ConfigurationManager.RefreshSection(this.SectionName);
                    this.Section = null;
                    LoadConfigWithoutLock();
                }
            }
        }

        /// <summary>
        /// Load the configuration section.
        /// Lock object to ensure thread safety.
        /// If the SectionName is specified it will attempt to load the section the standard way.
        /// If the SectionName is not specified it will attempt to deserialize the file at the FilePath.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        protected override void LoadConfig()
        {
            lock (_BigLock)
            {
                LoadConfigWithoutLock();
            }
        }

        /// <summary>
        /// Load the configuration section.
        /// If the SectionName is specified it will attempt to load the section the standard way.
        /// If the SectionName is not specified it will attempt to deserialize the file at the FilePath.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Configuration Section did not exist.</exception>
        private void LoadConfigWithoutLock()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.SectionName))
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
                }
                else
                {
                    this.Section = ConfigurationSectionFileWatcherBase<T>.DeserializeSection(this.FilePath);

                    if (this.Section == null)
                        throw new ConfigurationErrorsException(string.Format(Resources.Strings.Exception_Configuration_Section_Not_Found, this.FilePath));

                    if (string.IsNullOrEmpty(this.SectionName))
                        this.SectionName = Section.SectionInformation.Name;
                }

                this.IsConfigLoaded = true;
            }
            catch (Exception ex)
            {
                base.OnError(ex);
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
