using AutoHub.BusinessLogic.DTOs.BidDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IBidService
{
    Task<IEnumerable<BidResponseDTO>> GetUserBids(int userId);

    Task<IEnumerable<BidResponseDTO>> GetLotBids(int lotId);

    Task Create(int lotId, BidCreateRequestDTO createBidDTO);
}
