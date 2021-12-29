using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Entities.Identity;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            //Model <-> DTO maps
            CreateMap<UserResponseDTO, UserResponseModel>();
            CreateMap<UserRegisterRequestModel, UserRegisterRequestDTO>();
            CreateMap<UserLoginRequestModel, UserLoginRequestDTO>();
            CreateMap<UserLoginResponseDTO, UserLoginResponseModel>();
            CreateMap<UserUpdateRequestModel, UserUpdateRequestDTO>();

            //DTO <-> Entity maps
            CreateMap<AppUser, UserResponseDTO>();
            CreateMap<UserRegisterRequestDTO, AppUser>();
            CreateMap<UserUpdateRequestDTO, AppUser>();
        }
    }
}