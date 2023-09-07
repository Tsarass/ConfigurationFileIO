
# ConfigurationFileIO

Store settings of an application to a configuration file.
Settings can have a category, a name and a value.
Retrieve settings by specifying the category and name.
Custom delimiter supported in case the default one (=) is required in the settings names.

Supported value types: string, int, double, boolean.




## Usage/Examples

### Create configuration file

```c#
// Create a configuration file.
ConfigurationFile configurationFile = new ConfigurationFile("settings.cfg");

// Create a setting with name AutoLogin in category LogIn.
configurationFile.AddNewSetting("LogIn", "AutoLogin", true);

// Save the configuration file with the changes.
configurationFile.WriteSettings();
```

### Read configuration file and change settings

```c#
// Read settings from a configuration file.
ConfigurationFile configurationFile = new ConfigurationFile("settings.cfg");
configurationFile.ReadSettings();

// Get the value for a setting with name AutoLogin in category LogIn and toggle the value.
bool autoLogin = configurationFile.GetSettingValue("LogIn", "AutoLogin").AsBoolean();
configurationFile.SetSettingValue("LogIn", "AutoLogin", !autoLogin);

// Add a new setting AutoLoginUsername.
configurationFile.AddNewSetting("LogIn", "AutoLoginUsername", "User");

// Get the value of a non-existent setting and specify a default value.
int maxFalseAttempts = configurationFile.GetSettingValue("LogIn", "MaxFalseAttempts").AsInteger(3);
// maxFalseAttempts = 3.

// Save the configuration file with the changes.
configurationFile.WriteSettings();
```

