using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.UserModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(_unitOfWork.Users.GetAll());
        }

        public UserViewModel GetById(int id)
        {
            return _mapper.Map<UserViewModel>(_unitOfWork.Users.GetById(id));
        }


        public bool Register(UserCreateApiModel userModel)
        {
            if (IsPasswordMatchRules(userModel.Password) && IsEmailUnique(userModel.Email))
            {
                _unitOfWork.Users.Add(new User
                {
                    UserId = 0,
                    UserRoleId = UserRoleId.Regular,
                    FirstName = userModel.FirstName,
                    LastName = userModel.FirstName,
                    Email = userModel.Email,
                    Phone = userModel.Phone,
                    Password = HashPassword(userModel.Password),
                    RegistrationTime = DateTime.UtcNow
                });
                _unitOfWork.Commit();
                return true;
            }

            return false;
        }

        public bool Login()
        {
            throw new NotImplementedException();
        }

        public bool IsEmailUnique(string email)
        {
            return _unitOfWork.Users.GetAll().All(user => user.Email != email);
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