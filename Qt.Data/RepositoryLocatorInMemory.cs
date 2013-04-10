using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Data.Common;

namespace Qt.Data
{
    public class RepositoryLocatorInMemory : RepositoryLocatorBase, IResetable
    {
        private static readonly Object Lock = new object();

        protected override IRepository<T> CreateRepository<T>()
        {
            lock (Lock)
            {
                var type = typeof(T);
                if (RepositoryMap.ContainsKey(type)) return RepositoryMap[type] as IRepository<T>;
                var repository = new RepositoryInMemory<T>();
                RepositoryMap.Add(type, repository);
                return repository;
            }
        }

        public void Reset()
        {
            RepositoryMap = new Dictionary<Type, object>();
        }
    }
}
