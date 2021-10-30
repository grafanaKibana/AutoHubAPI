using System.Collections.Generic;
using AutoHub.BLL.DTOs.BidDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        BidResponseDTO GetById(int bidId);
        IEnumerable<BidResponseDTO> GetUserBids(int userId);
        BidResponseDTO GetUserBidById(int userId, int bidId);
        IEnumerable<BidResponseDTO> GetLotBids(int lotId);
        BidResponseDTO GetLotBidById(int lotId, int bidId);
        void CreateBid(BidCreateRequestDTO createBidDTO);
    }
}