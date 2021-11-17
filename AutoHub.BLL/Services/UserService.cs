using System;
using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Exceptions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly AutoHubContext _context;
        private readonly IMapper _mapper;

        public UserService(AutoHubContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        public IEnumerable<UserResponseDTO> GetAll()
        {
            var users = _context.Users
                .Include(user => user.UserRole)
                .ToList();

            var mappedUsers = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return mappedUsers;
        }

        public UserResponseDTO GetById(int userId)
        {
            var user = _context.Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.UserId == userId);

            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            return mappedUser;
        }

        public UserResponseDTO GetByEmail(string email)
        {
            var user = _context.Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.Email == email);

            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            return mappedUser;
        }

        public UserLoginResponseDTO Login(UserLoginRequestDTO userModel)
        {
            var user = _context.Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.Email == userModel.Email);

            if (user == null)
                throw new NotFoundException($"User with Email {userModel.Email} not found");

            var isPasswordVerified = _authService.VerifyPassword(userModel.Password, user.Password);

            if (!isPasswordVerified)
                throw new LoginFailedException("Wrong password");

            var mappedUser = new UserLoginResponseDTO
            {
                Email = user.Email,
                Token = _authService.GenerateWebTokenForUser(user)
            };
            return mappedUser;
        }

        public void Register(UserRegisterRequestDTO registerUserDTO)
        {
            var user = _context.Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.Email == registerUserDTO.Email);

            if (user != null)
                throw new RegistrationFailedException($"User with Email {registerUserDTO.Email} already exist");

            var newUser = _mapper.Map<User>(registerUserDTO);

            newUser.UserRoleId = UserRoleEnum.Regular;
            newUser.RegistrationTime = DateTime.UtcNow;
            newUser.Password = _authService.HashPassword(newUser.Password);

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void Update(int userId, UserUpdateRequestDTO updateUserDTO)
        {
            if (!Enum.IsDefined(typeof(UserRoleEnum), updateUserDTO.UserRoleId))
                throw new EntityValidationException("Incorrect user role ID");

            var user = _context.Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.UserId == userId);

            if (user == null)
                throw new NotFoundException($"User with ID {userId} not exist");

            user.UserRoleId = (UserRoleEnum)updateUserDTO.UserRoleId;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.Email = updateUserDTO.Email;
            user.Phone = updateUserDTO.Phone;
            user.Password = _authService.HashPassword(updateUserDTO.Password);

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null) throw new NotFoundException($"User with ID {userId} not exist");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}