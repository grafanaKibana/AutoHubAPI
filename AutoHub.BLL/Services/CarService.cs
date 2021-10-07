using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg
                .CreateMap<Car, CarViewModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<CarViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarViewModel>>(_unitOfWork.Cars.GetAll());
        }

        public CarViewModel GetById(int id)
        {
            var car = _unitOfWork.Cars.GetById(id);

            if (car == null)
                return null;

            return _mapper.Map<CarViewModel>(car);
        }

        public CarCreateApiModel CreateCar(CarCreateApiModel carModel)
        {
            _unitOfWork.Cars.Add(new Car
            {
                CarId = carModel.CarId,
                ImgUrl = carModel.ImgUrl,
                Brand = carModel.Brand,
                Model = carModel.Model,
                Description = carModel.Description,
                Color = carModel.Color,
                Year = carModel.Year,
                VIN = carModel.VIN,
                Mileage = carModel.Mileage,
                CostPrice = carModel.CostPrice,
                SellingPrice = carModel.SellingPrice,
                CarStatusId = CarStatusId.New
            });

            _unitOfWork.Commit();
            return carModel;
        }

        public CarUpdateApiModel UpdateCar(CarUpdateApiModel carModel)
        {
            _unitOfWork.Cars.Update(carModel.CarId, new Car
            {
                CarId = carModel.CarId,
                ImgUrl = carModel.ImgUrl,
                Brand = carModel.Brand,
                Model = carModel.Model,
                Description = carModel.Description,
                Color = carModel.Color,
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