using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.DAL.Entities;
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
            CreateMap<User, UserResponseDTO>()
                .ForPath(dest => dest.UserRole, o => o.MapFrom(user => user.UserRole.UserRoleName));
            CreateMap<UserRegisterRequestDTO, User>();
            CreateMap<UserUpdateRequestDTO, User>();
        }
    }
}