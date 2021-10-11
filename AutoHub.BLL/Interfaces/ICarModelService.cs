using System.Collections.Generic;
using AutoHub.BLL.Models.CarModelModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModelViewModel> GetAll();
        CarModelViewModel GetById(int id);
        CarModelCreateApiModel CreateCarModel(CarModelCreateApiModel carModelModel);
        bool Exist(string carModelName);
    }
}