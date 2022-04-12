using AutoHub.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace AutoHub.DataAccess.EntitySettings;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = 1,
                    Name = "Customer",
                    NormalizedName = "Customer".Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = 2,
                    Name = "Seller",
                    NormalizedName = "Seller".Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = 3,
                    Name = "Administrator",
                    NormalizedName = "Administrator".Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            });
    }
}
