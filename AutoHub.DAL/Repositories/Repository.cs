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
        private readonly AutoHubContext _context;

        public Repository(AutoHubContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public bool Contains(T item)
        {
            return _context.Set<T>().Contains(item);
        }

        public T Add(T newItem)
        {
            _context.Add(newItem);
            return newItem;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> newItems)
        {
            _context.AddRange(newItems);
            return newItems;
        }

        public T Update(T item)
        {
            return _context.Set<T>().Update(item).Entity;
        }

        public bool Delete(int id)
        {
            var toRemove = _context.Set<T>().Find(id);
            if (toRemove == null)
                return false;

            _context.Remove(toRemove);
            return true;
        }

        public IEnumerable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking();
            return includes.Aggregate(query, (current, include) => current.Include(include)).ToList();
        }

        public IEnumerable<T> Include(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Include(includes);
            return query.Where(predicate);
        }
    }
}