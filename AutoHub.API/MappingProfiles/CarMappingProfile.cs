using AutoHub.BLL.Models.CarModels;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarResponseModel>()
                .ForPath(dest => dest.CarBrand, o => o.MapFrom(car => car.CarBrand.CarBrandName))
                .ForPath(dest => dest.CarModel, o => o.MapFrom(car => car.CarModel.CarModelName))
                .ForPath(dest => dest.CarStatus, o => o.MapFrom(car => car.CarStatus.CarStatusName))
                .ForPath(dest => dest.CarColor, o => o.MapFrom(car => car.CarColor.CarColorName));

            CreateMap<CarBaseRequestModel, Car>()
                .ForPath(dest => dest.CarBrand, o => o.MapFrom(car => new CarBrand
                {
                    CarBrandName = car.CarBrand
                }))
                .ForPath(dest => dest.CarModel, o => o.MapFrom(car => new CarModel
                {
                    CarModelName = car.CarColor
                }))
                .ForPath(dest => dest.CarColor, o => o.MapFrom(car => new CarColor
                {
                    CarColorName = car.CarColor
                }));

            CreateMap<CarCreateRequestModel, Car>()
                .IncludeBase<CarBaseRequestModel, Car>();

            CreateMap<CarUpdateRequestModel, Car>()
                .IncludeBase<CarBaseRequestModel, Car>();
        }
    }
}