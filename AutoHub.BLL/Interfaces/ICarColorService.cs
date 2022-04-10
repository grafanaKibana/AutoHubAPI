using AutoHub.BLL.DTOs.CarColorDTOs;
using System.Collections.Generic;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarColorService
    {
        IEnumerable<CarColorResponseDTO> GetAll();

        CarColorResponseDTO GetById(int carColorId);

        void Create(CarColorCreateRequestDTO createColorDTO);

        void Update(int carColorId, CarColorUpdateRequestDTO updateColorDTO);

        void Delete(int carColorId);
    }
}