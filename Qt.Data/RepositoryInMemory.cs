using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Qt.Data.Common;
using Qt.Domain;

namespace Qt.Data
{
    public class RepositoryInMemory<TEntity> : IRepository<TEntity> where TEntity : class, IIdentityAware
    {

        private Dictionary<long, TEntity> _entityMap = new Dictionary<long, TEntity>();

        public IQueryable<TEntity> FetchAll()
        {
            return _entityMap.Values.AsQueryable();
        }

        public TEntity GetById(long id)
        {
            TEntity entity;
            _entityMap.TryGetValue(id, out entity);
            return entity;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Save(TEntity instance)
        {
            if (null == instance)
            {
                throw new InvalidDataException(string.Format("Instance is null for type {0}", typeof(TEntity)));
            }
            if (instance.Id == 0)
            {
                InjectIdValue(instance);
            }

            lock (MapLock())
            {
                _entityMap.Add(instance.Id, instance);
            }
            return instance;
        }

        public void Delete(TEntity instance)
        {
            if (instance == null) return;
            lock (MapLock())
            {
                _entityMap.Remove(instance.Id);
            }
        }

        public TEntity Update(TEntity instance)
        {
            if (null == instance || instance.Id == 0)
            {
                throw new InvalidDataException(string.Format("Instance is null for type {0}", typeof(TEntity)));
            }
            lock (MapLock())
            {
                _entityMap[instance.Id] = instance;
                return instance;
            }
        }

        #region Private Methods

        private Object MapLock()
        {
            return ((IDictionary)_entityMap).SyncRoot;
        }

        private readonly IDictionary<Type, MethodInfo> _setters = new Dictionary<Type, MethodInfo>();

        private void InjectIdValue(TEntity instance)
        {
            lock (MapLock())
            {
                long max = 0;
                if (_entityMap.Count > 0) max = _entityMap.Keys.Max();
                max = ++max;
                var setter = GetIdSetter(instance.GetType());
                setter.Invoke(instance, new object[] { max });
            }
        }

        private MethodInfo GetIdSetter(Type type)
        {
            if (_setters.ContainsKey(type)) return _setters[type];
            var setter = type.GetProperty("Id").GetSetMethod(true);
            _setters.Add(type, setter);
            return setter;
        }

        #endregion


    }

}
