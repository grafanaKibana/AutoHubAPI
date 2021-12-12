using AutoHub.BLL.DTOs.CarModelDTOs;
using System.Collections.Generic;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModelResponseDTO> GetAll();
        CarModelResponseDTO GetById(int carModelId);
        void Create(CarModelCreateRequestDTO createModelDTO);
        void Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO);
        void Delete(int carModelId);
    }
}