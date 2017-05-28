using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Entities;

namespace doLittle.Specs.Configuration
{
    public class EntityContext<T> : IEntityContext<T>
    {
        private readonly List<T> _entities;
        readonly List<T> _entitiesToDelete;

        public EntityContext()
        {
            _entities = new List<T>();
            _entitiesToDelete = new List<T>();
        }

        public EntityContext(IEnumerable<T> entities) : this()
        {
           Populate(entities);
        }

        public void Dispose()
        {}

        public IQueryable<T> Entities
        {
            get { return _entities.AsQueryable(); }
        }

        public void Attach(T entity)
        {
            _entities.Add(entity);
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {}

        public void Delete(T entity)
        {
            _entitiesToDelete.Add(entity);
        }

        public void Save(T entity)
        {
            
        }

        public bool CommitCalled = false;
        public void Commit()
        {
            foreach (var entity in _entitiesToDelete)
                _entities.Remove(entity);

            _entitiesToDelete.Clear();
            CommitCalled = true;
        }

        public void Populate(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }


        public T GetById<TProperty>(TProperty id)
        {
            throw new NotImplementedException();
        }


        public void DeleteById<TProperty>(TProperty id)
        {
            throw new NotImplementedException();
        }
    }
}