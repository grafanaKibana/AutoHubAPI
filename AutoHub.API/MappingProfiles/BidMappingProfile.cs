using System;
using AutoHub.BLL.Models.BidModels;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class BidMappingProfile : Profile
    {
        public BidMappingProfile()
        {
            CreateMap<Bid, BidResponseModel>()
                .ForMember(dest => dest.User, o => o.MapFrom(bid => bid.User))
                .ForMember(dest => dest.Lot, o => o.MapFrom(bid => bid.Lot));

            CreateMap<BidCreateRequestModel, Bid>()
                .ForMember(dest => dest.UserId, o => o.MapFrom(model => model.UserId))
                .ForMember(dest => dest.LotId, o => o.MapFrom(model => model.LotId))
                .ForMember(dest => dest.BidValue, o => o.MapFrom(model => model.BidValue))
                .ForMember(dest => dest.BidTime, o => o.MapFrom(model => DateTime.UtcNow));
        }
    }
}