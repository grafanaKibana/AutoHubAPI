using System.Collections.Generic;
using AutoHub.BLL.Models.UserModels;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAll();
        UserViewModel GetById(int id);
        bool Register(UserCreateApiModel userModel);
        bool Login();
        bool IsEmailUnique(string email);
        bool IsPasswordMatchRules(string password);
        string HashPassword(string password);
    }
}