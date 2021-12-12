using AutoHub.API.Models.CarModelModels;
using AutoHub.BLL.DTOs.CarModelDTOs;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarModelProfile : Profile
    {
        public CarModelProfile()
        {
            //Model <-> DTO maps
            CreateMap<CarModelResponseDTO, CarModelResponseModel>();
            CreateMap<CarModelCreateRequestModel, CarModelCreateRequestDTO>();
            CreateMap<CarModelUpdateRequestModel, CarModelUpdateRequestDTO>();

            //DTO <-> Entity maps
            CreateMap<CarModel, CarModelResponseDTO>();
            CreateMap<CarModelCreateRequestDTO, CarModel>();
            CreateMap<CarModelUpdateRequestDTO, CarModel>();
        }
    }
}