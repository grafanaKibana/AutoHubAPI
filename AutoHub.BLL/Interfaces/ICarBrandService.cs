using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarBrandService
    {
        IEnumerable<CarBrand> GetAll();
        CarBrand GetById(int id);
        CarBrand CreateCarBrand(CarBrand carBrandModel);
        CarBrand UpdateCarBrand(CarBrand carBrandModel);
        bool Exist(string carBrandName);
    }
}