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
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T Add(T newItem)
        {
            _context.Add(newItem);
            _context.SaveChanges();
            return newItem;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> newItems)
        {
            _context.AddRange(newItems);
            _context.SaveChanges();
            return newItems;
        }

        public bool Update(int id, T item)
        {
            if (_context.Set<T>().Find(id) == null)
            {
                return false;
            }
            _context.Update(item);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var toRemove = _context.Set<T>().Find(id);

            if (toRemove == null)
                return false;
            _context.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    };
}