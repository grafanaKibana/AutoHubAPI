using AutoHub.BusinessLogic.DTOs.CarDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.DataAccess;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoHub.BusinessLogic.Services;

public class CarService : ICarService
{
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;

    public CarService(AutoHubContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<CarResponseDTO> GetAll()
    {
        var cars = _context.Cars
            .Include(car => car.CarBrand)
            .Include(car => car.CarModel)
            .Include(car => car.CarColor)
            .Include(car => car.CarStatus)
            .ToList();

        var mappedCars = _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
        return mappedCars;
    }

    public CarResponseDTO GetById(int carId)
    {
        var car = _context.Cars
            .Include(car => car.CarBrand)
            .Include(car => car.CarModel)
            .Include(car => car.CarColor)
            .Include(car => car.CarStatus)
            .FirstOrDefault(car => car.CarId == carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        var mappedCar = _mapper.Map<CarResponseDTO>(car);
        return mappedCar;
    }

    public void Create(CarCreateRequestDTO createCarDTO)
    {
        var car = _mapper.Map<Car>(createCarDTO);

        var brand = _context.CarBrands.FirstOrDefault(carBrand => carBrand.CarBrandName == createCarDTO.CarBrand);
        var model = _context.CarModels.FirstOrDefault(carModel => carModel.CarModelName == createCarDTO.CarModel);
        var color = _context.CarColors.FirstOrDefault(carColor => carColor.CarColorName == createCarDTO.CarColor);

        car.CarBrand = brand ?? new CarBrand { CarBrandName = createCarDTO.CarBrand };
        car.CarModel = model ?? new CarModel { CarModelName = createCarDTO.CarModel };
        car.CarColor = color ?? new CarColor { CarColorName = createCarDTO.CarColor };
        car.CarStatusId = CarStatusEnum.New;

        _context.Cars.Add(car);
        _context.SaveChanges();
    }

    public void Update(int carId, CarUpdateRequestDTO updateCarDTO)
    {
        if (Enum.IsDefined(typeof(CarStatusEnum), updateCarDTO.CarStatusId).Equals(false))
        {
            throw new EntityValidationException($"Incorrect {nameof(CarStatus.CarStatusId)} value.");
        }

        var car = _context.Cars
            .Include(car => car.CarBrand)
            .Include(car => car.CarModel)
            .Include(car => car.CarColor)
            .Include(car => car.CarStatus)
            .FirstOrDefault(car => car.CarId == carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        if (car.CarBrand.CarBrandName != updateCarDTO.CarBrand)
        {
            var brand = _context.CarBrands.FirstOrDefault(
                carBrand => carBrand.CarBrandName == updateCarDTO.CarBrand);
            car.CarBrand = brand ?? new CarBrand { CarBrandName = updateCarDTO.CarBrand };
        }

        if (car.CarModel.CarModelName != updateCarDTO.CarModel)
        {
            var model = _context.CarModels.FirstOrDefault(
                carModel => carModel.CarModelName == updateCarDTO.CarModel);
            car.CarModel = model ?? new CarModel { CarModelName = updateCarDTO.CarModel };
        }

        if (car.CarColor.CarColorName != updateCarDTO.CarColor)
        {
            var color = _context.CarColors.FirstOrDefault(
                carColor => carColor.CarColorName == updateCarDTO.CarColor);
            car.CarColor = color ?? new CarColor { CarColorName = updateCarDTO.CarColor };
        }

        car.ImgUrl = updateCarDTO.ImgUrl;
        car.Description = updateCarDTO.Description;
        car.Year = updateCarDTO.Year;
        car.VIN = updateCarDTO.VIN;
        car.Mileage = updateCarDTO.Mileage;
        car.SellingPrice = updateCarDTO.SellingPrice;
        car.CostPrice = updateCarDTO.CostPrice;
        car.CarStatusId = (CarStatusEnum)updateCarDTO.CarStatusId;

        _context.Cars.Update(car);
        _context.SaveChanges();
    }

    public void UpdateStatus(int carId, int statusId)
    {
        if (Enum.IsDefined(typeof(CarStatusEnum), statusId).Equals(false))
        {
            throw new EntityValidationException($"Incorrect {nameof(CarStatus.CarStatusId)} value.");
        }

        var car = _context.Cars.Find(carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        car.CarStatusId = (CarStatusEnum)statusId;

        _context.Cars.Update(car);
        _context.SaveChanges();
    }

    public void Delete(int carId)
    {
        var car = _context.Cars.Find(carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        _context.Cars.Remove(car);
        _context.SaveChanges();
    }
}
