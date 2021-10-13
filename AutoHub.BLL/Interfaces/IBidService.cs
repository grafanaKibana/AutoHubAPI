using System.Collections.Generic;
using AutoHub.BLL.Models.BidModels;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        IEnumerable<BidResponseModel> GetAllUserBids(int userId);
        IEnumerable<BidResponseModel> GetAllLotBids(int lotId);
        BidResponseModel GetById(int id);
        BidCreateRequestModel CreateBid(BidCreateRequestModel bidModel);
    }
}