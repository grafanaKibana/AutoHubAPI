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

public class BidService(AutoHubContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    : IBidService
{
    public async Task<IEnumerable<BidResponseDTO>> GetUserBids(int userId, PaginationParameters paginationParameters)
    {
        if (await userManager.FindByIdAsync(userId.ToString()) is null)
        {
            throw new NotFoundException($"User with ID {userId} not exist.");
        }

        List<Bid> bids;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.Bids
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
            bids = await context.Bids.Where(bid => bid.BidId < before).Take(limit).ToListAsync();
        }
        else
        {
            bids = await context.Bids.Take(limit).ToListAsync();
        }

        var mappedBids = mapper.Map<IEnumerable<BidResponseDTO>>(bids);
        return mappedBids;
    }

    public async Task<IEnumerable<BidResponseDTO>> GetLotBids(int lotId, PaginationParameters paginationParameters)
    {
        if (await context.Lots.FindAsync(lotId) is null)
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        List<Bid> bids;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
        var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
        var query = context.Bids
            .OrderBy(x => x.BidId)
            .Where(bid => bid.LotId == lotId)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            bids = await query.Where(bid => bid.BidId > after).Take(limit).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            bids = await context.Bids.Where(bid => bid.BidId < before).Take(limit).ToListAsync();
        }
        else
        {
            bids = await context.Bids.Take(limit).ToListAsync();
        }

        var mappedBids = mapper.Map<IEnumerable<BidResponseDTO>>(bids);
        return mappedBids;
    }

    public async Task Create(int lotId, BidCreateRequestDTO createBidDTO)
    {
        if (await context.Lots.FindAsync(lotId) is null)
        {
            throw new NotFoundException($"Lot with ID {lotId} not exist.");
        }

        if (await userManager.FindByIdAsync(createBidDTO.UserId.ToString()) is null)
        {
            throw new NotFoundException($"User with ID {createBidDTO.UserId} not exist.");
        }

        var lotBids = context.Bids.Where(x => x.LotId == lotId); 
        
        if (await lotBids.AnyAsync())
        {
            var biggestLotBid = await lotBids.MaxAsync(x => x.BidValue);

            if (createBidDTO.BidValue < biggestLotBid)
            {
                throw new InvalidValueException($"Bid value: {createBidDTO.BidValue} less than the biggest lot bid: {biggestLotBid}.");
            }
        }
        
        var bid = mapper.Map<Bid>(createBidDTO);
        bid.LotId = lotId;
        bid.BidTime = DateTime.UtcNow;

        await context.Bids.AddAsync(bid);
        await context.SaveChangesAsync();
    }
}
