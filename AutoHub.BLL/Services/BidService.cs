using AutoHub.BusinessLogic.DTOs.BidDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.DataAccess;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Services;

public class BidService : IBidService
{
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;

    public BidService(AutoHubContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BidResponseDTO>> GetUserBids(int userId)
    {
        var userExist = await _context.Users.AnyAsync(user => user.Id == userId);

        if (userExist.Equals(false))
        {
            throw new NotFoundException($"User with ID {userId} not exist.");
        }

        var bids = await _context.Bids
            .Include(bid => bid.Lot.Car.CarBrand)
            .Include(bid => bid.Lot.Car.CarModel)
            .Include(bid => bid.Lot.Car.CarColor)
            .Include(bid => bid.Lot.Car.CarStatus)
            .Include(bid => bid.Lot.LotStatus)
            .Where(bid => bid.UserId == userId)
            .ToListAsync();

        var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
        return mappedBids;
    }

    public async Task<IEnumerable<BidResponseDTO>> GetLotBids(int lotId)
    {
        var lotExist = await _context.Lots.AnyAsync(lot => lot.LotId == lotId);

        if (lotExist.Equals(false))
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        var bids = await _context.Bids
            .Include(bid => bid.Lot.Car.CarBrand)
            .Include(bid => bid.Lot.Car.CarModel)
            .Include(bid => bid.Lot.Car.CarColor)
            .Include(bid => bid.Lot.Car.CarStatus)
            .Include(bid => bid.Lot.LotStatus)
            .Where(bid => bid.LotId == lotId)
            .ToListAsync();

        var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
        return mappedBids;
    }

    public async Task Create(int lotId, BidCreateRequestDTO createBidDTO)
    {
        var lotExist = await _context.Lots.AnyAsync(lot => lot.LotId == lotId);

        if (lotExist.Equals(false))
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        var userExist = await _context.Users.AnyAsync(user => user.Id == createBidDTO.UserId);

        if (userExist.Equals(false))
        {
            throw new NotFoundException($"User with ID {createBidDTO.UserId} not exist.");
        }

        var bid = _mapper.Map<Bid>(createBidDTO);
        bid.LotId = lotId;
        bid.BidTime = DateTime.UtcNow;

        await _context.Bids.AddAsync(bid);
        await _context.SaveChangesAsync();
    }
}
