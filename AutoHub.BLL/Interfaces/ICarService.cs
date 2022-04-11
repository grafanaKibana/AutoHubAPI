using AutoHub.BusinessLogic.DTOs.CarDTOs;
using System.Collections.Generic;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarService
{
    IEnumerable<CarResponseDTO> GetAll();

    CarResponseDTO GetById(int carId);

    void Create(CarCreateRequestDTO createCarDTO);

    void Update(int carId, CarUpdateRequestDTO updateCarDTO);

    void UpdateStatus(int carId, int statusId);

    void Delete(int carId);
}
