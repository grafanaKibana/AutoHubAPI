using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarColorDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarColorService
    {
        IEnumerable<CarColorResponseDTO> GetAll();
        CarColorResponseDTO GetById(int carColorId);
        void CreateCarColor(CarColorCreateRequestDTO createColorDTO);
        void UpdateCarColor(int carColorId, CarColorUpdateRequestDTO updateColorDTO);
    }
}