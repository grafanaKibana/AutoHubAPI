﻿using AutoHub.BusinessLogic.DTOs.CarDTOs;
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
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Constants;

namespace AutoHub.BusinessLogic.Services;

public class CarService(AutoHubContext context, IMapper mapper) : ICarService
{
    public async Task<IEnumerable<CarResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        List<Car> cars;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.Cars
            .OrderBy(x => x.CarId)
            .Take(limit)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            cars = await query.Where(x => x.CarId > after).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            cars = await query.Where(x => x.CarId < before).ToListAsync();
        }
        else
        {
            cars = await query.ToListAsync();
        }

        var mappedCars = mapper.Map<IEnumerable<CarResponseDTO>>(cars);
        return mappedCars;
    }

    public async Task<CarResponseDTO> GetById(int carId)
    {
        var car = await context.Cars.FindAsync(carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        var mappedCar = mapper.Map<CarResponseDTO>(car);
        return mappedCar;
    }

    public async Task Create(CarCreateRequestDTO createCarDTO)
    {
        var car = mapper.Map<Car>(createCarDTO);

        var brand = await context.CarBrands.FirstOrDefaultAsync(carBrand => carBrand.CarBrandName == createCarDTO.CarBrand);
        var model = await context.CarModels.FirstOrDefaultAsync(carModel => carModel.CarModelName == createCarDTO.CarModel);
        var color = await context.CarColors.FirstOrDefaultAsync(carColor => carColor.CarColorName == createCarDTO.CarColor);

        car.CarBrand = brand ?? new CarBrand { CarBrandName = createCarDTO.CarBrand };
        car.CarModel = model ?? new CarModel { CarModelName = createCarDTO.CarModel };
        car.CarColor = color ?? new CarColor { CarColorName = createCarDTO.CarColor };
        car.CarStatusId = CarStatusEnum.New;

        await context.Cars.AddAsync(car);
        await context.SaveChangesAsync();
    }

    public async Task Update(int carId, CarUpdateRequestDTO updateCarDTO)
    {
        if (Enum.IsDefined(typeof(CarStatusEnum), updateCarDTO.CarStatusId).Equals(false))
        {
            throw new EntityValidationException($"Incorrect {nameof(CarStatus.CarStatusId)} value.");
        }

        var car = await context.Cars.FindAsync(carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        if (car.CarBrand.CarBrandName != updateCarDTO.CarBrand)
        {
            var brand = await context.CarBrands.FirstOrDefaultAsync(carBrand => carBrand.CarBrandName == updateCarDTO.CarBrand);
            car.CarBrand = brand ?? new CarBrand { CarBrandName = updateCarDTO.CarBrand };
        }

        if (car.CarModel.CarModelName != updateCarDTO.CarModel)
        {
            var model = await context.CarModels.FirstOrDefaultAsync(carModel => carModel.CarModelName == updateCarDTO.CarModel);
            car.CarModel = model ?? new CarModel { CarModelName = updateCarDTO.CarModel };
        }

        if (car.CarColor.CarColorName != updateCarDTO.CarColor)
        {
            var color = await context.CarColors.FirstOrDefaultAsync(carColor => carColor.CarColorName == updateCarDTO.CarColor);
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

        context.Cars.Update(car);
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatus(int carId, int statusId)
    {
        if (Enum.IsDefined(typeof(CarStatusEnum), statusId).Equals(false))
        {
            throw new EntityValidationException($"Incorrect {nameof(CarStatus.CarStatusId)} value.");
        }

        var car = await context.Cars.FindAsync(carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        car.CarStatusId = (CarStatusEnum)statusId;

        context.Cars.Update(car);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int carId)
    {
        var car = await context.Cars.FindAsync(carId) ?? throw new NotFoundException($"Car with ID {carId} not exist.");

        context.Cars.Remove(car);
        await context.SaveChangesAsync();
    }
}
