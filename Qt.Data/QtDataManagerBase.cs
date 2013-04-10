using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Data.Common;
using Microsoft.Practices.ServiceLocation;

namespace Qt.Data
{
    public abstract class QtDataManagerBase
    {
        private static ITransactionManagerFactory _factory;

        private static ITransactionManagerFactory GetFactory()
        {
            if (_factory != null) return _factory;

            _factory = ServiceLocator.Current.GetInstance<ITransactionManagerFactory>();
            return _factory;
        }

        protected static TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
        {
            using (var transManager = GetFactory().CreateManager())
            {
                return transManager.ExecuteCommand(command.Invoke);
            }
        }

        protected static void ExecuteCommandWithoutResult(Action<IRepositoryLocator> command)
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
