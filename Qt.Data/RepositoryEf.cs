using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Qt.Data.Common;
using Qt.Domain;

namespace Qt.Data
{
    public class RepositoryEf<TEntity> : IRepository<TEntity> where TEntity : class,IIdentityAware
    {
        private DbContext _context;
        private static readonly Object Lock = new object();

        /// <summary>
        /// Will be provided by the TransManager
        /// </summary>
        /// <param name="context"></param>
        public RepositoryEf(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FetchAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Save(TEntity instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity instance)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity instance)
        {
            throw new NotImplementedException();
        }
    }
}
