using AutoHub.BLL.DTOs.BidDTOs;
using System.Collections.Generic;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        IEnumerable<BidResponseDTO> GetUserBids(int userId);

        IEnumerable<BidResponseDTO> GetLotBids(int lotId);

        void Create(int lotId, BidCreateRequestDTO createBidDTO);
    }
}