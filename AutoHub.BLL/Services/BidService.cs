using System;
using System.Collections.Generic;
using System.Linq;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.BidModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class BidService : IBidService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BidService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Bid, BidResponseModel>());
            _mapper = new Mapper(mapperConfig);
        }

        public IEnumerable<BidResponseModel> GetAllUserBids(int userId)
        {
            return _mapper.Map<IEnumerable<BidResponseModel>>(_unitOfWork.Bids
                .GetAll()
                .Where(bid => bid.UserId == userId));
        }

        public IEnumerable<BidResponseModel> GetAllLotBids(int lotId)
        {
            return _mapper.Map<IEnumerable<BidResponseModel>>(_unitOfWork.Bids
                .GetAll()
                .Where(bid => bid.LotId == lotId));
        }

        public BidResponseModel GetById(int id)
        {
            var bid = _unitOfWork.Bids.GetById(id);

            if (bid == null)
                return null;

            return _mapper.Map<BidResponseModel>(bid);
        }

        public BidCreateRequestModel CreateBid(BidCreateRequestModel bidModel)
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