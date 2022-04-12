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

namespace AutoHub.BusinessLogic.Services;

public class CarColorService : ICarColorService
{
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;

    public CarColorService(AutoHubContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarColorResponseDTO>> GetAll()
    {
        var colors = await _context.CarColors.ToListAsync();
        var mappedColors = _mapper.Map<IEnumerable<CarColorResponseDTO>>(colors);
        return mappedColors;
    }

    public async Task<CarColorResponseDTO> GetById(int carColorId)
    {
        var color = await _context.CarColors.FindAsync(carColorId) ?? throw new NotFoundException($"Car color with ID {carColorId} not exist.");

        var mappedColor = _mapper.Map<CarColorResponseDTO>(color);
        return mappedColor;
    }

    public async Task Create(CarColorCreateRequestDTO createColorDTO)
    {
        var isDuplicate = await _context.CarColors.AnyAsync(carColor => carColor.CarColorName == createColorDTO.CarColorName);

        if (isDuplicate.Equals(true))
        {
            throw new DuplicateException($"\"{createColorDTO.CarColorName}\" already exists.");
        }

        var color = _mapper.Map<CarColor>(createColorDTO);
        await _context.CarColors.AddAsync(color);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int carColorId, CarColorUpdateRequestDTO updateColorDTO)
    {
        var carColor = await _context.CarColors.FindAsync(carColorId) ?? throw new NotFoundException($"Car color with ID {carColorId} not exist.");

        carColor.CarColorName = updateColorDTO.CarColorName;

        _context.CarColors.Update(carColor);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int carColorId)
    {
        var carColor = await _context.CarColors.FindAsync(carColorId) ?? throw new NotFoundException($"Car color with ID {carColorId} not exist.");

        _context.CarColors.Remove(carColor);
        await _context.SaveChangesAsync();
    }
}
