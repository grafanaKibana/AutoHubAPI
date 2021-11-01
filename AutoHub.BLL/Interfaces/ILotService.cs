using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface ILotService
    {
        IEnumerable<Lot> GetAll();
        IEnumerable<Lot> GetActiveLots();
        Lot GetById(int id);
        Lot CreateLot(Lot lotModel);
    }
}