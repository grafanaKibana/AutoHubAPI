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

    public async Task<IEnumerable<CarModelResponseDTO>> GetAll()
    {
        var models = await _context.CarModels.ToListAsync();
        var mappedModels = _mapper.Map<IEnumerable<CarModelResponseDTO>>(models);
        return mappedModels;
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
