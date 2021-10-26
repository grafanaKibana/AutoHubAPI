using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        IEnumerable<Bid> GetBids(int userId);
        User GetById(int id);
        bool Register(User userModel);
        bool Login();
        User UpdateUser(User userModel);
        bool IsEmailUnique(string email);
        bool IsPasswordMatchRules(string password);
        string HashPassword(string password);
    }
}