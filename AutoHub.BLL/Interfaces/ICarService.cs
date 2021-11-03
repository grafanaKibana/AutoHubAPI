using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarResponseDTO> GetAll();
        CarResponseDTO GetById(int carId);
        void Create(CarCreateRequestDTO createCarDTO);
        void Update(int carId, CarUpdateRequestDTO updateCarDTO);
    }
}