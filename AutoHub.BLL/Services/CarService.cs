using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models;

namespace AutoHub.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CarModel> GetAll()
        {
            return _unitOfWork.Cars.GetAll().Select(car =>
                new CarModel
                {
                    CarId = car.CarId,
                    ImgUrl = car.ImgUrl,
                    Brand = car.Brand,
                    Model = car.Model,
                    Description = car.Description,
                    Color = car.Color,
                    Year = car.Year,
                    VIN = car.VIN,
                    Mileage = car.Mileage,
                    SellingPrice = car.SellingPrice
                });
        }

        public CarModel GetById(int id)
        {
            var car = _unitOfWork.Cars.Find(id);

            if (car == null) 
                return null;

            return new CarModel
            {
                CarId = car.CarId,
                ImgUrl = car.ImgUrl,
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                Color = car.Color,
                Year = car.Year,
                VIN = car.VIN,
                Mileage = car.Mileage,
                SellingPrice = car.SellingPrice
            };
        }
    }
}