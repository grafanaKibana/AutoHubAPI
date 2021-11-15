using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoHub.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        bool Any(Expression<Func<T, bool>> predicate);
        bool Contains(T item);
        T Add(T newItem);
        IEnumerable<T> AddRange(IEnumerable<T> newItems);
        T Update(T item);
        bool Delete(int id);
    }
}