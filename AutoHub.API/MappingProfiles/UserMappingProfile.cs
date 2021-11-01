using System;
using AutoHub.BLL.Models.UserModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponseModel>()
                .ForPath(dest => dest.UserRole, o => o.MapFrom(user => user.UserRole.UserRoleName));

            CreateMap<UserRegisterRequestModel, User>()
                .ForMember(dest => dest.FirstName, o => o.MapFrom(model => model.FirstName))
                .ForMember(dest => dest.LastName, o => o.MapFrom(model => model.LastName))
                .ForMember(dest => dest.UserRoleId, o => o.MapFrom(model => UserRoleEnum.Regular))
                .ForMember(dest => dest.RegistrationTime, o => o.MapFrom(model => DateTime.UtcNow));
        }
    }
}