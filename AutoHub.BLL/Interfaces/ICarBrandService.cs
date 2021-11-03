using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarBrandDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarBrandService
    {
        IEnumerable<CarBrandResponseDTO> GetAll();
        CarBrandResponseDTO GetById(int carBrandId);
        void CreateCarBrand(CarBrandCreateRequestDTO createBrandDTO);
        void UpdateCarBrand(int carBrandId, CarBrandUpdateRequestDTO updateBrandDTO);
    }
}