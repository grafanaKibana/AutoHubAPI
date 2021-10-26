using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarColorService
    {
        IEnumerable<CarColor> GetAll();
        CarColor GetById(int id);
        CarColor CreateCarColor(CarColor carColorModel);
        CarColor UpdateCarColor(CarColor carColorModel);
        bool Exist(string carColorName);
    }
}