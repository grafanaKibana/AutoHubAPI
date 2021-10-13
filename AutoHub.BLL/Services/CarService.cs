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
                .CreateMap<Car, CarResponseModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<CarResponseModel> GetAll()
        {
            return _mapper.Map<IEnumerable<CarResponseModel>>(_unitOfWork.Cars.GetAll());
        }

        public CarResponseModel GetById(int id)
        {
            var car = _unitOfWork.Cars.GetById(id);

            if (car == null)
                return null;

            return _mapper.Map<CarResponseModel>(car);
        }

        public CarCreateRequestModel CreateCar(CarCreateRequestModel carModel)
        {
            _unitOfWork.Cars.Add(new Car
            {
                CarId = carModel.CarId,
                CarBrandId = carModel.CarBrandId,
                CarModelId = carModel.CarModelId,
                CarColorId = carModel.CarColorId,
                ImgUrl = carModel.ImgUrl,
                Description = carModel.Description,
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

        public CarUpdateRequestModel UpdateCar(int id, CarUpdateRequestModel carModel)
        {
            _unitOfWork.Cars.Update(id, new Car
            {
                CarId = carModel.CarId,
                CarBrandId = carModel.CarBrandId,
                CarModelId = carModel.CarModelId,
                CarColorId = carModel.CarColorId,
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