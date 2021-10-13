using System.Collections.Generic;
using AutoHub.BLL.Models.UserModels;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserResponseModel> GetAll();
        UserResponseModel GetById(int id);
        bool Register(UserCreateRequestModel userModel);
        bool Login();
        bool IsEmailUnique(string email);
        bool IsPasswordMatchRules(string password);
        string HashPassword(string password);
    }
}