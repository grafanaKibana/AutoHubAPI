using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoHub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.DAL.Repositories
{
    // Generic variant of repository (Currently using non-generic repos)
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AutoHubContext _context;

        public Repository(AutoHubContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            /*if (typeof(Lot).IsAssignableFrom(typeof(T)))
            {
                return (IEnumerable<T>)_context.Lots
                    .Include(lot => lot.Car).ThenInclude(car => car.CarBrand)
                    .Include(lot => lot.Car).ThenInclude(car => car.CarModel)
                    .Include(lot => lot.Car).ThenInclude(car => car.CarColor)
                    .Include(lot => lot.Car).ThenInclude(car => car.CarStatus)
                    .Include(lot => lot.Creator).ThenInclude(user => user.UserRole)
                    .Include(lot => lot.Winner).ThenInclude(user => user.UserRole)
                    .Include(lot => lot.LotStatus)
                    .ToList();
            }*/
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public T Add(T newItem)
        {
            _context.Add(newItem);
            return newItem;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> newItems)
        {
            _context.AddRange(newItems);
            return newItems;
        }

        public bool Update(int id, T item)
        {
            var toUpdate = _context.Set<T>().Find(id);
            if (toUpdate == null)
            {
                return false;
            }

            _context.Entry(item).State = EntityState.Modified;
            return true;
        }

        public bool Delete(int id)
        {
            var toRemove = _context.Set<T>().Find(id);
            if (toRemove == null) return false;

            _context.Remove(toRemove);
            return true;
        }
    };
}