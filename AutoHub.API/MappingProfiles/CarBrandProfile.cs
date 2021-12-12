using AutoHub.API.Models.CarBrandModels;
using AutoHub.BLL.DTOs.CarBrandDTOs;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarBrandProfile : Profile
    {
        public CarBrandProfile()
        {
            //Model <-> DTO maps
            CreateMap<CarBrandResponseDTO, CarBrandResponseModel>();
            CreateMap<CarBrandCreateRequestModel, CarBrandCreateRequestDTO>();
            CreateMap<CarBrandUpdateRequestModel, CarBrandUpdateRequestDTO>();

            //DTO <-> Entity maps
            CreateMap<CarBrand, CarBrandResponseDTO>();
            CreateMap<CarBrandCreateRequestDTO, CarBrand>();
            CreateMap<CarBrandUpdateRequestDTO, CarBrand>();
        }
    }
}