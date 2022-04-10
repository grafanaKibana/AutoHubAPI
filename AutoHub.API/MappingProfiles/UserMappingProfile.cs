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
            CreateMap<ApplicationUser, UserResponseDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserRegisterRequestDTO, ApplicationUser>();
            CreateMap<UserUpdateRequestDTO, ApplicationUser>();
        }
    }
}