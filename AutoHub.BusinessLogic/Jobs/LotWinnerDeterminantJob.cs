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
        var lots = await _lotService.GetRequiredOfDeterminingWinner();

        foreach (var lot in lots)
        {
            if (lot.EndTime < DateTime.UtcNow)
            {
                var lotBids = (await _bidService.GetLotBids(lot.LotId, new PaginationParameters
                {
                    Limit = int.MaxValue
                })).ToList();

                if (lotBids.Any())
                {
                    var maxBid = lotBids.OrderBy(x => x.BidValue).Last();

                    await _lotService.Update(lot.LotId, new LotUpdateRequestDTO
                    {
                        WinnerId = maxBid.User.UserId,
                        LotStatusId = (int)LotStatusEnum.EndedUp
                    });
                }
            }
        }
    }
}