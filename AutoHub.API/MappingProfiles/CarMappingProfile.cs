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
                .ForMember(dest => dest.CarId, o => o.MapFrom(car => car.CarId))
                .ForPath(dest => dest.CarBrand, o => o.MapFrom(car => car.CarBrand.CarBrandName))
                .ForPath(dest => dest.CarModel, o => o.MapFrom(car => car.CarModel.CarModelName))
                .ForPath(dest => dest.CarStatus, o => o.MapFrom(car => car.CarStatus.CarStatusName))
                .ForPath(dest => dest.CarColor, o => o.MapFrom(car => car.CarColor.CarColorName))
                .ForMember(dest => dest.ImgUrl, o => o.MapFrom(car => car.ImgUrl))
                .ForMember(dest => dest.Description, o => o.MapFrom(car => car.Description))
                .ForMember(dest => dest.Year, o => o.MapFrom(car => car.Year))
                .ForMember(dest => dest.VIN, o => o.MapFrom(car => car.VIN))
                .ForMember(dest => dest.Mileage, o => o.MapFrom(car => car.Mileage))
                .ForMember(dest => dest.SellingPrice, o => o.MapFrom(car => car.SellingPrice));

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
                }))
                .ForMember(dest => dest.ImgUrl, o => o.MapFrom(car => car.ImgUrl))
                .ForMember(dest => dest.Description, o => o.MapFrom(car => car.Description))
                .ForMember(dest => dest.Year, o => o.MapFrom(car => car.Year))
                .ForMember(dest => dest.VIN, o => o.MapFrom(car => car.VIN))
                .ForMember(dest => dest.Mileage, o => o.MapFrom(car => car.Mileage))
                .ForMember(dest => dest.SellingPrice, o => o.MapFrom(car => car.SellingPrice));

            CreateMap<CarCreateRequestModel, Car>()
                .IncludeBase<CarBaseRequestModel, Car>()
                .ForMember(dest => dest.CostPrice, o => o.MapFrom(model => model.CostPrice));

            CreateMap<CarUpdateRequestModel, Car>()
                .IncludeBase<CarBaseRequestModel, Car>()
                .ForMember(dest => dest.CarId, o => o.MapFrom(model => model.CarId))
                .ForMember(dest => dest.CostPrice, o => o.MapFrom(model => model.CostPrice))
                .ForMember(dest => dest.CarStatusId, o => o.MapFrom(model => model.CarStatusId));
        }
    }
}