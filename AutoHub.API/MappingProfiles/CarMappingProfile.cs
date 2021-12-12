using AutoHub.API.Models.CarModels;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            //Model <-> DTO maps
            CreateMap<CarResponseDTO, CarResponseModel>();
            CreateMap<CarCreateRequestModel, CarCreateRequestDTO>();
            CreateMap<CarUpdateRequestModel, CarUpdateRequestDTO>();


            //DTO <-> Entity maps
            CreateMap<Car, CarResponseDTO>()
                .ForPath(dest => dest.CarBrand, o => o.MapFrom(car => car.CarBrand.CarBrandName))
                .ForPath(dest => dest.CarModel, o => o.MapFrom(car => car.CarModel.CarModelName))
                .ForPath(dest => dest.CarColor, o => o.MapFrom(car => car.CarColor.CarColorName))
                .ForPath(dest => dest.CarStatus, o => o.MapFrom(car => car.CarStatus.CarStatusName));
            CreateMap<CarCreateRequestDTO, Car>()
                .ForMember(dest => dest.CarBrand, o => o.Ignore())
                .ForMember(dest => dest.CarModel, o => o.Ignore())
                .ForMember(dest => dest.CarColor, o => o.Ignore())
                .ForMember(dest => dest.CarStatus, o => o.Ignore());

            CreateMap<CarUpdateRequestDTO, Car>();
        }
    }
}