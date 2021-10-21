using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
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
            if (!_unitOfWork.CarBrands.Any(brand => brand.CarBrandName == carModel.CarBrand.CarBrandName))
                _unitOfWork.CarBrands.Add(new CarBrand { CarBrandName = carModel.CarBrand.CarBrandName });

            if (!_unitOfWork.CarModels.Any(model => model.CarModelName == carModel.CarModel.CarModelName))
                _unitOfWork.CarModels.Add(new CarModel { CarModelName = carModel.CarModel.CarModelName });

            if (!_unitOfWork.CarColors.Any(color => color.CarColorName == carModel.CarColor.CarColorName))
                _unitOfWork.CarColors.Add(new CarColor { CarColorName = carModel.CarColor.CarColorName });
            _unitOfWork.Commit();

            _unitOfWork.Cars.Add(new Car
            {
                CarBrand =
                    _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == carModel.CarBrand.CarBrandName)
                        .FirstOrDefault(),
                CarModel =
                    _unitOfWork.CarModels.Find(model => model.CarModelName == carModel.CarModel.CarModelName)
                        .FirstOrDefault(),
                CarColor =
                    _unitOfWork.CarColors.Find(color => color.CarColorName == carModel.CarColor.CarColorName)
                        .FirstOrDefault(),
                ImgUrl = carModel.ImgUrl,
                Description = carModel.Description,
                Year = carModel.Year,
                VIN = carModel.VIN,
                Mileage = carModel.Mileage,
                CostPrice = carModel.CostPrice,
                SellingPrice = carModel.SellingPrice,
                CarStatusId = CarStatusEnum.New
            });

            _unitOfWork.Commit();
            return carModel;
        }

        public Car UpdateCar(int id, Car carModel)
        {
            if (!_unitOfWork.CarBrands.Any(brand => brand.CarBrandName == carModel.CarBrand.CarBrandName))
                _unitOfWork.CarBrands.Add(new CarBrand { CarBrandName = carModel.CarBrand.CarBrandName });

            if (!_unitOfWork.CarModels.Any(model => model.CarModelName == carModel.CarModel.CarModelName))
                _unitOfWork.CarModels.Add(new CarModel { CarModelName = carModel.CarModel.CarModelName });

            if (!_unitOfWork.CarColors.Any(color => color.CarColorName == carModel.CarColor.CarColorName))
                _unitOfWork.CarColors.Add(new CarColor { CarColorName = carModel.CarColor.CarColorName });
            _unitOfWork.Commit();
            _unitOfWork.Cars.Update(id, new Car
            {
                CarId = carModel.CarId,
                CarBrand =
                    _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == carModel.CarBrand.CarBrandName)
                        .FirstOrDefault(),
                CarModel =
                    _unitOfWork.CarModels.Find(model => model.CarModelName == carModel.CarModel.CarModelName)
                        .FirstOrDefault(),
                CarColor =
                    _unitOfWork.CarColors.Find(color => color.CarColorName == carModel.CarColor.CarColorName)
                        .FirstOrDefault(),
                ImgUrl = carModel.ImgUrl,
                Description = carModel.Description,
                Year = carModel.Year,
                VIN = carModel.VIN,
                Mileage = carModel.Mileage,
                CostPrice = carModel.CostPrice,
                SellingPrice = carModel.SellingPrice,
                CarStatusId = carModel.CarStatusId
            });

            _unitOfWork.Commit();
            return carModel;
        }
    }
}