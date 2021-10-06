using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoHub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.DAL.Repositories
{
    // Generic variant of repository (Currently using non-generic repos)
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(T newItem)
        {
            _entities.Add(newItem);
        }

        public void AddRange(IEnumerable<T> newItems)
        {
            _entities.AddRange(newItems);
        }

        public void Update(T item)
        {
            _entities.Update(item);
        }

        public void Delete(int id)
        {
            var toRemove = _entities.Find(id);

            if (toRemove != null)
                _entities.Remove(toRemove);
        }
    };
}