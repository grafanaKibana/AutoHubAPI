using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarBrandDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class CarBrandService : ICarBrandService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarBrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public IEnumerable<CarBrandResponseDTO> GetAll()
        {
            var brands = _unitOfWork.CarBrands.GetAll();
            var mappedBrands = _mapper.Map<IEnumerable<CarBrandResponseDTO>>(brands);
            return mappedBrands;
        }

        public CarBrandResponseDTO GetById(int carBrandId)
        {
            var brand = _unitOfWork.CarBrands.GetById(carBrandId);
            var mappedBrand = _mapper.Map<CarBrandResponseDTO>(brand);
            return mappedBrand;
        }

        public void CreateCarBrand(CarBrandCreateRequestDTO createBrandDTO)
        {
            var brand = _mapper.Map<CarBrand>(createBrandDTO);
            _unitOfWork.CarBrands.Add(brand);
            _unitOfWork.Commit();
        }

        public void UpdateCarBrand(CarBrandUpdateRequestDTO updateBrandDTO)
        {
            var carBrand = _unitOfWork.CarBrands.GetById(updateBrandDTO.CarBrandId);
            carBrand.CarBrandName = updateBrandDTO.CarBrandName;

            _unitOfWork.CarBrands.Update(carBrand);
            _unitOfWork.Commit();
        }
    }
}