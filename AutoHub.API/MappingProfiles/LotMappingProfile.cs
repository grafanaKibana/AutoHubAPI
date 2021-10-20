using System;
using AutoHub.BLL.Models.LotModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class LotMappingProfile : Profile
    {
        public LotMappingProfile()
        {
            CreateMap<Lot, LotResponseModel>()
                .ForMember(dest => dest.LotId, o => o.MapFrom(lot => lot.LotId))
                .ForPath(dest => dest.LotStatus, o => o.MapFrom(lot => lot.LotStatus.LotStatusName))
                .ForMember(dest => dest.Creator, o => o.MapFrom(lot => lot.Creator))
                .ForMember(dest => dest.Car, o => o.MapFrom(lot => lot.Car))
                .ForMember(dest => dest.Winner, o => o.MapFrom(lot => lot.Winner))
                .ForMember(dest => dest.StartTime, o => o.MapFrom(lot => lot.StartTime))
                .ForMember(dest => dest.EndTime, o => o.MapFrom(lot => lot.EndTime));

            CreateMap<LotCreateRequestModel, Lot>()
                .ForMember(dest => dest.LotStatusId, o => o.MapFrom(model => LotStatusId.New))
                .ForMember(dest => dest.CreatorId, o => o.MapFrom(model => model.UserId))
                .ForMember(dest => dest.CarId, o => o.MapFrom(model => model.CarId))
                .ForMember(dest => dest.StartTime, o => o.MapFrom(model => DateTime.UtcNow))
                .ForMember(dest => dest.EndTime,
                    o => o.MapFrom(model => DateTime.UtcNow.AddDays(model.DurationInDays)));
        }
    }
}