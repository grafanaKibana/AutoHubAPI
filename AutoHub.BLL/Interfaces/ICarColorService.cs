using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
using System.Collections.Generic;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ICarColorService
{
    IEnumerable<CarColorResponseDTO> GetAll();

    CarColorResponseDTO GetById(int carColorId);

    void Create(CarColorCreateRequestDTO createColorDTO);

    void Update(int carColorId, CarColorUpdateRequestDTO updateColorDTO);

    void Delete(int carColorId);
}
