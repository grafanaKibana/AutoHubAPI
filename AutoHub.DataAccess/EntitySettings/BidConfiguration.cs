using AutoHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DataAccess.EntitySettings;

public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.ToTable(nameof(Bid)).HasKey(bid => bid.BidId);

        builder.HasOne(bid => bid.User)
            .WithMany(user => user.UserBids)
            .HasForeignKey(bid => bid.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(bid => bid.Lot)
            .WithMany(lot => lot.Bids)
            .HasForeignKey(bid => bid.LotId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}