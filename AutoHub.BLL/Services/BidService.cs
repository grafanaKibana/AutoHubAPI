using System;
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

        public BidResponseDTO GetById(int bidId)
        {
            var bid = _unitOfWork.Bids.GetById(bidId);
            var mappedBid = _mapper.Map<BidResponseDTO>(bid);
            return mappedBid;
        }

        public void CreateBid(BidCreateRequestDTO createBidDTO)
        {
            var newBid = _mapper.Map<Bid>(createBidDTO);
            newBid.BidTime = DateTime.UtcNow;

            _unitOfWork.Bids.Add(newBid);
            _unitOfWork.Commit();
        }
    }
}