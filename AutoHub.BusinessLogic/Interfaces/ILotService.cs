using AutoHub.BusinessLogic.DTOs.LotDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface ILotService
{
    Task<IEnumerable<LotResponseDTO>> GetAll();

    Task<IEnumerable<LotResponseDTO>> GetInProgress();

    Task<LotResponseDTO> GetById(int lotId);

    Task Create(LotCreateRequestDTO createLotDTO);

    Task Update(int lotId, LotUpdateRequestDTO updateLotDTO);

    Task UpdateStatus(int lotId, int statusId);

    Task Delete(int lotId);
}
