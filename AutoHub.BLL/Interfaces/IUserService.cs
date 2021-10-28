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
        void Register(UserRegisterRequestDTO registerUserDTO);
        void UpdateUser(UserUpdateRequestDTO updateUserDTO);
        bool Login(UserLoginRequestDTO userModel);
    }
}