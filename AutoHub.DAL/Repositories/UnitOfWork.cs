﻿using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoHubContext _context;
        private IRepository<Bid> _bidRepository;
        private IRepository<CarBrand> _carBrandRepository;
        private IRepository<CarColor> _carColorRepository;
        private IRepository<CarModel> _carModelRepository;

        private IRepository<Car> _carRepository;
        private IRepository<Lot> _lotRepository;
        private IRepository<User> _userRepository;

        public UnitOfWork(AutoHubContext context)
        {
            _context = context;
        }

        public IRepository<Car> Cars => _carRepository ??= new Repository<Car>(_context);
        public IRepository<Lot> Lots => _lotRepository ??= new Repository<Lot>(_context);
        public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
        public IRepository<Bid> Bids => _bidRepository ??= new Repository<Bid>(_context);
        public IRepository<CarBrand> CarBrands => _carBrandRepository ??= new Repository<CarBrand>(_context);
        public IRepository<CarModel> CarModels => _carModelRepository ??= new Repository<CarModel>(_context);
        public IRepository<CarColor> CarColors => _carColorRepository ??= new Repository<CarColor>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}