using System;
using static ConfigurationFileIO.ConfigurationFileIOExceptions;

namespace ConfigurationFileIO
{
    /// <summary>
    /// Value of a configuration setting.
    /// </summary>
    public class ConfigurationValue
    {
        /// <summary>
        /// Create an empty configuration value.
        /// </summary>
        /// <returns></returns>
        public static ConfigurationValue CreateEmpty()
        {
            return new ConfigurationValue(string.Empty, isEmpty: true);
        }

        private string _value;
        private bool _isEmpty;

        /// <summary>
        /// Create a value for a configuration setting.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isEmpty"></param>
        internal ConfigurationValue(string value, bool isEmpty = false)
        {
            _value = value;
            _isEmpty = isEmpty;
        }

        /// <summary>
        /// Set the value of the setting.
        /// </summary>
        /// <param name="value"></param>
        internal void Set(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Get the value of this setting as a string.
        /// </summary>
        /// <param name="defaultValue">Default value to use if no value was found.</param>
        public string AsString(string defaultValue = default(string))
        {
            if (_isEmpty) return defaultValue;

            return _value;
        }

        /// <summary>
        /// Get the value of this setting as an integer.
        /// </summary>
        /// <returns></returns>
        /// <param name="defaultValue">Default value to use if no value was found.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public int AsInteger(int defaultValue = default(int))
        {
            if (_isEmpty) return defaultValue;

            bool isInteger = int.TryParse(_value, out int result);
            if (!isInteger)
            {
                throw new InvalidRequestedSettingFormat(typeof(int));
            }

            return result;
        }

        /// <summary>
        /// Get the value of this setting as a double.
        /// </summary>
        /// <returns></returns>
        /// <param name="defaultValue">Default value to use if no value was found.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public double AsDouble(double defaultValue = default(double))
        {
            if (_isEmpty) return defaultValue;

            bool isDouble = int.TryParse(_value, out int result);
            if (!isDouble)
            {
                throw new InvalidRequestedSettingFormat(typeof(double));
            }

            return result;
        }

        /// <summary>
        /// Get the value of this setting as a boolean.
        /// </summary>
        /// <returns></returns>
        /// <param name="defaultValue">Default value to use if no value was found.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public bool AsBoolean(bool defaultValue = default(bool))
        {
            if (_isEmpty) return defaultValue;

            bool isBoolean = bool.TryParse(_value, out bool result);
            if (!isBoolean)
            {
                throw new InvalidRequestedSettingFormat(typeof(bool));
            }

            return result;
        }

        /// <summary>
        /// Check for value equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ConfigurationValue other = obj as ConfigurationValue;
            if (other == null) return false;

            return this._value == other._value && this._isEmpty == other._isEmpty;
        }

        /// <summary>
        /// Default hash code function.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() 
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Get the value as string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _value;
        }
    }
}
