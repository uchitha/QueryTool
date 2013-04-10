using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qt.Data
{
    public class TransactionManagerInMemory : TransactionManagerBase
    {
        internal TransactionManagerInMemory()
        {
            RepositoryLocator = new RepositoryLocatorInMemory();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            IsDisposed = true;
        }

    }
}
