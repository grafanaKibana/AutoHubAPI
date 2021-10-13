using System.Collections.Generic;
using AutoHub.BLL.Models.CarModelModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModelResponseModel> GetAll();
        CarModelResponseModel GetById(int id);
        CarModelCreateRequestModel CreateCarModel(CarModelCreateRequestModel carModelModel);
        bool Exist(string carModelName);
    }
}