using System.Collections.Generic;
using System.Linq;

namespace ConfigurationFileIO.Models
{
    /// <summary>
    /// Configuration settings.
    /// </summary>
    internal class ConfigurationSettings
    {
        private Dictionary<string, List<ConfigurationSetting>> _configurationSettings = new Dictionary<string, List<ConfigurationSetting>>();

        /// <summary>
        /// Create an empty instance of configuration settings.
        /// </summary>
        internal ConfigurationSettings() 
        { }

        /// <summary>
        /// Add a new settings category if it does not exist. If the supplied category exists, nothing will happen.
        /// </summary>
        /// <param name="category"></param>
        internal void AddSettingsCategory(string category)
        {
            if (!_configurationSettings.ContainsKey(category))
            {
                _configurationSettings.Add(category, new List<ConfigurationSetting>());
            }
        }

        /// <summary>
        /// Remove a settings category if it exists. If the supplied category does not exist, nothing will happen.
        /// </summary>
        /// <param name="category"></param>
        internal void RemoveSettingsCategory(string category)
        {
            if (_configurationSettings.ContainsKey(category))
            {
                _configurationSettings.Remove(category);
            }
        }

        /// <summary>
        /// Add a new configuration setting to a category.<br/>
        /// If the supplied category does not exist, it will be created.<br/>
        /// If the setting with the specified name already exists, it will be overwritten.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="setting"></param>
        internal void AddNewSetting(string category, ConfigurationSetting setting)
        {
            if (!_configurationSettings.ContainsKey(category))
            {
                AddSettingsCategory(category);
            }

            // Look for setting with the same name, so we can overwrite it.
            ConfigurationSetting settingWithSameName = _configurationSettings[category].FirstOrDefault(a => a.Name == setting.Name);
            if (settingWithSameName != null)
            {
                _configurationSettings[category].Remove(settingWithSameName);

            }
            _configurationSettings[category].Add(setting);
        }

        /// <summary>
        /// Remove an existing setting with the specified name from a category.
        /// If the supplied category or setting does not exist, nothing will happen.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="settingName"></param>
        internal void RemoveExistingSetting(string category, string settingName)
        {
            if (_configurationSettings.ContainsKey(category))
            {
                ConfigurationSetting existingSetting = _configurationSettings[category].FirstOrDefault(a => a.Name == settingName);
                if (existingSetting != null)
                {
                    _configurationSettings[category].Remove(existingSetting);
                }
            }
        }

        /// <summary>
        /// Check if a category exists in the configuration settings.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal bool CategoryExists(string category)
        {
            return _configurationSettings.ContainsKey(category);
        }

        /// <summary>
        /// Check if a setting exists in the specified category in the configuration settings.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        internal bool SettingExists(string category, string settingName)
        {
            if (!CategoryExists(category))
            {
                return false;
            }
            if (_configurationSettings[category].FirstOrDefault(a => a.Name == settingName) is null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the setting categories.
        /// </summary>
        /// <returns></returns>
        internal List<string> GetSettingsCategories()
        {
            return _configurationSettings.Keys.ToList();
        }

        /// <summary>
        /// Get the settings in the specified category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>The settings in the specified category or empty list if the category does not exist.</returns>
        internal List<ConfigurationSetting> GetSettingsInCategory(string category)
        {
            if (!CategoryExists(category))
            {
                return new List<ConfigurationSetting>();
            }
            return _configurationSettings[category];
        }

        /// <summary>
        /// Get the value of a setting. 
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <returns>The value of the setting if it exists. Returns an empty value if it doesn't exist.</returns>
        internal ConfigurationValue GetSettingValue(string category, string settingName)
        {
            if (!_configurationSettings.ContainsKey(category))
            {
                return ConfigurationValue.CreateEmpty();
            }

            ConfigurationSetting settingWithName = _configurationSettings[category].FirstOrDefault(a => a.Name == settingName);
            if (settingWithName is null)
            {
                return ConfigurationValue.CreateEmpty();
            }

            return settingWithName.Value;
        }

        /// <summary>
        /// Set the value of a setting. If the setting does not exist, it will be created.
        /// </summary>
        /// <param name="category">Category of the setting.</param>
        /// <param name="settingName">Setting name.</param>
        /// <param name="settingValue">Value for the setting.</param>
        internal void SetSettingValue(string category, string settingName, string settingValue)
        {
            if (!SettingExists(category, settingName))
            {
                AddNewSetting(category, new ConfigurationSetting(settingName, settingValue));
            }
            else
            {
                ConfigurationSetting settingWithName = _configurationSettings[category].FirstOrDefault(a => a.Name == settingName);
                settingWithName.SetValue(settingValue);
            }
        }
    }
}
