using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Car> GetAll()
        {
            return _unitOfWork.Cars.GetAll();
        }

        public Car GetById(int id)
        {
            var car = _unitOfWork.Cars.GetById(id);
            return car;
        }

        public Car CreateCar(Car carModel)
        {
            _unitOfWork.Cars.Add(carModel);
            _unitOfWork.Commit();
            return carModel;
        }

        public Car UpdateCar(Car carModel)
        {
            _unitOfWork.Cars.Update(carModel);
            _unitOfWork.Commit();
            return carModel;
        }
    }
}