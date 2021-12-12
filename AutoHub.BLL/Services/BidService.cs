using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Exceptions;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoHub.BLL.Services
{
    public class BidService : IBidService
    {
        private readonly AutoHubContext _context;
        private readonly IMapper _mapper;

        public BidService(AutoHubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<BidResponseDTO> GetUserBids(int userId)
        {
            var userExist = _context.Users.Any(user => user.UserId == userId);

            if (!userExist) throw new NotFoundException($"User with ID {userId} not exist");

            var bids = _context.Bids
                .Include(bid => bid.User.UserRole)
                .Include(bid => bid.Lot.Creator.UserRole)
                .Include(bid => bid.Lot.Car.CarBrand)
                .Include(bid => bid.Lot.Car.CarModel)
                .Include(bid => bid.Lot.Car.CarColor)
                .Include(bid => bid.Lot.Car.CarStatus)
                .Include(bid => bid.Lot.Winner.UserRole)
                .Include(bid => bid.Lot.LotStatus)
                .Where(bid => bid.UserId == userId)
                .ToList();

            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
            return mappedBids;
        }

        public IEnumerable<BidResponseDTO> GetLotBids(int lotId)
        {
            var lotExist = _context.Lots.Any(lot => lot.LotId == lotId);

            if (!lotExist) throw new NotFoundException($"Lot with ID {lotId} not exist");

            var bids = _context.Bids
                .Include(bid => bid.User.UserRole)
                .Include(bid => bid.Lot.Creator.UserRole)
                .Include(bid => bid.Lot.Car.CarBrand)
                .Include(bid => bid.Lot.Car.CarModel)
                .Include(bid => bid.Lot.Car.CarColor)
                .Include(bid => bid.Lot.Car.CarStatus)
                .Include(bid => bid.Lot.Winner.UserRole)
                .Include(bid => bid.Lot.LotStatus)
                .Where(bid => bid.LotId == lotId)
                .ToList();

            var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
            return mappedBids;
        }

        public void Create(int lotId, BidCreateRequestDTO createBidDTO)
        {
            var lotExist = _context.Lots.Any(lot => lot.LotId == lotId);

            if (!lotExist) throw new NotFoundException($"Lot with ID {lotId} not exist");

            var bid = _mapper.Map<Bid>(createBidDTO);
            bid.LotId = lotId;
            bid.BidTime = DateTime.UtcNow;

            _context.Bids.Add(bid);
            _context.SaveChanges();
        }
    }
}