using System.Collections.Generic;
using AutoHub.BLL.DTOs.BidDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        IEnumerable<BidResponseDTO> GetUserBids(int userId);
        IEnumerable<BidResponseDTO> GetLotBids(int lotId);
        void Create(int lotId, BidCreateRequestDTO createBidDTO);
    }
}