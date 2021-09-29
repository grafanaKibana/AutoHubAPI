using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoHub.DAL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T newItem);
        void AddRange(IEnumerable<T> newItems);
        void Update(T item);
        void Delete(int id);
    }
}