using AutoHub.API.Models.CarColorModels;
using AutoHub.BLL.DTOs.CarColorDTOs;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarColorProfile : Profile
    {
        public CarColorProfile()
        {
            //Model <-> DTO maps
            CreateMap<CarColorResponseDTO, CarColorResponseModel>();
            CreateMap<CarColorCreateRequestModel, CarColorCreateRequestDTO>();
            CreateMap<CarColorUpdateRequestModel, CarColorUpdateRequestDTO>();

            //DTO <-> Entity maps
            CreateMap<CarColor, CarColorResponseDTO>();
            CreateMap<CarColorCreateRequestDTO, CarColor>();
            CreateMap<CarColorUpdateRequestDTO, CarColor>();
        }
    }
}