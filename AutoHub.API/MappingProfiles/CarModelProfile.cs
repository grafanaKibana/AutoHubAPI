using AutoHub.BLL.Models.CarModelModels;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarModelProfile : Profile
    {
        public CarModelProfile()
        {
            CreateMap<CarModel, CarModelResponseModel>();
            CreateMap<CarModelCreateRequestModel, CarModel>();
        }
    }
}