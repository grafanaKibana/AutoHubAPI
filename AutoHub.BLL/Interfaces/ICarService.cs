using System.Collections.Generic;
using AutoHub.BLL.Models;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarModel> GetAll();
    }
}