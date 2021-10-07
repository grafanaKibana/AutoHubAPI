using System.Collections.Generic;
using AutoHub.BLL.Models;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarModel> GetAll();
        CarModel GetById(int id);
        CarModel CreateCar(CarModel carModel);
        CarModel UpdateCar(CarModel carModel);
    }
}