using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class BidMappingProfile : Profile
    {
        public BidMappingProfile()
        {
            //Model <-> DTO maps
            CreateMap<BidResponseDTO, BidResponseModel>();
            CreateMap<BidCreateRequestModel, BidCreateRequestDTO>();

            //DTO <-> Entity maps
            CreateMap<Bid, BidResponseDTO>();
            CreateMap<BidCreateRequestDTO, Bid>();
        }
    }
}