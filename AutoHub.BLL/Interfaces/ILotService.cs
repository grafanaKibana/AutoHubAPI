using System.Collections.Generic;
using AutoHub.BLL.Models.LotModels;

namespace AutoHub.BLL.Interfaces
{
    public interface ILotService
    {
        IEnumerable<LotResponseModel> GetAll();
        IEnumerable<LotResponseModel> GetActiveLots();
        LotResponseModel GetById(int id);
    }
}