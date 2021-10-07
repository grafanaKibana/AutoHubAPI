using System.Collections.Generic;
using AutoHub.BLL.Models.CarModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarViewModel> GetAll();
        CarViewModel GetById(int id);
        CarCreateApiModel CreateCar(CarCreateApiModel carModel);
        CarUpdateApiModel UpdateCar(int id, CarUpdateApiModel carModel);
    }
}