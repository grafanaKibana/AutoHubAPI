using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AutoHubContext _db;

        public UserRepository(AutoHubContext context)
        {
            _db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.User;
        }

        public User GetById(int id)
        {
            return _db.User.Find(id);
        }

        public User Find(int id)
        {
            return _db.User.Find(id);
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _db.User.Where(predicate);
        }

        public void Add(User newItem)
        {
            _db.User.Add(newItem);
        }

        public void AddRange(IEnumerable<User> newItems)
        {
            _db.User.AddRange(newItems);
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = _db.User.Find(id);
            if (user != null) 
                _db.User.Remove(user);
        }
    }
}