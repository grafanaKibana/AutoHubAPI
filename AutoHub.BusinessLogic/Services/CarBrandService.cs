using System;
using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
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

public class CarBrandService : ICarBrandService
{
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;

    public CarBrandService(AutoHubContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarBrandResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        List<CarBrand> carBrands;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = _context.CarBrands
            .OrderBy(x => x.CarBrandId)
            .Take(limit)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            carBrands = await query.Where(x => x.CarBrandId > after).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            carBrands = await query.Where(x => x.CarBrandId < before).ToListAsync();
        }
        else
        {
            carBrands = await query.ToListAsync();
        }

        var mappedCarBrands = _mapper.Map<IEnumerable<CarBrandResponseDTO>>(carBrands);
        return mappedCarBrands;
    }

    public async Task<CarBrandResponseDTO> GetById(int carBrandId)
    {
        var brand = await _context.CarBrands.FindAsync(carBrandId) ?? throw new NotFoundException($"Car brand with ID {carBrandId} not exist.");

        var mappedBrand = _mapper.Map<CarBrandResponseDTO>(brand);
        return mappedBrand;
    }

    public async Task Create(CarBrandCreateRequestDTO createBrandDTO)
    {
        var isDuplicate = await _context.CarBrands.AnyAsync(carBrand => carBrand.CarBrandName == createBrandDTO.CarBrandName);

        if (isDuplicate)
        {
            throw new DuplicateException($"\"{createBrandDTO.CarBrandName}\" already exists.");
        }

        var brand = _mapper.Map<CarBrand>(createBrandDTO);
        await _context.CarBrands.AddAsync(brand);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int carBrandId, CarBrandUpdateRequestDTO updateBrandDTO)
    {
        var carBrand = await _context.CarBrands.FindAsync(carBrandId) ?? throw new NotFoundException($"Car brand with ID {carBrandId} not exist.");

        carBrand.CarBrandName = updateBrandDTO.CarBrandName;

        _context.CarBrands.Update(carBrand);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int carBrandId)
    {
        var carBrand = await _context.CarBrands.FindAsync(carBrandId) ?? throw new NotFoundException($"Car brand with ID {carBrandId} not exist.");

        _context.CarBrands.Remove(carBrand);
        await _context.SaveChangesAsync();
    }
}
