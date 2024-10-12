using AutoHub.BusinessLogic.DTOs.UserDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.DataAccess;
using AutoHub.Domain.Constants;
using AutoHub.Domain.Entities.Identity;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Services;

public class UserService(
    AutoHubContext context,
    IMapper mapper,
    IAuthenticationService authService,
    IEmailService emailService,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signManager)
    : IUserService
{
    private readonly IAuthenticationService _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    private readonly IEmailService _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

    public async Task<IEnumerable<UserResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.Users.OrderBy(x => x.Id).AsQueryable();
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

        var mappedUsers = mapper.Map<IEnumerable<UserResponseDTO>>(users).ToList();

        foreach (var dto in mappedUsers)
        {
            dto.UserRoles = await userManager.GetRolesAsync(users.Single(x => x.Id == dto.UserId));
        }

        return mappedUsers;
    }

    public async Task<UserResponseDTO> GetById(int userId)
    {
        var user = await context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        var mappedUser = mapper.Map<UserResponseDTO>(user);
        mappedUser.UserRoles = await userManager.GetRolesAsync(user);

        return mappedUser;
    }

    public async Task<UserResponseDTO> GetByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email) ?? throw new NotFoundException($"User with E-Mail {email} not exist.");

        var mappedUser = mapper.Map<UserResponseDTO>(user);
        mappedUser.UserRoles = await userManager.GetRolesAsync(user);

        return mappedUser;
    }

    public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userModel)
    {
        var user = await userManager.FindByNameAsync(userModel.Username) ?? throw new NotFoundException($"User with username {userModel.Username} not found.");

        if (user.EmailConfirmed.Equals(false))
        {
            throw new LoginFailedException("Please confirm registration via link in your email.");
        }
        
        var signInResult = await signManager.PasswordSignInAsync(userModel.Username, userModel.Password, userModel.RememberMe, false);

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
        if (await userManager.FindByEmailAsync(registerUserDTO.Email) is not null)
        {
            throw new RegistrationFailedException($"User with E-Mail ({registerUserDTO.Email}) already exists.");
        }

        if (await userManager.FindByNameAsync(registerUserDTO.Username) is not null)
        {
            throw new RegistrationFailedException($"User with username ({registerUserDTO.Username}) already exists.");
        }

        var newUser = mapper.Map<ApplicationUser>(registerUserDTO);

        newUser.RegistrationTime = DateTime.UtcNow;
        newUser.SecurityStamp = Guid.NewGuid().ToString();

        try
        {
            var result = await userManager.CreateAsync(newUser, registerUserDTO.Password);

            if (result.Succeeded.Equals(true))
            {
                var confirmationCode = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                confirmationCode = Base64Helper.Encode(confirmationCode);

                await userManager.ConfirmEmailAsync(newUser, Base64Helper.Decode(confirmationCode));

                await _emailService.SendEmail(new SendMailRequest
                {
                    ToEmail = registerUserDTO.Email,
                    Subject = "Confirm your account.",
                    Body = $"<div>Hi, {newUser.FullName}!</div>" +
                    $"Confirm registration by clicking the folowing link: " +
                    $"<a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">{confirmationCode}</a>."
                });

                await userManager.AddToRoleAsync(newUser, AuthorizationRoles.Customer);
                await signManager.SignInAsync(newUser, isPersistent: false);
            }
            else
            {
                throw new RegistrationFailedException(string.Join('\n', result.Errors.Select(e => e.Description)));
            }
        }
        catch (Exception)
        {
            await userManager.DeleteAsync(newUser);
            throw;
        }
    }

    public async Task Update(int userId, UserUpdateRequestDTO updateUserDTO)
    {
        var user = await context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        user.FirstName = updateUserDTO.FirstName;
        user.LastName = updateUserDTO.LastName;
        user.UserName = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        user.PhoneNumber = updateUserDTO.PhoneNumber;

        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task AddToRole(int userId, int roleId)
    {
        if (Enum.IsDefined(typeof(UserRoleEnum), roleId).Equals(false))
        {
            throw new EntityValidationException("Incorrect user role ID");
        }

        var user = await context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        var userRoles = await userManager.GetRolesAsync(user);

        if (userRoles.Contains(Enum.GetName(typeof(UserRoleEnum), roleId)))
        {
            throw new DuplicateException($"User already have {(UserRoleEnum)roleId} role.");
        }

        await userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRoleEnum), roleId));
        await context.SaveChangesAsync();
    }

    public async Task RemoveFromRole(int userId, int roleId)
    {
        if (Enum.IsDefined(typeof(UserRoleEnum), roleId).Equals(false))
        {
            throw new EntityValidationException("Incorrect user role ID");
        }

        var user = await context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        var userRoles = await userManager.GetRolesAsync(user);

        if (userRoles.Contains(Enum.GetName(typeof(UserRoleEnum), roleId)).Equals(false))
        {
            throw new NotFoundException($"User don`t have {(UserRoleEnum)roleId} role.");
        }

        await userManager.RemoveFromRoleAsync(user, Enum.GetName(typeof(UserRoleEnum), roleId));
        await context.SaveChangesAsync();
    }

    public async Task Delete(int userId)
    {
        var user = await context.Users.FindAsync(userId) ?? throw new NotFoundException($"User with ID {userId} not exist.");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task Logout() => await signManager.SignOutAsync();
}