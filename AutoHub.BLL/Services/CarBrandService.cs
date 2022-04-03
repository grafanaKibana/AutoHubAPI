using AutoHub.BLL.DTOs.CarBrandDTOs;
using AutoHub.BLL.Exceptions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AutoHub.BLL.Services
{
    public class CarBrandService : ICarBrandService
    {
        private readonly AutoHubContext _context;
        private readonly IMapper _mapper;

        public CarBrandService(AutoHubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CarBrandResponseDTO> GetAll()
        {
            var brands = _context.CarBrands.ToList();
            var mappedBrands = _mapper.Map<IEnumerable<CarBrandResponseDTO>>(brands);
            return mappedBrands;
        }

        public CarBrandResponseDTO GetById(int carBrandId)
        {
            var brand = _context.CarBrands.Find(carBrandId);

            if (brand == null) throw new NotFoundException($"Car brand with ID {carBrandId} not exist.");

            var mappedBrand = _mapper.Map<CarBrandResponseDTO>(brand);
            return mappedBrand;
        }

        public void Create(CarBrandCreateRequestDTO createBrandDTO)
        {
            var isDuplicate = _context.CarBrands.Any(carBrand => carBrand.CarBrandName == createBrandDTO.CarBrandName);

            if (isDuplicate) throw new DublicateException($"\"{createBrandDTO.CarBrandName}\" already exists.");

            var brand = _mapper.Map<CarBrand>(createBrandDTO);
            _context.CarBrands.Add(brand);
            _context.SaveChanges();
        }

        public void Update(int carBrandId, CarBrandUpdateRequestDTO updateBrandDTO)
        {
            var carBrand = _context.CarBrands.Find(carBrandId);

            if (carBrand == null) throw new NotFoundException($"Car brand with ID {carBrandId} not exist.");

            carBrand.CarBrandName = updateBrandDTO.CarBrandName;

            _context.CarBrands.Update(carBrand);
            _context.SaveChanges();
        }

        public void Delete(int carBrandId)
        {
            var carBrand = _context.CarBrands.Find(carBrandId);

            if (carBrand == null) throw new NotFoundException($"Car brand with ID {carBrandId} not exist.");

            _context.CarBrands.Remove(carBrand);
            _context.SaveChanges();
        }
    }
}