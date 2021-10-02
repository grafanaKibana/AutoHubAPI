using System;
using System.Security.Cryptography;
using System.Text;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
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
            entity.Property(user => user.RegistrationTime).IsRequired().HasDefaultValue(DateTime.UtcNow);
            entity.HasIndex(user => user.Email).IsUnique();

            entity.HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Nikita",
                    LastName = "Reshetnik",
                    Email = "reshetnik.nikita@gmail.com",
                    Phone = "+380698632559",
                    Password = Convert.ToBase64String(HashAlgorithm.Create("sha256")
                        .ComputeHash(Encoding.UTF8.GetBytes("password123"))),
                    RegistrationTime = DateTime.UtcNow,
                    UserRoleId = UserRoleId.Administrator,
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Julia",
                    LastName = "Clifford",
                    Email = "julia.clifford@hotmail.com",
                    Phone = "+380501449999",
                    Password = Convert.ToBase64String(HashAlgorithm.Create("sha256")
                        .ComputeHash(Encoding.UTF8.GetBytes("junkyardistolow"))),
                    RegistrationTime = DateTime.UtcNow,
                    UserRoleId = UserRoleId.Regular
                },
                new User
                {
                    UserId = 3,
                    FirstName = "Elon",
                    LastName = "Musk",
                    Email = "emusk@paypal.com",
                    Phone = "+380991449999",
                    Password = Convert.ToBase64String(HashAlgorithm.Create("sha256")
                        .ComputeHash(Encoding.UTF8.GetBytes("gogotothemars"))),
                    RegistrationTime = DateTime.UtcNow,
                    UserRoleId = UserRoleId.Regular
                });
        }
    }
}