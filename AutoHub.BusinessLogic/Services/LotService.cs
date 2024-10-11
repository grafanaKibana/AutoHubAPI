using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.DataAccess;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
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

public class LotService(AutoHubContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    : ILotService
{
    public async Task<IEnumerable<LotResponseDTO>> GetAll(PaginationParameters paginationParameters)
    {
        List<Lot> lots;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.Lots
            .OrderBy(x => x.LotId)
            .Take(limit)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            lots = await query.Where(x => x.LotId > after).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            lots = await query.Where(x => x.LotId < before).ToListAsync();
        }
        else
        {
            lots = await query.ToListAsync();
        }

        var mappedLots = mapper.Map<IEnumerable<LotResponseDTO>>(lots).ToList();

        foreach (var lot in mappedLots)
        {
            var creatorRoles = await userManager.GetRolesAsync(lots.Single(x => x.CreatorId == lot.Creator.UserId).Creator);
            lot.Creator.UserRoles = creatorRoles;

            if (lot.Winner is not null)
            {
                var winnerRoles =
                    await userManager.GetRolesAsync(lots.Single(x => x.WinnerId == lot.Winner.UserId).Winner);
                lot.Winner.UserRoles = winnerRoles;
            }
        }

        return mappedLots;
    }

    public async Task<IEnumerable<LotResponseDTO>> GetRequiredOfDeterminingWinner()
    {
        var lotsToDefineWinner = await context.Lots.Where(x => x.EndTime.HasValue && x.EndTime.Value < DateTime.UtcNow && x.Winner == null).ToListAsync();
        var mappedLots = mapper.Map<IEnumerable<LotResponseDTO>>(lotsToDefineWinner);
        return mappedLots;
    }

    public async Task<IEnumerable<LotResponseDTO>> GetInProgress(PaginationParameters paginationParameters)
    {
        List<Lot> lots;
        var limit = paginationParameters.Limit ?? DefaultPaginationValues.DefaultLimit;
        var query = context.Lots
            .OrderBy(x => x.LotId)
            .Where(x => x.LotStatusId == LotStatusEnum.InProgress)
            .Take(limit)
            .AsQueryable();

        if (paginationParameters.After is not null && paginationParameters.Before is null)
        {
            var after = Convert.ToInt32(Base64Helper.Decode(paginationParameters.After));
            lots = await query.Where(x => x.LotId > after).ToListAsync();
        }
        else if (paginationParameters.After is null && paginationParameters.Before is not null)
        {
            var before = Convert.ToInt32(Base64Helper.Decode(paginationParameters.Before));
            lots = await query.Where(x => x.LotId < before).ToListAsync();
        }
        else
        {
            lots = await query.ToListAsync();
        }

        var mappedLots = mapper.Map<IEnumerable<LotResponseDTO>>(lots);
        return mappedLots;
    }

    public async Task<LotResponseDTO> GetById(int lotId)
    {
        var lot = await context.Lots.FindAsync(lotId) ?? throw new NotFoundException($"Lot with ID {lotId} not exist.");
        
        var mappedLot = mapper.Map<LotResponseDTO>(lot);
        return mappedLot;
    }

    public async Task Create(LotCreateRequestDTO createLotDTO)
    {
        var lot = mapper.Map<Lot>(createLotDTO);

        lot.LotStatusId = LotStatusEnum.New;
        lot.StartTime = DateTime.UtcNow;
        lot.EndTime = lot.StartTime.AddDays(createLotDTO.DurationInDays);

        await context.Lots.AddAsync(lot);
        await context.SaveChangesAsync();
    }

    public async Task Update(int lotId, LotUpdateRequestDTO updateLotDTO)
    {
        if (Enum.IsDefined(typeof(LotStatusEnum), updateLotDTO.LotStatusId.Value).Equals(false))
        {
            throw new EntityValidationException($"Incorrect {nameof(LotStatus.LotStatusId)} value.");
        }

        var lot = await context.Lots.FindAsync(lotId) ?? throw new NotFoundException($"Lot with ID {lotId} not exist.");

        if (updateLotDTO.LotStatusId.HasValue)
        {
            lot.LotStatusId = (LotStatusEnum)updateLotDTO.LotStatusId.Value;
        }
        
        if (updateLotDTO.WinnerId.HasValue)
        {
            lot.Winner = await context.Users.FindAsync(updateLotDTO.WinnerId) ?? throw new NotFoundException($"User with ID {updateLotDTO.WinnerId} not exist.");
        }

        if (updateLotDTO.DurationInDays.HasValue)
        {
            lot.EndTime = lot.StartTime.AddDays(updateLotDTO.DurationInDays.Value);
        }
        
        context.Lots.Update(lot);
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatus(int lotId, int statusId)
    {
        if (Enum.IsDefined(typeof(LotStatusEnum), statusId).Equals(false))
        {
            throw new EntityValidationException($"Incorrect {nameof(LotStatus.LotStatusId)} value.");
        }

        var lot = await context.Lots.FindAsync(lotId) ?? throw new NotFoundException($"Lot with ID {lotId} not exist.");

        lot.LotStatusId = (LotStatusEnum)statusId;

        context.Lots.Update(lot);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int lotId)
    {
        var lot = await context.Lots.FindAsync(lotId) ?? throw new NotFoundException($"Lot with ID {lotId} not exist.");

        context.Lots.Remove(lot);
        await context.SaveChangesAsync();
    }
}
