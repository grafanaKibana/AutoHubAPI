using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AutoHub.DAL.EntitySettings
{
    public class LotStatusConfiguration : IEntityTypeConfiguration<LotStatus>
    {
        public void Configure(EntityTypeBuilder<LotStatus> builder)
        {
            builder.ToTable("LotStatus").HasKey(status => status.LotStatusId);

            builder.HasData(
                Enum.GetValues(typeof(LotStatusEnum))
                    .Cast<LotStatusEnum>()
                    .Select(s => new LotStatus
                    {
                        LotStatusId = s,
                        LotStatusName = s.ToString()
                    }));

            builder.HasMany(status => status.Lots)
                .WithOne(lot => lot.LotStatus)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}