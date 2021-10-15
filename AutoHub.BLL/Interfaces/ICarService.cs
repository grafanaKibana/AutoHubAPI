using System.Collections.Generic;
using AutoHub.BLL.Models.CarModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarResponseModel> GetAll();
        CarResponseModel GetById(int id);
        CarCreateRequestModel CreateCar(CarCreateRequestModel carModel);
        CarUpdateRequestModel UpdateCar(int id, CarUpdateRequestModel carModel);
    }
}