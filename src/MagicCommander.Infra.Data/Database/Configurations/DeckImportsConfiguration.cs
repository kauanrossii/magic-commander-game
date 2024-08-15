using MagicCommander.Domain.DecksImports.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagicCommander.Infra.Data.Database.Configurations;

public class DeckImportsConfiguration : IEntityTypeConfiguration<DeckImport>
{
    public void Configure(EntityTypeBuilder<DeckImport> builder)
    {
        builder
            .ToTable("DeckImports")
            .HasKey(di => di.Id);

        builder
            .Property(di => di.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(di => di.Key)
            .HasColumnName("Key")
            .IsRequired();

        builder
            .Property(di => di.UserId)
            .HasColumnName("UserId")
            .IsRequired();

        builder.OwnsMany(di => di.Status, status =>
        {
            status
                .ToTable("DeckImportStatus")
                .HasKey("Id");

            status
                .Property(st => st.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            status
                .Property(st => st.Key)
                .HasColumnName("Key")
                .IsRequired();

            status
                .Property(st => st.Current)
                .HasColumnName("Current")
                .IsRequired();

            status
                .Property(st => st.Type)
                .HasColumnName("Type")
                .HasConversion(new EnumToNumberConverter<TypeStatusDeckImport, int>())
                .IsRequired();

            status
                .Property(st => st.Observation)
                .HasColumnName("Observation");

            status.OwnsOne(st => st.Audit, audit =>
            {
                audit
                    .Property(csa => csa.CreatedAt)
                    .HasColumnName("CreatedAt");

                audit
                    .Property(csa => csa.UpdatedAt)
                    .HasColumnName("UpdatedAt");
            });
        });

        builder.OwnsMany(di => di.Cards, card =>
        {
            card
                .ToTable("CardImport")
                .HasKey("Id");

            card
                .Property(c => c.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            card
                .Property(c => c.Data)
                .HasJsonPropertyName("Data")
                .HasColumnType("json")
                .IsRequired();

            card.OwnsMany(c => c.Status, cardStatus =>
            {
                cardStatus
                    .ToTable("CardImportStatus")
                    .HasKey("Id");

                cardStatus
                    .Property(cs => cs.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                cardStatus
                    .WithOwner()
                    .HasForeignKey("CardImportId");

                cardStatus
                    .Property(cs => cs.Current)
                    .HasColumnName("Current")
                    .IsRequired();

                cardStatus
                    .Property(cs => cs.Key)
                    .HasColumnName("Key")
                    .IsRequired();

                cardStatus
                    .Property(cs => cs.Type)
                    .HasColumnName("Type")
                    .HasConversion(new EnumToNumberConverter<TypeStatusCardImport, int>())
                    .IsRequired();

                cardStatus
                    .Property(cs => cs.Observation)
                    .HasColumnName("Observation");

                cardStatus.OwnsOne(cs => cs.Audit, cardStatusAudit =>
                {
                    cardStatusAudit
                        .Property(csa => csa.CreatedAt)
                        .HasColumnName("CreatedAt");

                    cardStatusAudit
                        .Property(csa => csa.UpdatedAt)
                        .HasColumnName("UpdatedAt");
                });
            });
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
