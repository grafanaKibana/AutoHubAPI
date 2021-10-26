using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ILotService
    {
        IEnumerable<Lot> GetAll();
        IEnumerable<Lot> GetActive();
        IEnumerable<Bid> GetBids(int userId);
        Lot GetById(int id);
        Lot CreateLot(Lot lotModel);
        Lot UpdateLot(Lot lotModel);
    }
}