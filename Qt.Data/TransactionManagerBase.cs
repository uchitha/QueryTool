using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qt.Data
{
    public abstract class TransactionManagerBase : ITransactionManager
    {
        protected bool IsInTranx;

        protected IRepositoryLocator RepositoryLocator { get; set; }

        public TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
        {
            try
            {
                BeginTransaction();
                var result = command.Invoke(RepositoryLocator);
                CommitTransaction();
                return result;
            }
            catch (Exception)
            {
                if (IsInTranx) Rollback();
                throw;
            }
        }

        public void ExecuteCommandWithoutResult(Action<IRepositoryLocator> command)
        {
            try
            {
                BeginTransaction();
                command.Invoke(RepositoryLocator);
                CommitTransaction();
            }
            catch (Exception)
            {
                if (IsInTranx) Rollback();
                throw;
            }
        }

        public void BeginTransaction()
        {
            IsInTranx = true;
            return;
        }

        public void CommitTransaction()
        {
            IsInTranx = false;
            return;
        }

        public void Rollback()
        {
            IsInTranx = false;
            return;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected bool IsDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            RepositoryLocator = null;
            IsDisposed = true;
        }
    }
}
