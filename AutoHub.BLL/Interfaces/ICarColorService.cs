using System.Collections.Generic;
using AutoHub.BLL.Models.CarColorModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarColorService
    {
        IEnumerable<CarColorResponseModel> GetAll();
        CarColorResponseModel GetById(int id);
        CarColorCreateRequestModel CreateCarColor(CarColorCreateRequestModel carColorModel);
        bool Exist(string carColorName);
    }
}