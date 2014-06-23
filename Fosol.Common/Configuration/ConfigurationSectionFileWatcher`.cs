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
        private readonly bool _IsExternalConfig;
        #endregion

        #region Properties
        /// <summary>
        /// get - The name of the configuration section.
        /// </summary>
        public string SectionName { get; private set; }

        /// <summary>
        /// get - Whether the configuration file is an full external System.Configuration file.
        /// </summary>
        private bool IsExternalConfig 
        {
            get { return _IsExternalConfig; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of a ConfigurationSectionFileWatcher object.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "sectionNameOrFilePath" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "sectionNameOrFilePath" cannot be null.</exception>
        /// <param name="sectionNameOrFilename">Full path to the section configuration file, or the section name of the configuration.</param>
        public ConfigurationSectionFileWatcher(string sectionNameOrFilename)
            : base()
        {
            Validation.Assert.IsNotNullOrEmpty(sectionNameOrFilename, "sectionNameOrFilename");

            var full_path = System.IO.Path.Combine(Environment.CurrentDirectory, sectionNameOrFilename);

            // Check to see if the file exists at the specified path.  If it doesn't assume this is a section name instead.
            if (System.IO.File.Exists(sectionNameOrFilename))
                this.Filename = sectionNameOrFilename;
            else if (System.IO.File.Exists(full_path))
                this.Filename = full_path;
            else
                this.SectionName = sectionNameOrFilename;
        }

        /// <summary>
        /// Creates a new instance of a ConfigurationSectionFileWatcher object.
        /// 
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameters "externalConfigPath" and "sectionName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "externalConfigPath" and "sectionName" cannot be null.</exception>
        /// <param name="externalConfigFilename">Path to the external System.Configuration file.</param>
        /// <param name="sectionName">Name of the custom section within the configuraiton file.</param>
        public ConfigurationSectionFileWatcher(string externalConfigFilename, string sectionName)
        {
            Validation.Assert.IsNotNullOrEmpty(externalConfigFilename, "externalConfigFilename");
            Validation.Assert.IsNotNullOrEmpty(sectionName, "sectionName");

            var full_path = System.IO.Path.Combine(Environment.CurrentDirectory, externalConfigFilename);
            if (System.IO.File.Exists(externalConfigFilename))
                this.Filename = externalConfigFilename;
            else
                this.Filename = full_path;

            this.SectionName = sectionName;
            _IsExternalConfig = true;
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
                if (this.IsExternalConfig)
                {
                    // This is pointing to an external System.Configuration file which will contain a section within it.
                    if (!System.IO.File.Exists(this.Filename)
                        || !System.IO.File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, this.Filename)))
                        throw new ConfigurationErrorsException(string.Format(Resources.Multilingual.Exception_Configuration_Section_Not_Found, this.Filename));

                    var file_map = new ConfigurationFileMap(this.Filename);
                    this.Configuration = ConfigurationManager.OpenMappedMachineConfiguration(file_map);
                    this.Section = (T)this.Configuration.GetSection(this.SectionName);

                    if (this.Section == null)
                        throw new ConfigurationErrorsException(string.Format(Resources.Multilingual.Exception_Configuration_Section_Not_Found, this.SectionName));
                }
                else if (!string.IsNullOrEmpty(this.SectionName))
                {
                    this.Section = (T)Configuration.GetSection(this.SectionName);

                    if (this.Section == null)
                        throw new ConfigurationErrorsException(string.Format(Resources.Multilingual.Exception_Configuration_Section_Not_Found, this.SectionName));

                    // Set the FilePath if it hasn't already been set.
                    if (string.IsNullOrEmpty(this.Filename))
                    {
                        if (!string.IsNullOrEmpty(this.Section.SectionInformation.ConfigSource))
                            this.Filename = this.Section.SectionInformation.ConfigSource;
                        else
                            this.Filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                    }
                }
                else
                {
                    // The external independant section configuration file.
                    this.Section = ConfigurationSectionFileWatcherBase<T>.DeserializeSection(this.Filename);

                    if (this.Section == null)
                        throw new ConfigurationErrorsException(string.Format(Resources.Multilingual.Exception_Configuration_Section_Not_Found, this.Filename));

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
