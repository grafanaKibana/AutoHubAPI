using System.Collections.Generic;
using System.Linq;
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
            if (!_unitOfWork.CarBrands.Any(brand => brand.CarBrandName == carModel.CarBrand))
                _unitOfWork.CarBrands.Add(new CarBrand { CarBrandName = carModel.CarBrand });

            if (!_unitOfWork.CarModels.Any(model => model.CarModelName == carModel.CarModel))
                _unitOfWork.CarModels.Add(new CarModel { CarModelName = carModel.CarModel });

            if (!_unitOfWork.CarColors.Any(color => color.CarColorName == carModel.CarColor))
                _unitOfWork.CarColors.Add(new CarColor { CarColorName = carModel.CarColor });
            _unitOfWork.Commit();

            _unitOfWork.Cars.Add(new Car
            {
                CarBrand =
                    _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == carModel.CarBrand).FirstOrDefault(),
                CarModel =
                    _unitOfWork.CarModels.Find(model => model.CarModelName == carModel.CarModel).FirstOrDefault(),
                CarColor =
                    _unitOfWork.CarColors.Find(color => color.CarColorName == carModel.CarColor).FirstOrDefault(),
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
            if (!_unitOfWork.CarBrands.Any(brand => brand.CarBrandName == carModel.CarBrand))
                _unitOfWork.CarBrands.Add(new CarBrand { CarBrandName = carModel.CarBrand });

            if (!_unitOfWork.CarModels.Any(model => model.CarModelName == carModel.CarModel))
                _unitOfWork.CarModels.Add(new CarModel { CarModelName = carModel.CarModel });

            if (!_unitOfWork.CarColors.Any(color => color.CarColorName == carModel.CarColor))
                _unitOfWork.CarColors.Add(new CarColor { CarColorName = carModel.CarColor });
            _unitOfWork.Commit();
            _unitOfWork.Cars.Update(id, new Car
            {
                CarId = carModel.CarId,
                CarBrand =
                    _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == carModel.CarBrand).FirstOrDefault(),
                CarModel =
                    _unitOfWork.CarModels.Find(model => model.CarModelName == carModel.CarModel).FirstOrDefault(),
                CarColor =
                    _unitOfWork.CarColors.Find(color => color.CarColorName == carModel.CarColor).FirstOrDefault(),
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