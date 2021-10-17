using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class BidConfiguration
    {
        public BidConfiguration(EntityTypeBuilder<Bid> entity)
        {
            entity.ToTable("Bid").HasKey(bid => bid.BidId);

            entity.HasOne(bid => bid.User)
                .WithMany(user => user.UserBids)
                .HasForeignKey(bid => bid.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(bid => bid.Lot)
                .WithMany(lot => lot.Bids)
                .HasForeignKey(bid => bid.LotId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}