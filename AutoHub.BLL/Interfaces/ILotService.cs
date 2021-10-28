using System.Collections.Generic;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.DTOs.LotDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface ILotService
    {
        IEnumerable<LotResponseDTO> GetAll();
        IEnumerable<LotResponseDTO> GetActive();
        IEnumerable<BidResponseDTO> GetBids(int userId);
        LotResponseDTO GetById(int lotId);
        void CreateLot(LotCreateRequestDTO createLotDTO);
        void UpdateLot(LotUpdateRequestDTO updateLotDTO);
    }
}