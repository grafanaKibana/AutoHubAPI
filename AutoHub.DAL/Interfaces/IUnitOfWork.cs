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
        IRepository<CarStatus> CarStatuses { get; }
        IRepository<LotStatus> LotStatuses { get; }
        IRepository<UserRole> UserRoles { get; }

        int Commit();
    }
}