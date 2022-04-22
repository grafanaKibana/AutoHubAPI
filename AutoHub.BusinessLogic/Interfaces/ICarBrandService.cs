using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
using AutoHub.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarBrandService
{
    Task<IEnumerable<CarBrandResponseDTO>> GetAll(PaginationParameters paginationParameters);

    Task<CarBrandResponseDTO> GetById(int carBrandId);

    Task Create(CarBrandCreateRequestDTO createBrandDTO);

    Task Update(int carBrandId, CarBrandUpdateRequestDTO updateBrandDTO);

    Task Delete(int carBrandId);
}