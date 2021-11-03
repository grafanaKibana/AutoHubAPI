using System.Collections.Generic;
using AutoHub.BLL.DTOs.UserDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserResponseDTO> GetAll();
        UserResponseDTO GetById(int userId);
        UserResponseDTO GetByEmail(string email);
        UserLoginResponseDTO Login(UserLoginRequestDTO userModel);
        void Register(UserRegisterRequestDTO registerUserDTO);
        void Update(int userId, UserUpdateRequestDTO updateUserDTO);
        void Delete(int userId);
    }
}