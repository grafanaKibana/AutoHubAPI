using AutoHub.BLL.Models.CarColorModels;
using AutoHub.DAL.Entities;
using AutoMapper;

namespace AutoHub.API.MappingProfiles
{
    public class CarColorProfile : Profile
    {
        public CarColorProfile()
        {
            CreateMap<CarColor, CarColorResponseModel>();
            CreateMap<CarColorCreateRequestModel, CarColor>();
        }
    }
}