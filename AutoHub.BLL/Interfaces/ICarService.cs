using System.Collections.Generic;
using AutoHub.BLL.DTOs.CarDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarResponseDTO> GetAll();
        CarResponseDTO GetById(int carId);
        void CreateCar(CarCreateRequestDTO createCarDTO);
        void UpdateCar(int carId, CarUpdateRequestDTO updateCarDTO);
    }
}