using System.Collections.Generic;
using AutoHub.BLL.Models.BidModels;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        IEnumerable<BidViewModel> GetAllUserBids(int userId);
        IEnumerable<BidViewModel> GetAllLotBids(int lotId);
        BidViewModel GetById(int id);
        BidCreateApiModel CreateBid(BidCreateApiModel bidModel);
    }
}