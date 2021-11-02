using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoHub.DAL.Entities;
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
            //TODO: This shouldn`t exist! (Needed to be done via AutoInclude)
            if (typeof(Lot).IsAssignableFrom(typeof(T)))
                return (IEnumerable<T>)_context.Lots
                    .Include(lot => lot.Car).ThenInclude(car => car.CarBrand)
                    .Include(lot => lot.Car).ThenInclude(car => car.CarModel)
                    .Include(lot => lot.Car).ThenInclude(car => car.CarColor)
                    .Include(lot => lot.Car).ThenInclude(car => car.CarStatus)
                    .Include(lot => lot.Creator).ThenInclude(user => user.UserRole)
                    .Include(lot => lot.Winner).ThenInclude(user => user.UserRole)
                    .Include(lot => lot.LotStatus)
                    .ToList();
            return _context.Set<T>();
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

        public bool Contains(T item)
        {
            return _context.Set<T>().Contains(item);
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

        public T Update(T item)
        {
            return _context.Set<T>().Update(item).Entity;
        }

        public bool Delete(int id)
        {
            var toRemove = _context.Set<T>().Find(id);
            if (toRemove == null) return false;

            _context.Remove(toRemove);
            return true;
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var dbSet = _context.Set<T>();

            IQueryable<T> query = null;

            foreach (var include in includes) query = dbSet.Include(include);
            return query ?? dbSet;
        }
    }

    /*public static class IncludeExtension
    {
        public static IQueryable<T> Include<T>(this DbSet<T> dbSet,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;
        }
    }*/
}