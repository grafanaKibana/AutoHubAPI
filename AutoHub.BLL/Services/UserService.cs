using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<UserResponseDTO> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            var mappedUsers = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return mappedUsers;
        }

        public IEnumerable<BidResponseDTO> GetBids(int userId)
        {
            var users = _unitOfWork.Bids.Find(bid => bid.UserId == userId);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(users);
            return mappedBids;
        }

        public UserResponseDTO GetById(int userId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            return mappedUser;
        }

        public void Register(UserRegisterRequestDTO registerUserDTO)
        {
            var user = _mapper.Map<User>(registerUserDTO);

            user.UserRoleId = UserRoleEnum.Regular;
            user.RegistrationTime = DateTime.UtcNow;

            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();
        }

        public bool Login(UserLoginRequestDTO userModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserUpdateRequestDTO updateUserDTO)
        {
            var user = _unitOfWork.Users.GetById(updateUserDTO.UserId);

            user.UserRoleId = (UserRoleEnum)updateUserDTO.UserRoleId;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.Email = updateUserDTO.Email;
            user.Phone = updateUserDTO.Phone;
            user.Password = HashPassword(updateUserDTO.Password);

            _unitOfWork.Users.Update(user);
            _unitOfWork.Commit();
        }

        private bool IsEmailUnique(string email)
        {
            return _unitOfWork.Users.Any(user => user.Email != email);
        }

        // private bool IsPasswordMatchRules(string password) => new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").IsMatch(password);

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(HashAlgorithm.Create("sha256")
                .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}