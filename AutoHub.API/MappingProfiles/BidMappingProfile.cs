using AutoHub.API.Models.BidModels;
using AutoHub.BusinessLogic.DTOs.BidDTOs;
using AutoHub.Domain.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles;

public class BidMappingProfile : Profile
{
    public BidMappingProfile()
    {
        //Model <-> DTO maps
        CreateMap<BidCreateRequest, BidCreateRequestDTO>();

        //DTO <-> Entity maps
        CreateMap<Bid, BidResponseDTO>();
        CreateMap<BidCreateRequestDTO, Bid>();
    }
}
