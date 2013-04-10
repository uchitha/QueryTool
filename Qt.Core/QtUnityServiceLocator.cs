using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Qt.Core
{
    class QtUnityServiceLocator : ServiceLocatorImplBase
    {
        public IUnityContainer UnityContainer { get; private set; }
        private readonly UnityServiceLocator _serviceLocator;

        public QtUnityServiceLocator(IUnityContainer unityContainer)
        {
            _serviceLocator = new UnityServiceLocator(unityContainer);
            UnityContainer = unityContainer;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _serviceLocator.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _serviceLocator.GetAllInstances(serviceType);
        }
    }
}
