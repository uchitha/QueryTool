using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Qt.Core
{
    public class UnityBootStrapper
    {
        public string ConfigurationFileName { get; set; }
        private volatile bool _isInitialized;
        private static readonly object Lock = new object();

        public void Initialize()
        {
            if (_isInitialized) return;
            lock (Lock)
            {
                if (_isInitialized) return;
                var container = CreateContainer();
                ServiceLocator.SetLocatorProvider(() => new QtUnityServiceLocator(container));
                _isInitialized = true;
            }

        }

        private IUnityContainer CreateContainer()
        {
            ConfigurationFileName = string.IsNullOrEmpty(ConfigurationFileName) ? "unity.config" : ConfigurationFileName;
            var map = new ExeConfigurationFileMap { ExeConfigFilename = ConfigurationFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            if (!config.HasFile)
                throw new ConfigurationErrorsException(string.Format("Configuration file {0} was not found", config.FilePath));
            var section = config.GetSection("unity") as UnityConfigurationSection;
            if (section == null)
                throw new ConfigurationErrorsException(string.Format("Unity section was not found at config file {0}", config.FilePath));

            var container = new UnityContainer();
            section.Configure(container, "container");
            return container;
        }
    }
}
