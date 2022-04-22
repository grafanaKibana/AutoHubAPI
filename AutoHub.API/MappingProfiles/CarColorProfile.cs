using AutoHub.API.Models.CarColorModels;
using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
using AutoHub.Domain.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles;

public class CarColorProfile : Profile
{
    public CarColorProfile()
    {
        //Model <-> DTO maps
        CreateMap<CarColorCreateRequest, CarColorCreateRequestDTO>();
        CreateMap<CarColorUpdateRequest, CarColorUpdateRequestDTO>();

        //DTO <-> Entity maps
        CreateMap<CarColor, CarColorResponseDTO>();
        CreateMap<CarColorCreateRequestDTO, CarColor>();
        CreateMap<CarColorUpdateRequestDTO, CarColor>();
    }
}