using System.Collections.Generic;
using AutoHub.BLL.Models.LotModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ILotService
    {
        IEnumerable<LotModel> GetAll();
        IEnumerable<LotModel> GetActiveLots();
        LotModel GetById(int id);
    }
}