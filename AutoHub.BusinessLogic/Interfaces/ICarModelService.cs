using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarModelService
{
    Task<IEnumerable<CarModelResponseDTO>> GetAll(PaginationParameters paginationParameters);

    Task<CarModelResponseDTO> GetById(int carModelId);

    Task Create(CarModelCreateRequestDTO createModelDTO);

    Task Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO);

    Task Delete(int carModelId);
}
