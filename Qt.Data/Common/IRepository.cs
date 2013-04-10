using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Qt.Domain;

namespace Qt.Data.Common
{
    public interface IRepository<TEntity> where TEntity : class, IIdentityAware
    {
        IQueryable<TEntity> FetchAll();
        TEntity GetById(long id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Save(TEntity instance);
        void Delete(TEntity instance);
        TEntity Update(TEntity instance);
    }

}
