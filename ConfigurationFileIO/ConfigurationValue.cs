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
            return new ConfigurationValue(string.Empty);
        }

        private string _value;

        /// <summary>
        /// Create a value for a configuration setting.
        /// </summary>
        /// <param name="value"></param>
        internal ConfigurationValue(string value)
        {
            _value = value;
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
        public string AsString()
        {
            return _value;
        }

        /// <summary>
        /// Get the value of this setting as an integer.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int AsInteger()
        {
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
        /// <exception cref="InvalidOperationException"></exception>
        public double AsDouble()
        {
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
        /// <exception cref="InvalidOperationException"></exception>
        public bool AsBoolean()
        {
            bool isBoolean = bool.TryParse(_value, out bool result);
            if (!isBoolean)
            {
                throw new InvalidRequestedSettingFormat(typeof(bool));
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            ConfigurationValue other = obj as ConfigurationValue;
            if (other == null) return false;

            return this._value == other._value;
        }

        public override int GetHashCode() 
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
