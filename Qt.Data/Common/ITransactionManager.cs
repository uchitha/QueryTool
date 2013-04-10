using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qt.Data
{
    public interface ITransactionManager : IDisposable
    {
        TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command);
        void ExecuteCommandWithoutResult(Action<IRepositoryLocator> command);
        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
