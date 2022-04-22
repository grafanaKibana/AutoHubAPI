using AutoHub.API.Models.CarBrandModels;
using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
using AutoHub.Domain.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles;

public class CarBrandProfile : Profile
{
    public CarBrandProfile()
    {
        //Model <-> DTO maps
        CreateMap<CarBrandCreateRequest, CarBrandCreateRequestDTO>();
        CreateMap<CarBrandUpdateRequest, CarBrandUpdateRequestDTO>();

        //DTO <-> Entity maps
        CreateMap<CarBrand, CarBrandResponseDTO>();
        CreateMap<CarBrandCreateRequestDTO, CarBrand>();
        CreateMap<CarBrandUpdateRequestDTO, CarBrand>();
    }
}