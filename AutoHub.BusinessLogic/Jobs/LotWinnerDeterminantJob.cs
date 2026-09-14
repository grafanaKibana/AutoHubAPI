using System;
using System.Linq;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Enums;
using Quartz;

namespace AutoHub.BusinessLogic.Jobs;

using System.Threading;

public class LotWinnerDeterminantJob(ILotService lotService, IBidService bidService) : IJob
{
    private readonly ILotService lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
    private readonly IBidService bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("JobTriggered");

        var lotIdsToDeterminate = (await lotService.GetRequiredOfDeterminingWinner())
            .Select(lot => lot.LotId)
            .ToList();

        await Parallel.ForEachAsync(lotIdsToDeterminate, cancellationToken: CancellationToken.None, async (lotId, _) =>
        {
            var lotBids = (await bidService.GetLotBids(lotId, new PaginationParameters(int.MaxValue))).ToList();
            if (lotBids.Count == 0)
            {
                return;
            }

            var maxBid = lotBids.MaxBy(x => x.BidValue);

            await lotService.Update(lotId, new LotUpdateRequestDTO
            {
                WinnerId = maxBid.User.UserId,
                LotStatusId = (int)LotStatusEnum.EndedUp
            });
        });
    }
}