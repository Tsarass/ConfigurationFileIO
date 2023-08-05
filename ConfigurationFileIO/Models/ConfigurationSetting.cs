namespace ConfigurationFileIO.Models
{
    /// <summary>
    /// A configuration setting with a name and value.
    /// </summary>
    internal class ConfigurationSetting
    {
        private string _name;
        private ConfigurationValue _value;

        /// <summary>
        /// Create a new configuration setting with a name and value.
        /// </summary>
        /// <param name="name">Name of the setting.</param>
        /// <param name="value">Value of the setting as string.</param>
        internal ConfigurationSetting(string name, string value)
        {
            _name = name;
            _value = new ConfigurationValue(value);
        }

        /// <summary>Name of the setting.</summary>
        internal string Name { get { return _name; } }
        /// <summary>Value of the setting.</summary>
        internal ConfigurationValue Value { get { return _value; } }

        /// <summary>
        /// Set the value of the setting.
        /// </summary>
        /// <param name="settingValue"></param>
        internal void SetValue(string settingValue)
        {
            _value.Set(settingValue);
        }

        public override bool Equals(object obj)
        {
            ConfigurationSetting other = obj as ConfigurationSetting;
            if (other == null) return false;

            return this.Name == other.Name && this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"\"{_name}\":\"{_value}\"";
        }
    }
}
