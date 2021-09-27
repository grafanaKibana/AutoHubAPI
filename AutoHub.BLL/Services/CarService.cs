using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models;
using AutoHub.DAL;

namespace AutoHub.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly AutoHubContext _dbContext = new();

        public IEnumerable<CarModel> GetAll()
        {
            return _dbContext.Car.Select(car =>
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
    }
}