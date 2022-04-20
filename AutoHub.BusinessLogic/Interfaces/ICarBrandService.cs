using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarBrandService
{
    Task<IEnumerable<CarBrandResponseDTO>> GetAll(PaginationParameters paginationParameters);

    Task<CarBrandResponseDTO> GetById(int carBrandId);

    Task Create(CarBrandCreateRequestDTO createBrandDTO);

    Task Update(int carBrandId, CarBrandUpdateRequestDTO updateBrandDTO);

    Task Delete(int carBrandId);
}
