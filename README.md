
# ConfigurationFileIO

Store settings of an application to a configuration file.
Settings can have a category, a name and a value.
Retrieve settings by specifying the category and name.
Custom delimiter supported in case the default one (=) is required in the settings names.

Supported value types: string, int, double, boolean.




## Usage/Examples

```c#
// Read settings from a configuration file.
ConfigurationFile configurationFile = new ConfigurationFile("settings.cfg");
configurationFile.ReadSettings();

// Get the value for a setting with name AutoLogin in category LogIn.
bool autoLogin = configurationFile.getSettingValue("LogIn", "AutoLogin").AsBoolean();
// Change the value.
configurationFile.setSettingValue("LogIn", "AutoLogin", true);

// Add a new setting AutoLoginUsername.
ConfigurationFile.AddNewSetting("LogIn", "AutoLoginUsername", "User");

// Save the configuration file with the changes.
configurationFile.SaveSettings();
```

