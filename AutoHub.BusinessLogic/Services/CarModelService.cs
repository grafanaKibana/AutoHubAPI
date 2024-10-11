using System;
using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.DataAccess;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Constants;

namespace AutoHub.BusinessLogic.Services;

public class CarModelService(AutoHubContext context, IMapper mapper) : ICarModelService
{
    public async Task<IEnumerable<CarModelResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        List<CarModel> carModels;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.CarModels
            .OrderBy(x => x.CarModelId)
            .Take(limit)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            carModels = await query.Where(x => x.CarModelId > after).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            carModels = await query.Where(x => x.CarModelId < before).ToListAsync();
        }
        else
        {
            carModels = await query.ToListAsync();
        }

        var mappedCarModel = mapper.Map<IEnumerable<CarModelResponseDTO>>(carModels);
        return mappedCarModel;
    }

    public async Task<CarModelResponseDTO> GetById(int carModelId)
    {
        var model = await context.CarModels.FindAsync(carModelId) ?? throw new NotFoundException($"Car model with ID {carModelId} not exist.");

        var mappedModels = mapper.Map<CarModelResponseDTO>(model);
        return mappedModels;
    }

    public async Task Create(CarModelCreateRequestDTO createModelDTO)
    {
        var isDuplicate = await context.CarModels.AnyAsync(carModel => carModel.CarModelName == createModelDTO.CarModelName);

        if (isDuplicate.Equals(true))
        {
            throw new DuplicateException($"\"{createModelDTO.CarModelName}\" already exists.");
        }

        var model = mapper.Map<CarModel>(createModelDTO);
        await context.CarModels.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO)
    {
        var carModel = await context.CarModels.FindAsync(carModelId) ?? throw new NotFoundException($"Car model with ID {carModelId} not exist.");

        carModel.CarModelName = updateModelDTO.CarModelName;

        context.CarModels.Update(carModel);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int carModelId)
    {
        var carModel = await context.CarModels.FindAsync(carModelId) ?? throw new NotFoundException($"Car model with ID {carModelId} not exist.");

        context.CarModels.Remove(carModel);
        await context.SaveChangesAsync();
    }
}
