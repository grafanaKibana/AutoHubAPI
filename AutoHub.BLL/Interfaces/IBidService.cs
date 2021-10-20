using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface IBidService
    {
        IEnumerable<Bid> GetAllUserBids(int userId);
        IEnumerable<Bid> GetAllLotBids(int lotId);
        Bid GetById(int id);
        Bid CreateBid(Bid bidModel);
    }
}