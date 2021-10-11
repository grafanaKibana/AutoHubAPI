using System.Collections.Generic;
using AutoHub.BLL.Models.CarColorModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarColorService
    {
        IEnumerable<CarColorViewModel> GetAll();
        CarColorViewModel GetById(int id);
        CarColorCreateApiModel CreateCarColor(CarColorCreateApiModel carColorModel);
        bool Exist(string carColorName);
    }
}