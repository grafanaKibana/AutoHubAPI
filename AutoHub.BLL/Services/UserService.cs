using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.GetAll();
        }

        public IEnumerable<Bid> GetBids(int userId)
        {
            //TODO: Set-up Including of user, and lot and its members
            return _unitOfWork.Bids.Find(bid => bid.UserId == userId);
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public bool Register(User userModel)
        {
            if (!IsPasswordMatchRules(userModel.Password) || !IsEmailUnique(userModel.Email))
                return false;
            _unitOfWork.Users.Add(userModel);
            _unitOfWork.Commit();
            return true;
        }

        public User UpdateUser(User userModel)
        {
            var user = _unitOfWork.Users.GetById(userModel.UserId);
            user.UserRoleId = userModel.UserRoleId;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Phone = userModel.Phone;
            user.Password = HashPassword(userModel.Password);

            _unitOfWork.Users.Update(userModel);
            _unitOfWork.Commit();
            return userModel;
        }

        public bool Login()
        {
            throw new NotImplementedException();
        }

        public bool IsEmailUnique(string email)
        {
            return _unitOfWork.Users.Any(user => user.Email != email);
        }

        public bool IsPasswordMatchRules(string password)
        {
            var rgx = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return rgx.IsMatch(password);
        }

        public string HashPassword(string password)
        {
            return Convert
                .ToBase64String(HashAlgorithm.Create("sha256")
                    .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}