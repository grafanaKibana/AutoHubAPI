using AutoHub.API.Models.CarModelModels;
using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
using AutoHub.Domain.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles;

public class CarModelProfile : Profile
{
    public CarModelProfile()
    {
        //Model <-> DTO maps
        CreateMap<CarModelCreateRequest, CarModelCreateRequestDTO>();
        CreateMap<CarModelUpdateRequest, CarModelUpdateRequestDTO>();

        //DTO <-> Entity maps
        CreateMap<CarModel, CarModelResponseDTO>();
        CreateMap<CarModelCreateRequestDTO, CarModel>();
        CreateMap<CarModelUpdateRequestDTO, CarModel>();
    }
}
