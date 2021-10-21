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

            CreateMap<BidResponseModel, Bid>()
                .ForMember(dest => dest.User, o => o.MapFrom(model => model.User))
                .ForMember(dest => dest.Lot, o => o.MapFrom(model => model.Lot));
        }
    }
}