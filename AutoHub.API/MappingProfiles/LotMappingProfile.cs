using AutoHub.API.Models.LotModels;
using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class LotMappingProfile : Profile
    {
        public LotMappingProfile()
        {
            //Model <-> DTO maps
            CreateMap<LotResponseDTO, LotResponseModel>();
            CreateMap<LotCreateRequestModel, LotCreateRequestDTO>();
            CreateMap<LotUpdateRequestModel, LotUpdateRequestDTO>();

            //DTO <-> Entity maps
            CreateMap<Lot, LotResponseDTO>()
                .ForPath(dest => dest.LotStatus, o => o.MapFrom(lot => lot.LotStatus.LotStatusName));
            CreateMap<LotCreateRequestDTO, Lot>();
            CreateMap<LotUpdateRequestDTO, Lot>();
        }
    }
}