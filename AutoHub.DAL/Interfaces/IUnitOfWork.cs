using System;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoHub.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AutoHub.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Car> Cars { get; }
        IRepository<Lot> Lots { get; }
        IRepository<User> Users { get; }
        
        int Commit();
    }
}