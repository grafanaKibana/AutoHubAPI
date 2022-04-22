using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AutoHub.DataAccess.EntitySettings;

public class LotStatusConfiguration : IEntityTypeConfiguration<LotStatus>
{
    public void Configure(EntityTypeBuilder<LotStatus> builder)
    {
        builder.ToTable(nameof(LotStatus)).HasKey(status => status.LotStatusId);

        builder.HasData(
            Enum.GetValues(typeof(LotStatusEnum))
                .Cast<LotStatusEnum>()
                .Select(status => new LotStatus
                {
                    LotStatusId = status,
                    LotStatusName = status.ToString()
                }));

        builder.HasMany(status => status.Lots)
            .WithOne(lot => lot.LotStatus)
            .OnDelete(DeleteBehavior.NoAction);
    }
}