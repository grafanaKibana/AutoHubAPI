using System;
using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;

namespace AutoHub.BLL.Services
{
    public class BidService : IBidService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BidService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Bid> GetAllUserBids(int userId)
        {
            return _unitOfWork.Bids.GetAll().Where(bid => bid.UserId == userId);
        }

        public IEnumerable<Bid> GetAllLotBids(int lotId)
        {
            return _unitOfWork.Bids.GetAll().Where(bid => bid.LotId == lotId);
        }

        public Bid GetById(int id)
        {
            var bid = _unitOfWork.Bids.GetById(id);

            return bid ?? null;
        }

        public Bid CreateBid(Bid bidModel)
        {
            _unitOfWork.Bids.Add(new Bid
            {
                UserId = bidModel.UserId,
                LotId = bidModel.UserId,
                BidValue = bidModel.BidValue,
                BidTime = DateTime.UtcNow
            });

            _unitOfWork.Commit();
            return bidModel;
        }
    }
}