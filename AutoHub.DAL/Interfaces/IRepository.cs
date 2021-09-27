using System.Collections.Generic;

namespace AutoHub.DAL.Interfaces
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