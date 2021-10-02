using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.DAL.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private readonly AutoHubContext _db;

        public CarRepository(AutoHubContext context)
        {
            _db = context;
        }

        public IEnumerable<Car> GetAll()
        {
            return _db.Car;
        }

        public Car GetById(int id)
        {
            return _db.Car.Find(id);
        }
        
        public IEnumerable<Car> Find(Expression<Func<Car, bool>> predicate)
        {
            return _db.Car.Where(predicate);
        }

        public void Add(Car newItem)
        {
            _db.Car.Add(newItem);
        }

        public void AddRange(IEnumerable<Car> newItems)
        {
            _db.Car.AddRange(newItems);
        }

        public void Update(Car item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var car = _db.Car.Find(id);
            if (car != null) 
                _db.Car.Remove(car);
        }
    }
}