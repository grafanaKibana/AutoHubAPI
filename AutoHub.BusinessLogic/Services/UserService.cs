using AutoHub.BusinessLogic.DTOs.UserDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.DataAccess;
using AutoHub.Domain.Constants;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Entities.Identity;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IAuthenticationService _authService;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signManager;
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;

    public UserService(AutoHubContext context, IMapper mapper, IAuthenticationService authService, IEmailService emailService,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
    {
        _context = context;
        _mapper = mapper;
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _userManager = userManager;
        _signManager = signManager;
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = _context.Users.OrderBy(x => x.Id).AsQueryable();
        List<ApplicationUser> users;

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            users = await query.Where(x => x.Id > after).Take(limit).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            users = await query.Where(x => x.Id < before).Take(limit).ToListAsync();
        }
        else
        {
            users = await query.Take(limit).ToListAsync();
        }

        var mappedUsers = _mapper.Map<IEnumerable<UserResponseDTO>>(users).ToList();

        foreach (var dto in mappedUsers)
        {
            dto.UserRoles = await _userManager.GetRolesAsync(users.Single(x => x.Id == dto.UserId));
        }

        return mappedUsers;
    }

    public async Task<UserResponseDTO> GetById(int userId)
    {
        var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        var mappedUser = _mapper.Map<UserResponseDTO>(user);
        mappedUser.UserRoles = await _userManager.GetRolesAsync(user);

        return mappedUser;
    }

    public async Task<UserResponseDTO> GetByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new NotFoundException($"User with E-Mail {email} not exist.");

        var mappedUser = _mapper.Map<UserResponseDTO>(user);
        mappedUser.UserRoles = await _userManager.GetRolesAsync(user);

        return mappedUser;
    }

    public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userModel)
    {
        var user = await _userManager.FindByNameAsync(userModel.Username) ?? throw new NotFoundException($"User with username {userModel.Username} not found.");

        if (user.EmailConfirmed.Equals(false))
        {
            throw new LoginFailedException("Please confirm registration via link in your email.");
        }

        var signInResult = await _signManager.PasswordSignInAsync(userModel.Username, userModel.Password, userModel.RememberMe, false);

        if (signInResult.Succeeded.Equals(false))
        {
            throw new LoginFailedException("Wrong password.");
        }

        var mappedUser = new UserLoginResponseDTO
        {
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email,
            Token = await _authService.GenerateWebTokenForUser(user)
        };

        return mappedUser;
    }

    public async Task Register(UserRegisterRequestDTO registerUserDTO)
    {
        _ = await _userManager.FindByEmailAsync(registerUserDTO.Email) ?? throw new RegistrationFailedException($"User with E-Mail ({registerUserDTO.Email}) already exists.");
        _ = await _userManager.FindByNameAsync(registerUserDTO.Username) ?? throw new RegistrationFailedException($"User with username ({registerUserDTO.Username}) already exists.");

        var newUser = _mapper.Map<ApplicationUser>(registerUserDTO);

        newUser.RegistrationTime = DateTime.UtcNow;
        newUser.SecurityStamp = Guid.NewGuid().ToString();

        try
        {
            var result = await _userManager.CreateAsync(newUser, registerUserDTO.Password);

            if (result.Succeeded.Equals(true))
            {
                var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                confirmationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationCode));

                await _userManager.ConfirmEmailAsync(newUser, WebEncoders.Base64UrlDecode(confirmationCode).ToString());

                await _emailService.SendEmail(new SendMailRequest
                {
                    ToEmail = registerUserDTO.Email,
                    Subject = "Confirm your account.",
                    Body = $"<div>Hi, {newUser.FullName}!</div>" +
                    $"Confirm registration by clicking the folowing link: " +
                    $"<a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">{confirmationCode}</a>."
                });

                await _userManager.AddToRoleAsync(newUser, AuthorizationRoles.Customer);
                await _signManager.SignInAsync(newUser, isPersistent: false);
            }
            else
            {
                throw new RegistrationFailedException(string.Join('\n', result.Errors.Select(e => e.Description)));
            }
        }
        catch (Exception)
        {
            await _userManager.DeleteAsync(newUser);
            throw;
        }
    }

    public async Task Update(int userId, UserUpdateRequestDTO updateUserDTO)
    {
        var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        user.FirstName = updateUserDTO.FirstName;
        user.LastName = updateUserDTO.LastName;
        user.UserName = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        user.PhoneNumber = updateUserDTO.PhoneNumber;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task AddToRole(int userId, int roleId)
    {
        if (Enum.IsDefined(typeof(UserRoleEnum), roleId).Equals(false))
        {
            throw new EntityValidationException("Incorrect user role ID");
        }

        var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles.Contains(Enum.GetName(typeof(UserRoleEnum), roleId)))
        {
            throw new DuplicateException($"User already have {(UserRoleEnum)roleId} role.");
        }

        await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRoleEnum), roleId));
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromRole(int userId, int roleId)
    {
        if (Enum.IsDefined(typeof(UserRoleEnum), roleId).Equals(false))
        {
            throw new EntityValidationException("Incorrect user role ID");
        }

        var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles.Contains(Enum.GetName(typeof(UserRoleEnum), roleId)).Equals(false))
        {
            throw new NotFoundException($"User don`t have {(UserRoleEnum)roleId} role.");
        }

        await _userManager.RemoveFromRoleAsync(user, Enum.GetName(typeof(UserRoleEnum), roleId));
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int userId)
    {
        var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task Logout() => await _signManager.SignOutAsync();
}