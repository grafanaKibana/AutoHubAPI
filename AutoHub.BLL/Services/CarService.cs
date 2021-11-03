using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.BLL.Interfaces;
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

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CarResponseDTO> GetAll()
        {
            var cars = _unitOfWork.Cars.GetAll();
            var mappedCars = _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
            return mappedCars;
        }

        public CarResponseDTO GetById(int carId)
        {
            var car = _unitOfWork.Cars.GetById(carId);
            var mappedCar = _mapper.Map<CarResponseDTO>(car);
            return mappedCar;
        }

        public void Create(CarCreateRequestDTO createCarDTO)
        {
            var car = _mapper.Map<Car>(createCarDTO);

            var brand = _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == createCarDTO.CarBrand)
                .FirstOrDefault();
            var model = _unitOfWork.CarModels.Find(model => model.CarModelName == createCarDTO.CarModel)
                .FirstOrDefault();
            var color = _unitOfWork.CarColors.Find(color => color.CarColorName == createCarDTO.CarColor)
                .FirstOrDefault();

            car.CarStatusId = CarStatusEnum.New;
            car.CarBrand = brand ?? new CarBrand { CarBrandName = createCarDTO.CarBrand };
            car.CarModel = model ?? new CarModel { CarModelName = createCarDTO.CarModel };
            car.CarColor = color ?? new CarColor { CarColorName = createCarDTO.CarColor };

            _unitOfWork.Cars.Add(car);
            _unitOfWork.Commit();
        }

        public void Update(int carId, CarUpdateRequestDTO updateCarDTO)
        {
            var car = _unitOfWork.Cars.GetById(carId);

            var brand = _unitOfWork.CarBrands.Find(brand => brand.CarBrandName == updateCarDTO.CarBrand)
                .FirstOrDefault();
            var model = _unitOfWork.CarModels.Find(model => model.CarModelName == updateCarDTO.CarModel)
                .FirstOrDefault();
            var color = _unitOfWork.CarColors.Find(color => color.CarColorName == updateCarDTO.CarColor)
                .FirstOrDefault();

            car.CarBrand = brand ?? new CarBrand { CarBrandName = updateCarDTO.CarBrand };
            car.CarModel = model ?? new CarModel { CarModelName = updateCarDTO.CarModel };
            car.CarColor = color ?? new CarColor { CarColorName = updateCarDTO.CarColor };
            car.CarStatusId = (CarStatusEnum)updateCarDTO.CarStatusId;

            _unitOfWork.Cars.Update(car);
            _unitOfWork.Commit();
        }
    }
}