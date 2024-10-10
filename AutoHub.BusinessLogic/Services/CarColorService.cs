using System;
using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
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

public class CarColorService(AutoHubContext context, IMapper mapper) : ICarColorService
{
    public async Task<IEnumerable<CarColorResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        List<CarColor> carColors;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.CarColors
            .OrderBy(x => x.CarColorId)
            .Take(limit)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            carColors = await query.Where(x => x.CarColorId > after).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            carColors = await query.Where(x => x.CarColorId < before).ToListAsync();
        }
        else
        {
            carColors = await query.ToListAsync();
        }

        var mappedCarColors = mapper.Map<IEnumerable<CarColorResponseDTO>>(carColors);
        return mappedCarColors;
    }

    public async Task<CarColorResponseDTO> GetById(int carColorId)
    {
        var color = await context.CarColors.FindAsync(carColorId) ?? throw new NotFoundException($"Car color with ID {carColorId} not exist.");

        var mappedColor = mapper.Map<CarColorResponseDTO>(color);
        return mappedColor;
    }

    public async Task Create(CarColorCreateRequestDTO createColorDTO)
    {
        var isDuplicate = await context.CarColors.AnyAsync(carColor => carColor.CarColorName == createColorDTO.CarColorName);

        if (isDuplicate.Equals(true))
        {
            throw new DuplicateException($"\"{createColorDTO.CarColorName}\" already exists.");
        }

        var color = mapper.Map<CarColor>(createColorDTO);
        await context.CarColors.AddAsync(color);
        await context.SaveChangesAsync();
    }

    public async Task Update(int carColorId, CarColorUpdateRequestDTO updateColorDTO)
    {
        var carColor = await context.CarColors.FindAsync(carColorId) ?? throw new NotFoundException($"Car color with ID {carColorId} not exist.");

        carColor.CarColorName = updateColorDTO.CarColorName;

        context.CarColors.Update(carColor);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int carColorId)
    {
        var carColor = await context.CarColors.FindAsync(carColorId) ?? throw new NotFoundException($"Car color with ID {carColorId} not exist.");

        context.CarColors.Remove(carColor);
        await context.SaveChangesAsync();
    }
}
