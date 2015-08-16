using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace VolnovNotificator
{
    class AppSettingsWrapper
    {
        readonly ExeConfigurationFileMap _confFileMap = new ExeConfigurationFileMap();
        readonly Configuration _configuration;
        readonly KeyValueConfigurationCollection _appSettingsKeyCollection;

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
            return (from KeyValueConfigurationElement keyValueElement in _appSettingsKeyCollection
                         select new KeyValuePair<string, string>(keyValueElement.Key, keyValueElement.Value)).ToList();
        }
    }
}