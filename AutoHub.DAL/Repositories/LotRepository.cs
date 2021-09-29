using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.DAL.Repositories
{
    public class LotRepository : IRepository<Lot>
    {
        private readonly AutoHubContext _db;

        public LotRepository(AutoHubContext context)
        {
            _db = context;
        }

        public IEnumerable<Lot> GetAll()
        {
            return _db.Lot;
        }

        public Lot GetById(int id)
        {
            return _db.Lot.Find(id);
        }

        public IEnumerable<Lot> Find(Expression<Func<Lot, bool>> predicate)
        {
            return _db.Lot.Where(predicate);
        }

        public void Add(Lot newItem)
        {
            _db.Lot.Add(newItem);
        }

        public void AddRange(IEnumerable<Lot> newItems)
        {
            _db.Lot.AddRange(newItems);
        }

        public void Update(Lot item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var lot = _db.Lot.Find(id);
            if (lot != null)
            {
                _db.Lot.Remove(lot);
            }
        }
    }
}