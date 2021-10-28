using AutoHub.BLL.DTOs.BidDTOs;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        BidResponseDTO GetById(int bidId);
        void CreateBid(BidCreateRequestDTO createBidDTO);
    }
}