using MagicCommander.Domain.Users.Entites;
using MagicCommander.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagicCommander.Infra.Data.Database.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("User")
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(d => d.Key)
            .HasColumnName("Key")
            .IsRequired();


        builder
            .Property(u => u.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder
            .Property(u => u.Email)
            .HasColumnName("Email")
            .IsRequired();

        builder
            .Property(u => u.Password)
            .HasColumnName("Password")
            .IsRequired();

        builder
            .Property(u => u.Role)
            .HasConversion(new EnumToNumberConverter<TypeRole, int>())
            .HasColumnName("Role")
            .IsRequired();

        builder.OwnsOne(d => d.Audit, audit =>
        {
            audit
                .Property(a => a.CreatedAt)
                .HasColumnName("CreatedAt");

            audit
                .Property(a => a.UpdatedAt)
                .HasColumnName("UpdatedAt");
        });
    }
}
