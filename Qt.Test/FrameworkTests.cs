using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Qt.Core;
using Qt.Data.Common;

namespace Qt.Test
{
    [TestFixture]
    public class FrameworkTests
    {
        [Test]
        public void DependancyInjectionTest()
        {
            var bootStrapper = new UnityBootStrapper() { ConfigurationFileName = ConfigurationManager.AppSettings["UnityConfigFile"] };
            bootStrapper.Initialize();

            var a = ServiceLocator.Current.GetInstance<ITransactionManagerFactory>();
            var b = ServiceLocator.Current.GetInstance<ITransactionManagerFactory>();

            //ITransactionManagerFactory is a singleton
            Assert.AreEqual(a, b);

        }
    }
}
