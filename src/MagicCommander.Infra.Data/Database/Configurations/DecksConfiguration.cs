using System;
using MagicCommander.Domain.Decks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCommander.Infra.Data.Database.Configurations;

public class DecksConfiguration : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        builder
            .ToTable("Deck")
            .HasKey("Id");

        builder
            .Property(d => d.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(d => d.Key)
            .HasColumnName("Key")
            .IsRequired();

        builder
            .Property(d => d.Name)
            .HasColumnName("Name")
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
