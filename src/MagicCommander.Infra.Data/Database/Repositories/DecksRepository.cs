using MagicCommander.Domain.Decks;
using MagicCommander.Domain.Decks.Entities;
using MagicCommander.Infra.Data.Database._Shared;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Infra.Data.Database.Repositories;

public class DecksRepository : Repository<Deck>, IDecksRepository
{
    public DecksRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
