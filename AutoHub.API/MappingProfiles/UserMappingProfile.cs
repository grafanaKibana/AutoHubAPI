using AutoHub.BLL.Models.UserModels;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponseModel>()
                .ForMember(dest => dest.UserId, o => o.MapFrom(user => user.UserId))
                .ForPath(dest => dest.UserRole, o => o.MapFrom(user => user.UserRole.UserRoleName))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(user => user.FirstName))
                .ForMember(dest => dest.LastName, o => o.MapFrom(user => user.LastName))
                .ForMember(dest => dest.Email, o => o.MapFrom(user => user.Email))
                .ForMember(dest => dest.Phone, o => o.MapFrom(user => user.Phone))
                .ForMember(dest => dest.RegistrationTime, o => o.MapFrom(user => user.RegistrationTime));
        }
    }
}