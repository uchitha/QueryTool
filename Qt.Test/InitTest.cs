using System.Configuration;
using NUnit.Framework;
using Qt.Core;

namespace Qt.Test
{
    [SetUpFixture]
    public class InitTest
    {

        [SetUp]
        public void EnvironmentSetup()
        {
            InitializeDi();
        }

        [TearDown]
        public void TearDown()
        {

        }

        /// <summary>
        /// Initialize Unity
        /// </summary>
        private void InitializeDi()
        {
            var bootStrapper = new UnityBootStrapper() { ConfigurationFileName = ConfigurationManager.AppSettings["UnityConfigFile"] };
            bootStrapper.Initialize();
        }
    }
}
