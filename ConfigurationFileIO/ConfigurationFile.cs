﻿using System.Collections.Generic;
using System.Linq;
using static ConfigurationFileIO.ConfigurationFileIOExceptions;
using ConfigurationFileIO.Models;
using System.IO;

namespace ConfigurationFileIO
{
    /// <summary>
    /// Read from or write settings to a configuration file.
    /// </summary>
    public class ConfigurationFile
    {
        private string _configurationFilePath;
        private ConfigurationSettings _settings;
        private char _delimiter;

        /// <summary>
        /// Set up a configuration file to read from or write settings to.<br/><br/>
        /// When requesting a value of a non-existent category or setting, an empty value will be returned.<br/>
        /// Use <see cref="CategoryExists(string)"/> and <see cref="SettingExists(string, string)"/> to check if a category
        /// or a setting exists in the configuration file.
        /// </summary>
        /// <param name="configurationFilePath">File path to the configuration file.</param>
        /// <param name="demiliter">Delimiter which will separate setting names from values.</param>
        /// <exception cref="AccessingConfigurationFileFailed"></exception>
        public ConfigurationFile(string configurationFilePath, char demiliter='=')
        {
            _configurationFilePath = configurationFilePath;
            _delimiter = demiliter;

            if (!File.Exists(_configurationFilePath))
            {
                try
                {
                    File.WriteAllText(_configurationFilePath, "");
                }
                catch
                {
                    throw new System.Exception($"Could not create configuration file with path {_configurationFilePath}.");
                }
                
            }
        }

        /// <summary>
        /// Read the settings from the configuration file.
        /// </summary>
        /// <exception cref="AccessingConfigurationFileFailed"></exception>
        private void ReadSettings()
        {
            ConfigurationFileReader configurationFileReader = new ConfigurationFileReader(_configurationFilePath, _delimiter);
            try
            {
                _settings = configurationFileReader.ReadConfigurationSettings();
            }
            catch (AccessingConfigurationFileFailed)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the settings for the configuration file safely.
        /// </summary>
        /// <returns></returns>
        private ConfigurationSettings GetSettingsSafe()
        {
            if (_settings is null)
            {
                ReadSettings();
            }

            return _settings;
        }

        /// <summary>
        /// Write the settings to the configuration file.
        /// </summary>
        public void WriteSettings()
        {
            ConfigurationFileWriter configurationFileReader = new ConfigurationFileWriter(_configurationFilePath, _delimiter);
            try
            {
                configurationFileReader.WriteConfigurationSettings(_settings);
            }
            catch (AccessingConfigurationFileFailed)
            {
                throw;
            }
        }

        ///<inheritdoc cref="ConfigurationSettings.CategoryExists(string)"/>
        public bool CategoryExists(string category)
        {
            return GetSettingsSafe().CategoryExists(category);
        }

        ///<inheritdoc cref="ConfigurationSettings.SettingExists"/>
        public bool SettingExists(string category, string settingName)
        {
            return GetSettingsSafe().SettingExists(category, settingName);
        }

        ///<inheritdoc cref="ConfigurationSettings.GetSettingsCategories"/>
        public List<string> GetSettingsCategories()
        {
            return GetSettingsSafe().GetSettingsCategories();
        }

        ///<inheritdoc cref="ConfigurationSettings.GetSettingsInCategory(string)"/>
        public List<string> GetSettingsInCategory(string category)
        {
            return GetSettingsSafe().GetSettingsInCategory(category).Select(a => a.Name).ToList();
        }

        ///<inheritdoc cref="ConfigurationSettings.GetSettingValue(string, string)"/>
        public ConfigurationValue GetSettingValue(string category, string settingName)
        {
            return GetSettingsSafe().GetSettingValue(category, settingName);
        }

        /// <inheritdoc cref="ConfigurationSettings.AddSettingsCategory(string)"/>
        public void AddSettingsCategory(string category)
        {
            GetSettingsSafe().AddSettingsCategory(category);
        }

        /// <inheritdoc cref="ConfigurationSettings.RemoveSettingsCategory(string)"/>
        public void RemoveSettingsCategory(string category)
        {
            GetSettingsSafe().RemoveSettingsCategory(category);
        }

        /// <inheritdoc cref="ConfigurationSettings.AddNewSetting(string, ConfigurationSetting)"/>
        public void AddNewSetting(string category, string settingName, string settingValue)
        {
            GetSettingsSafe().AddNewSetting(category, new ConfigurationSetting(settingName, settingValue));
        }

        /// <inheritdoc cref="ConfigurationSettings.RemoveExistingSetting(string, string)"/>
        public void RemoveExistingSetting(string category, string settingName)
        {
            GetSettingsSafe().RemoveExistingSetting(category, settingName);
        }
    }
}
