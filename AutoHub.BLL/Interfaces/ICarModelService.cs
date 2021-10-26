using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModel> GetAll();
        CarModel GetById(int id);
        CarModel CreateCarModel(CarModel carModelModel);
        CarModel UpdateCarModel(CarModel carModelModel);
        bool Exist(string carModelName);
    }
}