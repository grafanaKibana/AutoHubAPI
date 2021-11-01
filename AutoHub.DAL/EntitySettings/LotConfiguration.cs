using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class LotConfiguration
    {
        public LotConfiguration(EntityTypeBuilder<Lot> entity)
        {
            entity.ToTable("Lot").HasKey(lot => lot.LotId);

            entity.Property(lot => lot.StartTime).IsRequired();

            entity.HasOne(lot => lot.Creator)
                .WithMany(user => user.UserLots)
                .HasForeignKey(lot => lot.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(lot => lot.Winner)
                .WithMany(user => user.VictoryLots)
                .HasForeignKey(lot => lot.WinnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}