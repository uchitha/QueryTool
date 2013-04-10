using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Qt.Data;
using Qt.Data.Common;

namespace Qt.Web.ControllerHelpers
{
    public class QtBaseControllerHelper
    {
        private ITransactionManagerFactory _factory;

        private ITransactionManagerFactory GetFactory()
        {
            if (_factory != null) return _factory;
            
            _factory = ServiceLocator.Current.GetInstance<ITransactionManagerFactory>();
            return _factory;
        }

        protected TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
        {
            using (var transManager = GetFactory().CreateManager())
            {
                return transManager.ExecuteCommand(command.Invoke);
            }
        }

        protected void ExecuteCommandWithoutResult(Action<IRepositoryLocator> command)
        {
            var func = new Func<IRepositoryLocator, bool>(locator =>
            {
                command.Invoke(locator);
                return true;
            });
            ExecuteCommand(func);
        }

    }
}
