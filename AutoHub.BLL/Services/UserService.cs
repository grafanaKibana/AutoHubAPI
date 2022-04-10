using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Exceptions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoHub.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AutoHub.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthenticationService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly AutoHubContext _context;
        private readonly IMapper _mapper;

        public UserService(AutoHubContext context, IMapper mapper, IAuthenticationService authService,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
            _userManager = userManager;
            _signManager = signManager;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
        {
            var users = _context.Users.ToList();

            var mappedUsers = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            foreach (var dto in mappedUsers)
            {
                dto.UserRoles = await _userManager.GetRolesAsync(users.FirstOrDefault(x => x.Id == dto.UserId));
            }

            return mappedUsers;
        }

        public async Task<UserResponseDTO> GetByIdAsync(int userId)
        {
            var user = _context.Users.Find(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            mappedUser.UserRoles = await _userManager.GetRolesAsync(user);

            return mappedUser;
        }

        public async Task<UserResponseDTO> GetByEmailAsync(string email)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == email) ?? throw new NotFoundException($"User with E-Mail {email} not exist.");
            var mappedUser = _mapper.Map<UserResponseDTO>(user);
            mappedUser.UserRoles = await _userManager.GetRolesAsync(user);

            return mappedUser;
        }

        public async Task<UserLoginResponseDTO> LoginAsync(UserLoginRequestDTO userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Username) ?? throw new NotFoundException($"User with username {userModel.Username} not found.");

            var isPasswordVerified = await _signManager.PasswordSignInAsync(userModel.Username, userModel.Password, userModel.RememberMe, false);

            if (isPasswordVerified.Succeeded.Equals(false))
            {
                throw new LoginFailedException("Wrong password.");
            }

            var mappedUser = new UserLoginResponseDTO
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Token = _authService.GenerateWebTokenForUser(user)
            };

            return mappedUser;
        }

        public async Task RegisterAsync(UserRegisterRequestDTO registerUserDTO)
        {
            var isDuplicate = _context.Users.Any(user => user.Email == registerUserDTO.Email);

            if (isDuplicate.Equals(true))
            {
                throw new RegistrationFailedException($"User with E-Mail ({registerUserDTO.Email}) already exists.");
            }

            var newUser = _mapper.Map<ApplicationUser>(registerUserDTO);

            newUser.RegistrationTime = DateTime.UtcNow;
            newUser.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(newUser, registerUserDTO.Password);

            await _userManager.AddToRoleAsync(newUser, "Customer");

            if (result.Succeeded.Equals(false))
            {
                throw new RegistrationFailedException(string.Concat(result.Errors, " \n"));
            }

            await _signManager.SignInAsync(newUser, isPersistent: false);
        }

        public void Update(int userId, UserUpdateRequestDTO updateUserDTO)
        {
            var user = _context.Users.Find(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.UserName = updateUserDTO.Username;
            user.Email = updateUserDTO.Email;
            user.PhoneNumber = updateUserDTO.PhoneNumber;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void UpdateRole(int userId, int roleId) //TODO: Change to "SetRole"
        {
            if (Enum.IsDefined(typeof(UserRoleEnum), roleId).Equals(false))
            {
                throw new EntityValidationException("Incorrect user role ID");
            }

            var user = _context.UserRoles.Find(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

            user.RoleId = roleId;
            _context.UserRoles.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = _context.Users.Find(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task LogoutAsync() => await _signManager.SignOutAsync();
    }
}