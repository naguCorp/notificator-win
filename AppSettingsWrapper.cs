using System.Collections.Generic;
using System.Configuration;

namespace VolnovNotificator
{
    class AppSettingsWrapper
    {
        ExeConfigurationFileMap _confFileMap = new ExeConfigurationFileMap();
        Configuration _configuration;
        KeyValueConfigurationCollection _appSettingsKeyCollection;

        public AppSettingsWrapper(string configPath)
        {
            _confFileMap.ExeConfigFilename = configPath;
            _configuration = ConfigurationManager.OpenMappedExeConfiguration(_confFileMap, ConfigurationUserLevel.None);
            _appSettingsKeyCollection = _configuration.AppSettings.Settings;
        }

        public void EditRecord(string key, string value)
        {
            _appSettingsKeyCollection.Remove(key);
            _appSettingsKeyCollection.Add(key, value);
            _configuration.Save();
        }

        public void RemoveRecord(string key)
        {
            _appSettingsKeyCollection.Remove(key);
        }

        public List<KeyValuePair<string, string>> GetAllConfigData()
        {
            List<KeyValuePair<string, string>> allConfigData = new List<KeyValuePair<string, string>>();
            foreach (KeyValueConfigurationElement keyValueElement in _appSettingsKeyCollection)
                allConfigData.Add(new KeyValuePair<string, string>(keyValueElement.Key, keyValueElement.Value));
            return allConfigData;
        } 
    }
}