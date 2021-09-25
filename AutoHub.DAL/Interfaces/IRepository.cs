using System.Collections.Generic;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> GetAll();
        T GetById(int id);
        T Add(T newItem);
        bool Update(T item);
        bool Delete(int id);
    }
}