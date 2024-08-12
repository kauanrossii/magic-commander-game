using MagicCommander.Domain.Cards.Entities;
using MagicCommander.Domain.Decks.Entities;
using MagicCommander.Domain.Users;
using MagicCommander.Infra.Data.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Infra.Data.Database;

public class MagicContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardsConfiguration());
        modelBuilder.ApplyConfiguration(new DecksConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}
