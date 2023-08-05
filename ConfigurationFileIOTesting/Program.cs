using ConfigurationFileIO;
using System;

namespace ConfigurationFileIOTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigurationFile configurationFile = new ConfigurationFile(@"..\..\..\test1.txt");
            var value = configurationFile.GetSettingValue("Backup", "SERVER_PATH");
        }
    }
}