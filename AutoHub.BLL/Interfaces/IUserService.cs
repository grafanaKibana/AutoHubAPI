using AutoHub.BusinessLogic.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDTO>> GetAllAsync();

    Task<UserResponseDTO> GetByIdAsync(int userId);

    Task<UserResponseDTO> GetByEmailAsync(string email);

    Task<UserLoginResponseDTO> LoginAsync(UserLoginRequestDTO userModel);

    Task RegisterAsync(UserRegisterRequestDTO registerUserDTO);

    void Update(int userId, UserUpdateRequestDTO updateUserDTO);

    void UpdateRole(int userId, int roleId);

    void Delete(int userId);

    Task LogoutAsync();
}
