using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        Car CreateCar(Car carModel);
        Car UpdateCar(int id, Car carModel);
    }
}