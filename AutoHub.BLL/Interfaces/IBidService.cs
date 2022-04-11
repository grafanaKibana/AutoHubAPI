using AutoHub.BusinessLogic.DTOs.BidDTOs;
using System.Collections.Generic;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IBidService
{
    IEnumerable<BidResponseDTO> GetUserBids(int userId);

    IEnumerable<BidResponseDTO> GetLotBids(int lotId);

    void Create(int lotId, BidCreateRequestDTO createBidDTO);
}
