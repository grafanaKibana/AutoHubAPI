using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Lot> GetActive()
        {
            return _unitOfWork.Lots.GetAll().Where(lot =>
                lot.LotStatusId == LotStatusEnum.InProgress);
        }

        public IEnumerable<Bid> GetBids(int lotId)
        {
            //TODO: Why _unitOfWork.Lots.GetById(lotId).Bids returns [nothing]?
            //TODO: Set-up Including of user, and lot and its members
            return _unitOfWork.Bids.Find(bid => bid.LotId == lotId);
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

        public Lot UpdateLot(Lot lotModel)
        {
            var lot = _unitOfWork.Lots.GetById(lotModel.LotId);
            lot.CreatorId = lotModel.CreatorId;
            lot.LotStatusId = lotModel.LotStatusId;
            lot.CarId = lotModel.CarId;
            lot.WinnerId = lotModel.WinnerId;
            // lot.EndTime = lot.StartTime + lotModel.Duration

            _unitOfWork.Lots.Update(lot);
            _unitOfWork.Commit();
            return lotModel;
        }
    }
}