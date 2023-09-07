using System.Collections.Generic;
using System.Linq;
using static ConfigurationFileIO.ConfigurationFileIOExceptions;
using ConfigurationFileIO.Models;

namespace ConfigurationFileIO
{
    /// <summary>
    /// Read from or write settings to a configuration file.
    /// </summary>
    public class ConfigurationFile
    {
        private string _configurationFilePath;
        private ConfigurationSettings _settings = new ConfigurationSettings();
        private char _delimiter;

        /// <summary>
        /// Set up a configuration file to read from or write settings to.<br/><br/>
        /// When requesting a value of a non-existent category or setting, an empty value will be returned.<br/>
        /// Use <see cref="CategoryExists(string)"/> and <see cref="SettingExists(string, string)"/> to check if a category
        /// or a setting exists in the configuration file.
        /// </summary>
        /// <param name="configurationFilePath">The file path to the configuration file.</param>
        /// <param name="demiliter">Delimiter which will separate setting names from values.</param>
        public ConfigurationFile(string configurationFilePath, char demiliter='=')
        {
            _configurationFilePath = configurationFilePath;
            _delimiter = demiliter;
        }

        /// <summary>
        /// Read the settings from the configuration file.
        /// </summary>
        /// <exception cref="AccessingConfigurationFileFailed"></exception>
        public void ReadSettings()
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

        /// <summary>
        /// Check if a category exists in the configuration settings.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool CategoryExists(string category)
        {
            return _settings.CategoryExists(category);
        }

        /// <summary>
        /// Check if a setting exists in the specified category in the configuration settings.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public bool SettingExists(string category, string settingName)
        {
            return _settings.SettingExists(category, settingName);
        }

        /// <summary>
        /// Get the setting categories.
        /// </summary>
        /// <returns></returns>
        public List<string> GetSettingsCategories()
        {
            return _settings.GetSettingsCategories();
        }

        /// <summary>
        /// Get the settings in the specified category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>The settings in the specified category or empty list if the category does not exist.</returns>
        public List<string> GetSettingsInCategory(string category)
        {
            return _settings.GetSettingsInCategory(category).Select(a => a.Name).ToList();
        }

        /// <summary>
        /// Get the value of a setting. 
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <returns>The value of the setting if it exists. Returns an empty value if it doesn't exist.</returns>
        public ConfigurationValue GetSettingValue(string category, string settingName)
        {
            return _settings.GetSettingValue(category, settingName);
        }

        /// <summary>
        /// Set the value of a setting. If the setting does not exist, it will be created.
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Value for the setting.</param>
        public void SetSettingValue(string category, string settingName, string settingValue)
        {
            _settings.SetSettingValue(category, settingName, settingValue);
        }

        /// <summary>
        /// Set the value of a setting. If the setting does not exist, it will be created.
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Value for the setting.</param>
        public void SetSettingValue(string category, string settingName, int settingValue)
        {
            SetSettingValue(category, settingName, settingValue.ToString());
        }

        /// <summary>
        /// Set the value of a setting. If the setting does not exist, it will be created.
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Value for the setting.</param>
        public void SetSettingValue(string category, string settingName, double settingValue)
        {
            SetSettingValue(category, settingName, settingValue.ToString());
        }

        /// <summary>
        /// Set the value of a setting. If the setting does not exist, it will be created.
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Value for the setting.</param>
        public void SetSettingValue(string category, string settingName, bool settingValue)
        {
            SetSettingValue(category, settingName, settingValue.ToString());
        }

        /// <summary>
        /// Add a new settings category if it does not exist. If the supplied category exists, nothing will happen.
        /// </summary>
        /// <param name="category"></param>
        public void AddSettingsCategory(string category)
        {
            _settings.AddSettingsCategory(category);
        }

        /// <summary>
        /// Remove a settings category if it exists. If the supplied category does not exist, nothing will happen.
        /// </summary>
        /// <param name="category"></param>
        public void RemoveSettingsCategory(string category)
        {
            _settings.RemoveSettingsCategory(category);
        }

        /// <summary>
        /// Add a new configuration setting to a category.<br/>
        /// If the supplied category does not exist, it will be created.<br/>
        /// If the setting with the specified name already exists, it will be overwritten.
        /// </summary>
        /// <param name="category">Setting category.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Setting value.</param>
        public void AddNewSetting(string category, string settingName, string settingValue)
        {
            _settings.AddNewSetting(category, new ConfigurationSetting(settingName, settingValue));
        }

        /// <summary>
        /// Add a new configuration setting to a category.<br/>
        /// If the supplied category does not exist, it will be created.<br/>
        /// If the setting with the specified name already exists, it will be overwritten.
        /// </summary>
        /// <param name="category">Setting category.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Setting value.</param>
        public void AddNewSetting(string category, string settingName, int settingValue)
        {
            AddNewSetting(category, settingName, settingValue.ToString());
        }

        /// <summary>
        /// Add a new configuration setting to a category.<br/>
        /// If the supplied category does not exist, it will be created.<br/>
        /// If the setting with the specified name already exists, it will be overwritten.
        /// </summary>
        /// <param name="category">Setting category.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Setting value.</param>
        public void AddNewSetting(string category, string settingName, double settingValue)
        {
            AddNewSetting(category, settingName, settingValue.ToString());
        }

        /// <summary>
        /// Add a new configuration setting to a category.<br/>
        /// If the supplied category does not exist, it will be created.<br/>
        /// If the setting with the specified name already exists, it will be overwritten.
        /// </summary>
        /// <param name="category">Setting category.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Setting value.</param>
        public void AddNewSetting(string category, string settingName, bool settingValue)
        {
            AddNewSetting(category, settingName, settingValue.ToString());
        }


        /// <summary>
        /// Remove an existing setting with the specified name from a category.
        /// If the supplied category or setting does not exist, nothing will happen.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="settingName"></param>
        public void RemoveExistingSetting(string category, string settingName)
        {
            _settings.RemoveExistingSetting(category, settingName);
        }
    }
}
