using System.Collections.Generic;
using System.IO;
using static ConfigurationFileIO.ConfigurationFileIOExceptions;

namespace ConfigurationFileIO.Models
{
    /// <summary>
    /// Reads from a configuration file.
    /// </summary>
    internal class ConfigurationFileReader
    {
        private string _configurationFilePath;
        private char _delimiter;

        // State variables.
        private bool _isWithinCategory;
        private string _currentCategory;
        ConfigurationSettings _configurationSettings;
        private List<string> _errors;
        private int _currentLineNumber;

        /// <summary>
        /// Read from the configuration file with the specified file path.
        /// </summary>
        /// <param name="configurationFilePath"></param>
        internal ConfigurationFileReader(string configurationFilePath, char delimiter)
        {
            _configurationFilePath = configurationFilePath;
            _delimiter = delimiter;
        }

        /// <summary>
        /// Reset all state variables.
        /// </summary>
        private void ResetState()
        {
            _currentLineNumber = 1;
            _isWithinCategory = false;
            _currentCategory = string.Empty;
            _configurationSettings = new ConfigurationSettings();
            _errors = new List<string>();
        }

        /// <summary>
        /// Read the configuration settings from the file.
        /// </summary>
        /// <returns>The settings read from the file.</returns>
        /// <exception cref="AccessingConfigurationFileFailed"></exception>
        internal ConfigurationSettings ReadConfigurationSettings()
        {
            ResetState();
            string[] lines;

            try
            {
                lines = File.ReadAllLines(_configurationFilePath);
            }
            catch
            {
                throw new AccessingConfigurationFileFailed();
            }

            foreach (string line in lines)
            {
                ProcessLine(line);
                _currentLineNumber++;
            }


            return _configurationSettings;
        }

        /// <summary>
        /// Process a line of the configuration file.<br/>
        /// Comment or empty lines are ignored.<br/>
        /// Only settings under a category will be assumed as valid.
        /// </summary>
        /// <param name="line"></param>
        private void ProcessLine(string line)
        {
            // Skip comments or empty lines.
            if (IsCommentOrEmpty(line))
            {
                return;
            }

            // Process lines that start a category.
            if (IsCategoryLine(line))
            {
                string categoryName = GetCategoryName(line);
                if (string.IsNullOrEmpty(categoryName))
                {
                    _errors.Add($"Line {_currentLineNumber} starts a category but has empty category name. Content: {line}");
                }
                else
                {
                    _isWithinCategory = true;
                    _currentCategory = categoryName;
                    _configurationSettings.AddSettingsCategory(categoryName);
                }

                return;
            }

            // Process setting lines only if a category is active.
            if (_isWithinCategory)
            {
                // Settings will be written in the format <name>=<value>
                string[] tokens = line.Split(_delimiter);
                if (tokens.Length < 2)
                {
                    _errors.Add($"Line {_currentLineNumber} has invalid setting syntax. Content: {line}");
                }
                else
                {
                    string settingName = tokens[0];
                    string settingValue = tokens[1];

                    ConfigurationSetting newSetting = new ConfigurationSetting(settingName, settingValue);
                    _configurationSettings.AddNewSetting(_currentCategory, newSetting);
                }
            }
        }

        private bool IsCommentOrEmpty(string line)
        {
            return line.StartsWith(@"//") || (line == "");
        }

        private bool IsCategoryLine(string line)
        {
            return line.StartsWith("[") && line.EndsWith("]");
        }

        /// <summary>
        /// Get the category name from a category line.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string GetCategoryName(string line)
        {
            if (line.Length <= 2)
            {
                return string.Empty;
            }

            return line.Substring(1, line.Length - 2);
        }
    }
}
