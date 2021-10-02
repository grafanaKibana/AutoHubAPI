using System.Collections.Generic;
using AutoHub.BLL.Models;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        bool Register();
        bool Login();
        bool IsEmailUnique(string email);
        bool IsPasswordMatchRules(string password);
    }
}