using System.Collections.Generic;
using AutoHub.BLL.Models.CarBrandModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarBrandService
    {
        IEnumerable<CarBrandViewModel> GetAll();
        CarBrandViewModel GetById(int id);
        CarBrandCreateApiModel CreateCarBrand(CarBrandCreateApiModel carBrandModel);
        bool Exist(string carBrandName);
    }
}