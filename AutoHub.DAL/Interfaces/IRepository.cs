using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoHub.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        bool Contains(T item);
        T Add(T newItem);
        IEnumerable<T> AddRange(IEnumerable<T> newItems);
        T Update(T item);
        bool Delete(int id);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}