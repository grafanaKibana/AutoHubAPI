using AutoHub.BusinessLogic.DTOs.BidDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IBidService
{
    Task<IEnumerable<BidResponseDTO>> GetUserBids(int userId, PaginationParameters paginationParameters);

    Task<IEnumerable<BidResponseDTO>> GetLotBids(int lotId, PaginationParameters paginationParameters);

    Task Create(int lotId, BidCreateRequestDTO createBidDTO);
}
