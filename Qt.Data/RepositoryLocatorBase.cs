using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Qt.Data.Common;
using Qt.Domain;

namespace Qt.Data
{
    public abstract class RepositoryLocatorBase : IRepositoryLocator
    {
        protected IDictionary<Type, object> RepositoryMap = new Dictionary<Type, object>();

        /// <summary>
        /// To be extended by each repository locator implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected abstract IRepository<T> CreateRepository<T>() where T : class, IIdentityAware;


        public IQueryable<TEntity> FetchAll<TEntity>() where TEntity : class, IIdentityAware
        {
            return GetRepository<TEntity>().FetchAll();
        }

        public TEntity GetById<TEntity>(long id) where TEntity : class, IIdentityAware
        {
            return GetRepository<TEntity>().GetById(id);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IIdentityAware
        {
            return GetRepository<TEntity>().Find(predicate);
        }

        public TEntity Save<TEntity>(TEntity instance) where TEntity : class, IIdentityAware
        {
            return GetRepository<TEntity>().Save(instance);
        }

        public void Delete<TEntity>(TEntity instance) where TEntity : class, IIdentityAware
        {
            GetRepository<TEntity>().Delete(instance);
        }

        public TEntity Update<TEntity>(TEntity instance) where TEntity : class, IIdentityAware
        {
            return GetRepository<TEntity>().Update(instance);
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IIdentityAware
        {
            var type = typeof(TEntity);
            if (RepositoryMap.ContainsKey(type)) return RepositoryMap[type] as IRepository<TEntity>;
            return CreateRepository<TEntity>();
        }

    }
}
