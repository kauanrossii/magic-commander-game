using MagicCommander.Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagicCommander.Infra.Data.Database.Configurations;

public class CardsConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder
            .ToTable("Card")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(c => c.Key)
            .HasColumnName("Key")
            .IsRequired();

        builder
            .Property(c => c.MultiverseId)
            .HasColumnName("MultiverseId");

        builder
            .Property(c => c.Cmc)
            .HasColumnName("Cmc");

        builder
            .Property(c => c.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder
            .Property(c => c.Type)
            .HasColumnName("Type")
            .IsRequired();

        builder
            .Property(c => c.ManaCost)
            .HasColumnName("ManaCost")
            .IsRequired();

        builder
            .Property(c => c.Set)
            .HasColumnName("Set")
            .IsRequired();

        builder
            .Property(c => c.Rarity)
            .HasConversion(new EnumToNumberConverter<TypeRarity, int>())
            .HasColumnName("Rarity")
            .IsRequired();

        builder
            .Property(c => c.Text)
            .HasColumnName("Text")
            .IsRequired();

        builder
            .Property(c => c.Artist)
            .HasColumnName("Artist")
            .IsRequired();

        builder
            .Property(c => c.Number)
            .HasColumnName("Number")
            .IsRequired();

        builder
            .Property(c => c.Power)
            .HasColumnName("Power")
            .IsRequired();

        builder
            .Property(c => c.Toughness)
            .HasColumnName("Toughness")
            .IsRequired();

        builder
            .Property(c => c.Layout)
            .HasColumnName("Layout")
            .IsRequired();

        builder
            .Property(c => c.ImageUrl)
            .HasColumnName("ImageUrl")
            .IsRequired();

        builder
            .Property(c => c.ExternalId)
            .HasColumnName("ExternalId")
            .IsRequired();

        builder
            .Property(c => c.Colors)
            .HasColumnName("Colors")
            .IsRequired();

        builder
            .Property(c => c.Supertypes)
            .HasConversion(
                c => string.Join(",", c.Select(e => e.ToString().ToArray())),
                c => c.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => (SupertypeCard)int.Parse(e))
                    .ToList()
            )
            .HasColumnName("Supertypes")
            .IsRequired();

        builder
            .Property(c => c.Types)
            .HasConversion(
                c => string.Join(",", c.Select(e => e.ToString().ToArray())),
                c => c.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => (TypeCard)int.Parse(e))
                    .ToList()
            )
            .HasColumnName("Types")
            .IsRequired();

        builder
            .Property(c => c.Subtypes)
            .HasColumnName("Subtypes")
            .IsRequired();

        builder
            .OwnsMany(c => c.Rulings, rulings =>
            {
                rulings
                    .ToTable("Ruling")
                    .HasKey(r => r.Id);

                rulings
                    .Property(r => r.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                rulings
                    .Property(r => r.Key)
                    .HasColumnName("Key")
                    .IsRequired();

                rulings
                    .Property(r => r.Text)
                    .HasColumnName("Text")
                    .IsRequired();

                rulings
                    .Property(r => r.Date)
                    .HasColumnName("Date")
                    .IsRequired();
            });

        builder
            .OwnsOne(c => c.Audit, audit =>
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
