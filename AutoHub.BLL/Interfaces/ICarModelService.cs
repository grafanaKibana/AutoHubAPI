using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarModelDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarModelService
    {
        IEnumerable<CarModelResponseDTO> GetAll();
        CarModelResponseDTO GetById(int carBrandId);
        void CreateCarModel(CarModelCreateRequestDTO createModelDTO);
        void UpdateCarModel(CarModelUpdateRequestDTO updateModelDTO);
    }
}