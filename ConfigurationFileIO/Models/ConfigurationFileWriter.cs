using System.Collections.Generic;
using System.IO;
using static ConfigurationFileIO.ConfigurationFileIOExceptions;

namespace ConfigurationFileIO.Models
{
    internal class ConfigurationFileWriter
    {
        private string _configurationFilePath;
        private char _delimiter;

        /// <summary>
        /// Write to a configuration file with the specified file path.
        /// </summary>
        /// <param name="configurationFilePath"></param>
        /// <param name="delimiter"></param>
        internal ConfigurationFileWriter(string configurationFilePath, char delimiter)
        {
            _configurationFilePath = configurationFilePath;
            _delimiter = delimiter;
        }

        /// <summary>
        /// Write the configuration settings to the file.
        /// </summary>
        /// <param name="configurationSettings">Configuration settings to write.</param>
        /// <exception cref="WritingConfigurationFileFailed"></exception>
        internal void WriteConfigurationSettings(ConfigurationSettings configurationSettings)
        {
            List<string> configurationFileLines = new List<string>();
            configurationSettings.GetSettingsCategories().ForEach(category =>
            {
                configurationFileLines.Add($"[{category}]");
                configurationSettings.GetSettingsInCategory(category).ForEach(setting =>
                {
                    configurationFileLines.Add($"{setting.Name}{_delimiter}{setting.Value}");
                });
                configurationFileLines.Add($"\r\n");
            });

            try
            {
                File.WriteAllLines(_configurationFilePath, configurationFileLines.ToArray());
            }
            catch
            {
                throw new WritingConfigurationFileFailed();
            }
            
        }
    }
}
