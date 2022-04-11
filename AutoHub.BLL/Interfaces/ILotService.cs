using AutoHub.BusinessLogic.DTOs.LotDTOs;
using System.Collections.Generic;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ILotService
{
    IEnumerable<LotResponseDTO> GetAll();

    IEnumerable<LotResponseDTO> GetInProgress();

    LotResponseDTO GetById(int lotId);

    void Create(LotCreateRequestDTO createLotDTO);

    void Update(int lotId, LotUpdateRequestDTO updateLotDTO);

    void UpdateStatus(int lotId, int statusId);

    void Delete(int lotId);
}
