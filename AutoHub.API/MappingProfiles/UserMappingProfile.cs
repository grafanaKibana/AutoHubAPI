using AutoHub.API.Models.UserModels;
using AutoHub.BusinessLogic.DTOs.UserDTOs;
using AutoHub.Domain.Entities.Identity;
using AutoMapper;

namespace AutoHub.API.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        //Model <-> DTO maps
        CreateMap<UserRegisterRequest, UserRegisterRequestDTO>();
        CreateMap<UserLoginRequest, UserLoginRequestDTO>();
        CreateMap<UserLoginResponseDTO, UserLoginResponse>();
        CreateMap<UserUpdateRequest, UserUpdateRequestDTO>();

        //DTO <-> Entity maps
        CreateMap<ApplicationUser, UserResponseDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        CreateMap<UserRegisterRequestDTO, ApplicationUser>();
        CreateMap<UserUpdateRequestDTO, ApplicationUser>();
    }
}
