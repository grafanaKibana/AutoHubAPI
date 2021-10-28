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

            //DTO <-> Entity maps
            CreateMap<User, UserResponseDTO>()
                .ForPath(dest => dest.UserRole, o => o.MapFrom(user => user.UserRole.UserRoleName));
            CreateMap<UserRegisterRequestDTO, User>();
        }
    }
}