using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using AutoHub.DataAccess;
using AutoHub.Domain.Constants;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Services;

public class CarModelService : ICarModelService
{
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;

    public CarModelService(AutoHubContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarModelResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        List<CarModel> carModels;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = _context.CarModels
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

        var mappedCarModel = _mapper.Map<IEnumerable<CarModelResponseDTO>>(carModels);
        return mappedCarModel;
    }

    public async Task<CarModelResponseDTO> GetById(int carModelId)
    {
        var model = await _context.CarModels.FindAsync(carModelId) ?? throw new NotFoundException($"Car model with ID {carModelId} not exist.");

        var mappedModels = _mapper.Map<CarModelResponseDTO>(model);
        return mappedModels;
    }

    public async Task Create(CarModelCreateRequestDTO createModelDTO)
    {
        var isDuplicate = await _context.CarModels.AnyAsync(carModel => carModel.CarModelName == createModelDTO.CarModelName);

        if (isDuplicate.Equals(true))
        {
            throw new DuplicateException($"\"{createModelDTO.CarModelName}\" already exists.");
        }

        var model = _mapper.Map<CarModel>(createModelDTO);
        await _context.CarModels.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO)
    {
        var carModel = await _context.CarModels.FindAsync(carModelId) ?? throw new NotFoundException($"Car model with ID {carModelId} not exist.");

        carModel.CarModelName = updateModelDTO.CarModelName;

        _context.CarModels.Update(carModel);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int carModelId)
    {
        var carModel = await _context.CarModels.FindAsync(carModelId) ?? throw new NotFoundException($"Car model with ID {carModelId} not exist.");

        _context.CarModels.Remove(carModel);
        await _context.SaveChangesAsync();
    }
}