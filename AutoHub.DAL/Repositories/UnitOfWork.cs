using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoHubContext _context;

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