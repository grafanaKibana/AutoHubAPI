using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoHub.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Add(T newItem);
        IEnumerable<T> AddRange(IEnumerable<T> newItems);
        bool Update(int id, T item);
        bool Delete(int id);
    }
}