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
            CreateMap<Bid, BidResponseModel>();

            CreateMap<BidCreateRequestModel, Bid>()
                .ForMember(dest => dest.BidTime, o => o.MapFrom(model => DateTime.UtcNow));
        }
    }
}