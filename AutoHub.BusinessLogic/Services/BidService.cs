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
using AutoHub.Domain.Constants;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AutoHub.BusinessLogic.Services;

public class BidService : IBidService
{
    private readonly AutoHubContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public BidService(AutoHubContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IEnumerable<BidResponseDTO>> GetUserBids(int userId, PaginationParameters paginationParameters)
    {
        if (await _userManager.FindByIdAsync(userId.ToString()) is null)
        {
            throw new NotFoundException($"User with ID {userId} not exist.");
        }

        List<Bid> bids;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = _context.Bids
            .OrderBy(x => x.BidId)
            .Where(bid => bid.UserId == userId)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            bids = await query.Where(bid => bid.BidId > after).Take(limit).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
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
        if (await _context.Lots.FindAsync(lotId) is null)
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        List<Bid> bids;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
        var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
        var query = _context.Bids
            .OrderBy(x => x.BidId)
            .Where(bid => bid.LotId == lotId)
            .AsQueryable();

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
        if (await _context.Lots.FindAsync(lotId) is null)
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        if (await _userManager.FindByIdAsync(createBidDTO.UserId.ToString()) is null)
        {
            throw new NotFoundException($"User with ID {createBidDTO.UserId} not exist.");
        }

        var biggestLotBid = _context.Bids.OrderBy(x => x.BidValue).Last().BidValue;

        if (createBidDTO.BidValue < biggestLotBid)
        {
            throw new InvalidValueException($"Bid value: {createBidDTO.BidValue} less than the biggest lot bid: {biggestLotBid}.");
        }
        
        var bid = _mapper.Map<Bid>(createBidDTO);
        bid.LotId = lotId;
        bid.BidTime = DateTime.UtcNow;

        await _context.Bids.AddAsync(bid);
        await _context.SaveChangesAsync();
    }
}
