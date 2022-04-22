using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
using AutoHub.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarColorService
{
    Task<IEnumerable<CarColorResponseDTO>> GetAll(PaginationParameters paginationParameters);

    Task<CarColorResponseDTO> GetById(int carColorId);

    Task Create(CarColorCreateRequestDTO createColorDTO);

    Task Update(int carColorId, CarColorUpdateRequestDTO updateColorDTO);

    Task Delete(int carColorId);
}