using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Qt.Data.Common;
using Qt.Domain;

namespace Qt.Data
{
    public interface IRepositoryLocator
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IIdentityAware;

        IQueryable<TEntity> FetchAll<TEntity>() where TEntity : class, IIdentityAware;
        TEntity GetById<TEntity>(long id) where TEntity : class, IIdentityAware;
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IIdentityAware;

        TEntity Save<TEntity>(TEntity instance) where TEntity : class, IIdentityAware;
        void Delete<TEntity>(TEntity instance) where TEntity : class, IIdentityAware;
        TEntity Update<TEntity>(TEntity instance) where TEntity : class, IIdentityAware;
    }
}
