using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Lot> GetAll()
        {
            return _unitOfWork.Lots.GetAll();
        }

        public IEnumerable<Lot> GetActiveLots()
        {
            return _unitOfWork.Lots.Find(lot =>
                lot.LotStatusId == LotStatusEnum.InProgress);
        }

        public Lot GetById(int id)
        {
            return _unitOfWork.Lots.GetById(id);
        }

        public Lot CreateLot(Lot lotModel)
        {
            _unitOfWork.Lots.Add(lotModel);
            _unitOfWork.Commit();
            return lotModel;
        }
    }
}