using System;
using System.Linq;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Enums;
using Quartz;

namespace AutoHub.BusinessLogic.Jobs;

public class LotWinnerDeterminantJob : IJob
{
    private readonly ILotService _lotService;
    private readonly IBidService _bidService;

    public LotWinnerDeterminantJob(ILotService lotService, IBidService bidService)
    {
        _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
        _bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("JobTriggered");
        var lotIdsToDeterminate = (await _lotService.GetRequiredOfDeterminingWinner())
            .Where(lot => lot.EndTime < DateTime.UtcNow)
            .Select(lot => lot.LotId);

        foreach (var lotId in lotIdsToDeterminate)
        {
            var lotBids = (await _bidService.GetLotBids(lotId, new PaginationParameters(int.MaxValue))).ToList();
            if (lotBids.Any())
            {
                var maxBid = lotBids.MaxBy(x => x.BidValue);

                await _lotService.Update(lotId, new LotUpdateRequestDTO
                {
                    WinnerId = maxBid.User.UserId,
                    LotStatusId = (int)LotStatusEnum.EndedUp
                });
            }
        }
    }
}