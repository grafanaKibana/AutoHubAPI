using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoHub.DAL.Repositories;

namespace AutoHub.BLL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoHubContext _context;

        private CarRepository _carRepository;
        private LotRepository _lotRepository;
        private UserRepository _userRepository;
        
        public UnitOfWork(AutoHubContext context)
        {
            _context = context;
        }

        public IRepository<Car> Cars => _carRepository ??= new CarRepository(_context);
        public IRepository<Lot> Lots => _lotRepository ??= new LotRepository(_context);
        public IRepository<User> Users => _userRepository ??= new UserRepository(_context);
        
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