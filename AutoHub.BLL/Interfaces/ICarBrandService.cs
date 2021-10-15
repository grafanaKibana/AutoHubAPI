using System.Collections.Generic;
using AutoHub.BLL.Models.CarBrandModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarBrandService
    {
        IEnumerable<CarBrandResponseModel> GetAll();
        CarBrandResponseModel GetById(int id);
        CarBrandCreateRequestModel CreateCarBrand(CarBrandCreateRequestModel carBrandModel);
        bool Exist(string carBrandName);
    }
}