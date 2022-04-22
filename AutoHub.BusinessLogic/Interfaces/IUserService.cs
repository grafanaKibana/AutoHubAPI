using AutoHub.BusinessLogic.DTOs.UserDTOs;
using AutoHub.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAll(PaginationParameters paginationParameters);

    Task<UserResponseDTO> GetById(int userId);

    Task<UserResponseDTO> GetByEmail(string email);

    Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userModel);

    Task Register(UserRegisterRequestDTO registerUserDTO);

    Task Update(int userId, UserUpdateRequestDTO updateUserDTO);

    Task AddToRole(int userId, int roleId);

    Task RemoveFromRole(int userId, int roleId);

    Task Delete(int userId);

    Task Logout();
}