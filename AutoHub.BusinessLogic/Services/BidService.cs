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
using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.Models;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Org.BouncyCastle.Utilities.Encoders;

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

    public async Task<IEnumerable<BidResponseDTO>> GetUserBids(int userId, PaginationParameters paginationParameters)
    {
        var userExist = await _context.Users.AnyAsync(user => user.Id == userId);

        if (userExist.Equals(false))
        {
            throw new NotFoundException($"User with ID {userId} not exist.");
        }

        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
        var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
        var query = _context.Bids
            .Include(bid => bid.Lot.Car.CarBrand)
            .Include(bid => bid.Lot.Car.CarModel)
            .Include(bid => bid.Lot.Car.CarColor)
            .Include(bid => bid.Lot.Car.CarStatus)
            .Include(bid => bid.Lot.LotStatus)
            .OrderBy(x => x.BidId)
            .Where(bid => bid.UserId == userId)
            .AsQueryable();
        List<Bid> bids;

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            bids = await query.Where(bid => bid.BidId > after).Take(limit).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            bids = await _context.Bids.Where(bid => bid.BidId < before).Take(limit).ToListAsync();
        }
        else
        {
            bids = await _context.Bids.Take(limit).ToListAsync();
        }

        var mappedBids = _mapper.Map<IEnumerable<BidResponseDTO>>(bids);
        return mappedBids;
    }

    public async Task<IEnumerable<BidResponseDTO>> GetLotBids(int lotId, PaginationParameters paginationParameters)
    {
        var lotExist = await _context.Lots.AnyAsync(lot => lot.LotId == lotId);

        if (lotExist.Equals(false))
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
        var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
        var query = _context.Bids
            .Include(bid => bid.Lot.Car.CarBrand)
            .Include(bid => bid.Lot.Car.CarModel)
            .Include(bid => bid.Lot.Car.CarColor)
            .Include(bid => bid.Lot.Car.CarStatus)
            .Include(bid => bid.Lot.LotStatus)
            .OrderBy(x => x.BidId)
            .Where(bid => bid.LotId == lotId)
            .AsQueryable();
        List<Bid> bids;

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            bids = await query.Where(bid => bid.BidId > after).Take(limit).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            bids = await _context.Bids.Where(bid => bid.BidId < before).Take(limit).ToListAsync();
        }
        else
        {
            bids = await _context.Bids.Take(limit).ToListAsync();
        }

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
