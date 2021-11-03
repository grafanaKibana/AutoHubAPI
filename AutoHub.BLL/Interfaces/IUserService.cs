using System.Collections.Generic;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.DTOs.UserDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserResponseDTO> GetAll();
        IEnumerable<BidResponseDTO> GetBids(int userId);
        UserResponseDTO GetById(int userId);
        UserResponseDTO GetByEmail(string email);
        UserLoginResponseDTO Login(UserLoginRequestDTO userModel);
        void Register(UserRegisterRequestDTO registerUserDTO);
        void UpdateUser(int userId, UserUpdateRequestDTO updateUserDTO);
    }
}