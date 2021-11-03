using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public IEnumerable<UserResponseDTO> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            var mappedUsers = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return mappedUsers;
        }

        public UserResponseDTO GetById(int userId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            return mappedUser;
        }

        public UserResponseDTO GetByEmail(string email)
        {
            var user = _unitOfWork.Users.Find(user => user.Email == email).FirstOrDefault();
            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            return mappedUser;
        }

        public UserLoginResponseDTO Login(UserLoginRequestDTO userModel)
        {
            var user = _unitOfWork.Users.Find(user1 =>
                user1.Email == userModel.Email).FirstOrDefault();

            if (user == null || !_authService.VerifyPassword(userModel.Password, user.Password)) return null;

            var mappedUser = new UserLoginResponseDTO
            {
                Email = user.Email,
                Token = _authService.GenerateWebTokenForUser(user)
            };
            return mappedUser;
        }

        public void Register(UserRegisterRequestDTO registerUserDTO)
        {
            var user = _mapper.Map<User>(registerUserDTO);

            user.UserRoleId = UserRoleEnum.Regular;
            user.RegistrationTime = DateTime.UtcNow;
            user.Password = _authService.HashPassword(user.Password);

            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();
        }

        public void Update(int userId, UserUpdateRequestDTO updateUserDTO)
        {
            var user = _unitOfWork.Users.GetById(userId);

            user.UserRoleId = (UserRoleEnum)updateUserDTO.UserRoleId;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.Email = updateUserDTO.Email;
            user.Phone = updateUserDTO.Phone;
            user.Password = _authService.HashPassword(updateUserDTO.Password);

            _unitOfWork.Users.Update(user);
            _unitOfWork.Commit();
        }

        public IEnumerable<BidResponseDTO> GetBids(int userId)
        {
            var users = _unitOfWork.Bids.Find(bid => bid.UserId == userId);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(users);
            return mappedBids;
        }
    }
}