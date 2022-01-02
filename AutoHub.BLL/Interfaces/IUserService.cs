using AutoHub.BLL.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAll();
        Task<UserResponseDTO> GetById(int userId);
        Task<UserResponseDTO> GetByEmail(string email);
        Task<UserLoginResponseDTO> LoginAsync(UserLoginRequestDTO userModel);
        Task RegisterAsync(UserRegisterRequestDTO registerUserDTO);
        void Update(int userId, UserUpdateRequestDTO updateUserDTO);
        void UpdateRole(int userId, int roleId);
        void Delete(int userId);
    }
}