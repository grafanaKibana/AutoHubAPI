using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
using System.Collections.Generic;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarBrandService
{
    IEnumerable<CarBrandResponseDTO> GetAll();

    CarBrandResponseDTO GetById(int carBrandId);

    void Create(CarBrandCreateRequestDTO createBrandDTO);

    void Update(int carBrandId, CarBrandUpdateRequestDTO updateBrandDTO);

    void Delete(int carBrandId);
}
