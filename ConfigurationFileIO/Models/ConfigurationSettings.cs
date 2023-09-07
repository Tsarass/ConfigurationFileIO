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

        internal void AddSettingsCategory(string category)
        {
            if (!_configurationSettings.ContainsKey(category))
            {
                _configurationSettings.Add(category, new List<ConfigurationSetting>());
            }
        }

        internal void RemoveSettingsCategory(string category)
        {
            if (_configurationSettings.ContainsKey(category))
            {
                _configurationSettings.Remove(category);
            }
        }

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

        internal bool CategoryExists(string category)
        {
            return _configurationSettings.ContainsKey(category);
        }

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

        internal List<string> GetSettingsCategories()
        {
            return _configurationSettings.Keys.ToList();
        }

        internal List<ConfigurationSetting> GetSettingsInCategory(string category)
        {
            if (!CategoryExists(category))
            {
                return new List<ConfigurationSetting>();
            }
            return _configurationSettings[category];
        }

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
