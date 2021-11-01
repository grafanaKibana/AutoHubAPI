using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool Register(User userModel);
        bool Login();
        bool IsEmailUnique(string email);
        bool IsPasswordMatchRules(string password);
        string HashPassword(string password);
    }
}