using AutoHub.BusinessLogic.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAll();

    Task<UserResponseDTO> GetById(int userId);

    Task<UserResponseDTO> GetByEmailAsync(string email);

    Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userModel);

    Task Register(UserRegisterRequestDTO registerUserDTO);

    Task Update(int userId, UserUpdateRequestDTO updateUserDTO);

    Task UpdateRole(int userId, int roleId);

    Task Delete(int userId);

    Task Logout();
}
