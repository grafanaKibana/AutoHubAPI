using AutoHub.BLL.DTOs.CarModelDTOs;
using AutoHub.BLL.Exceptions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AutoHub.BLL.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly AutoHubContext _context;
        private readonly IMapper _mapper;

        public CarModelService(AutoHubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CarModelResponseDTO> GetAll()
        {
            var models = _context.CarModels.ToList();
            var mappedModels = _mapper.Map<IEnumerable<CarModelResponseDTO>>(models);
            return mappedModels;
        }

        public CarModelResponseDTO GetById(int carModelId)
        {
            var model = _context.CarModels.Find(carModelId);

            if (model == null) throw new NotFoundException($"Car model with ID {carModelId} not exist.");

            var mappedModels = _mapper.Map<CarModelResponseDTO>(model);
            return mappedModels;
        }

        public void Create(CarModelCreateRequestDTO createModelDTO)
        {
            var isDuplicate = _context.CarModels.Any(carModel => carModel.CarModelName == createModelDTO.CarModelName);

            if (isDuplicate) throw new DublicateException($"\"{createModelDTO.CarModelName}\" already exists.");

            var model = _mapper.Map<CarModel>(createModelDTO);
            _context.CarModels.Add(model);
            _context.SaveChanges();
        }

        public void Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO)
        {
            var carModel = _context.CarModels.Find(carModelId);

            if (carModel == null) throw new NotFoundException($"Car model with ID {carModelId} not exist.");

            carModel.CarModelName = updateModelDTO.CarModelName;

            _context.CarModels.Update(carModel);
            _context.SaveChanges();
        }

        public void Delete(int carModelId)
        {
            var carModel = _context.CarModels.Find(carModelId);

            if (carModel == null) throw new NotFoundException($"Car model with ID {carModelId} not exist.");

            _context.CarModels.Remove(carModel);
            _context.SaveChanges();
        }
    }
}