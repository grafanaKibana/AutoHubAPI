using System;
using AutoHub.DAL.Entities;

namespace AutoHub.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Car> Cars { get; }
        IRepository<Lot> Lots { get; }
        IRepository<User> Users { get; }

        int Commit();
    }
}