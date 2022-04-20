using AutoHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DataAccess.EntitySettings;

public class LotConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.ToTable(nameof(Lot)).HasKey(lot => lot.LotId);

        builder.Property(lot => lot.StartTime).IsRequired();
        builder.Property(lot => lot.WinnerId).IsRequired(false);

        builder.HasOne(lot => lot.Creator)
            .WithMany(user => user.UserLots)
            .HasForeignKey(lot => lot.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(lot => lot.Winner)
            .WithMany(user => user.VictoryLots)
            .HasForeignKey(lot => lot.WinnerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
