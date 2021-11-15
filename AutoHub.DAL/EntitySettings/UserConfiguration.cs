using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User").HasKey(user => user.UserId);

            entity.Property(user => user.FirstName).IsRequired().HasMaxLength(30);
            entity.Property(user => user.LastName).IsRequired().HasMaxLength(30);
            entity.Property(user => user.Email).IsRequired().HasMaxLength(60);
            entity.Property(user => user.Phone).IsRequired().HasMaxLength(24);
            entity.Property(user => user.Password).IsRequired().HasMaxLength(2000);
            entity.Property(user => user.RegistrationTime).IsRequired();
            entity.HasIndex(user => user.Email).IsUnique();
        }
    }
}