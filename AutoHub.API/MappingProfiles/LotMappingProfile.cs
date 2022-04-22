using AutoHub.API.Models.LotModels;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.Domain.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles;

public class LotMappingProfile : Profile
{
    public LotMappingProfile()
    {
        //Model <-> DTO maps
        CreateMap<LotCreateRequest, LotCreateRequestDTO>();
        CreateMap<LotUpdateRequest, LotUpdateRequestDTO>();

        //DTO <-> Entity maps
        CreateMap<Lot, LotResponseDTO>()
            .ForPath(dest => dest.LotStatus, o => o.MapFrom(lot => lot.LotStatus.LotStatusName));
        CreateMap<LotCreateRequestDTO, Lot>();
        CreateMap<LotUpdateRequestDTO, Lot>();
    }
}