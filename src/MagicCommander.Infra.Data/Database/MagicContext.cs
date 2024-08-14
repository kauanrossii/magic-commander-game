using MagicCommander.Infra.Data.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Infra.Data.Database;

public class MagicContext : DbContext
{
    public MagicContext(DbContextOptions<MagicContext> options) : base(options) {
        var optionsBuilder = new DbContextOptionsBuilder<MagicContext>();
        var connectionString = Environment.GetEnvironmentVariable("PostgresqlConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardsConfiguration());
        modelBuilder.ApplyConfiguration(new DecksConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}
