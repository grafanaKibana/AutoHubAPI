using System;
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

        public bool SetStatus(int lotId, int statusId)
        {
            if (!_unitOfWork.Lots.Any(lot => lot.LotId == lotId))
                return false;

            if (!Enum.IsDefined(typeof(LotStatusEnum), statusId))
                return false;

            var lotToUpdate = _unitOfWork.Lots.Find(lot => lot.LotId == lotId).FirstOrDefault();
            lotToUpdate.LotStatusId = (LotStatusEnum)statusId;
            _unitOfWork.Lots.Update(lotToUpdate);
            _unitOfWork.Commit();
            return true;

            /*switch (statusId)
            {
                case (int)LotStatusEnum.New:
                    lotToUpdate.LotStatusId = LotStatusEnum.New;
                    _unitOfWork.Lots.Update(lotToUpdate);
                    _unitOfWork.Commit();
                    return true;

                case (int)LotStatusEnum.NotStarted:
                    lotToUpdate.LotStatusId = LotStatusEnum.NotStarted;
                    _unitOfWork.Lots.Update(lotToUpdate);
                    _unitOfWork.Commit();
                    return true;

                case (int)LotStatusEnum.InProgress:
                    lotToUpdate.LotStatusId = LotStatusEnum.InProgress;
                    _unitOfWork.Lots.Update(lotToUpdate);
                    _unitOfWork.Commit();
                    return true;

                case (int)LotStatusEnum.EndedUp:
                    lotToUpdate.LotStatusId = LotStatusEnum.EndedUp;
                    _unitOfWork.Lots.Update(lotToUpdate);
                    _unitOfWork.Commit();
                    return true;

                default:
                    return false;
            }

            return false;*/
        }

        public bool SetWinner(int lotId, int userId)
        {
            if (!_unitOfWork.Lots.Any(lot => lot.LotId == lotId) &&
                !_unitOfWork.Users.Any(user => user.UserId == userId))
                return false;

            var lotToUpdate = _unitOfWork.Lots.Find(lot => lot.LotId == lotId).FirstOrDefault();
            lotToUpdate.WinnerId = userId;
            _unitOfWork.Lots.Update(lotToUpdate);
            _unitOfWork.Commit();
            return true;
        }
    }
}