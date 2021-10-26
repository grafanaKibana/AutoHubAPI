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
                .ForPath(dest => dest.LotStatus, o => o.MapFrom(lot => lot.LotStatus.LotStatusName));

            CreateMap<LotCreateRequestModel, Lot>()
                .ForMember(dest => dest.LotStatusId, o => o.MapFrom(model => LotStatusEnum.New))
                .ForMember(dest => dest.StartTime, o => o.MapFrom(model => DateTime.UtcNow))
                .ForMember(dest => dest.EndTime,
                    o => o.MapFrom(model => DateTime.UtcNow.AddDays(model.DurationInDays)));

            CreateMap<LotUpdateRequestModel, Lot>()
                .ForMember(dest => dest.LotStatusId, o => o.MapFrom(model => (LotStatusEnum)model.LotStatusId));
            //TODO: I need to map Duration in days to destination.EndTime = destination.StartTime + model.DurationInDays
            //// .ForMember(dest => dest.EndTime, o => o.MapFrom(model => ));
            // .ForMember(dest => dest.StartTime, o => o.MapFrom(model => model))
        }
    }
}