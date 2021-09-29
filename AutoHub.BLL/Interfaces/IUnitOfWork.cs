using System;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Car> Cars { get; }
        public IRepository<Lot> Lots { get; }
        public IRepository<User> Users { get; }

        int Commit();
    }
}