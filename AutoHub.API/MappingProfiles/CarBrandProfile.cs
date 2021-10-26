using AutoHub.BLL.Models.CarBrandModels;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarBrandProfile : Profile
    {
        public CarBrandProfile()
        {
            CreateMap<CarBrand, CarBrandResponseModel>();
            CreateMap<CarBrandCreateRequestModel, CarBrand>();
            CreateMap<CarBrandUpdateRequestModel, CarBrand>();
        }
    }
}