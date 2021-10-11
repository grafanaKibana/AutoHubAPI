using System;
using AutoHub.DAL.Entities;

namespace AutoHub.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Car> Cars { get; }
        IRepository<Lot> Lots { get; }
        IRepository<User> Users { get; }
        IRepository<Bid> Bids { get; }
        IRepository<CarBrand> CarBrands { get; }
        IRepository<CarModel> CarModels { get; }
        IRepository<CarColor> CarColors { get; }

        int Commit();
    }
}