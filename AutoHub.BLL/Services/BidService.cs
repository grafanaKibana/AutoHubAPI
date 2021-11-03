using System;
using System.Collections.Generic;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Interfaces;
using AutoMapper;

namespace AutoHub.BLL.Services
{
    public class BidService : IBidService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BidService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<BidResponseDTO> GetUserBids(int userId)
        {
            var bids = _unitOfWork.Bids.Include(bid => bid.UserId == userId, bid => bid.Lot, bid => bid.User);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
            return mappedBids;
        }

        public IEnumerable<BidResponseDTO> GetLotBids(int lotId)
        {
            var bids = _unitOfWork.Bids.Include(bid => bid.LotId == lotId, bid => bid.Lot, bid => bid.User);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
            return mappedBids;
        }

        public void Create(BidCreateRequestDTO createBidDTO)
        {
            var newBid = _mapper.Map<Bid>(createBidDTO);
            newBid.BidTime = DateTime.UtcNow;

            _unitOfWork.Bids.Add(newBid);
            _unitOfWork.Commit();
        }
    }
}