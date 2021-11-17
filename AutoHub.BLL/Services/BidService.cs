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
            var bids = _unitOfWork.Bids.GetAll(bid => bid.UserId == userId,
                bid => bid.User, bid => bid.Lot, bid => bid.Lot.Creator, bid => bid.Lot.Car, bid => bid.Lot.Winner);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
            return mappedBids;
        }

        public IEnumerable<BidResponseDTO> GetLotBids(int lotId)
        {
            var bids = _unitOfWork.Bids.GetAll(bid => bid.LotId == lotId,
                bid => bid.User, bid => bid.Lot, bid => bid.Lot.Creator, bid => bid.Lot.Car, bid => bid.Lot.Winner);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
            return mappedBids;
        }

        public void Create(int lotId, BidCreateRequestDTO createBidDTO)
        {
            var lot = _context.Lots.GetById(lotId);

            if (lot == null)
                throw new NotFoundException($"Lot with ID {lotId} not exist");

            var bid = _mapper.Map<Bid>(createBidDTO);
            bid.LotId = lotId;
            bid.BidTime = DateTime.UtcNow;

            _unitOfWork.Bids.Add(bid);
            _unitOfWork.Commit();
        }
    }
}