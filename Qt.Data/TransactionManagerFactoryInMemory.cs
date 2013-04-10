using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Data.Common;

namespace Qt.Data
{
    class TransactionManagerFactoryInMemory : ITransactionManagerFactory
    {
        private TransactionManagerInMemory TransactionManager;

        public ITransactionManager CreateManager()
        {
            if (TransactionManager != null) return TransactionManager;
            TransactionManager = new TransactionManagerInMemory();
            return TransactionManager;
        }
    }
}
