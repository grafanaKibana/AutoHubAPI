using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarModelDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModelResponseDTO> GetAll();
        CarModelResponseDTO GetById(int carBrandId);
        void Create(CarModelCreateRequestDTO createModelDTO);
        void Update(int carModelId, CarModelUpdateRequestDTO updateModelDTO);
    }
}