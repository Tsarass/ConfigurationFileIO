using System;

namespace ConfigurationFileIO
{
    /// <summary>
    /// Exceptions relevant to configuration file IO.
    /// </summary>
    public class ConfigurationFileIOExceptions
    {
        /// <summary>
        /// Accessing a configuration file failed.
        /// </summary>
        public class AccessingConfigurationFileFailed : Exception 
        {
            /// <summary>
            /// Accessing a configuration file failed.
            /// </summary>
            public AccessingConfigurationFileFailed() : base("Could not access configuration file.")
            {
                    
            }
        }

        /// <summary>
        /// Writing a configuration file failed.
        /// </summary>
        public class WritingConfigurationFileFailed : Exception
        {
            /// <summary>
            /// Writing a configuration file failed.
            /// </summary>
            public WritingConfigurationFileFailed() : base("Could not write to the configuration file.")
            {

            }
        }

        /// <summary>
        /// A setting value was requested with the wrong format.
        /// </summary>
        public class InvalidRequestedSettingFormat : Exception
        {
            /// <summary>
            /// A setting value was requested with the wrong format.
            /// </summary>
            public InvalidRequestedSettingFormat(Type requestedValueType)
                : base($"Invalid format for setting requested: {requestedValueType.Name}")
            { }
        }
    }
}
