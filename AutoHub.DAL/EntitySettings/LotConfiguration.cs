using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lot").HasKey(lot => lot.LotId);

            builder.Navigation(lot => lot.LotStatus).AutoInclude();

            builder.Property(lot => lot.StartTime).IsRequired();

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
}