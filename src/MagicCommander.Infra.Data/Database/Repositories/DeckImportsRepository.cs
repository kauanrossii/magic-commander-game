using MagicCommander.Domain.DecksImports;
using MagicCommander.Domain.DecksImports.Entities;
using MagicCommander.Infra.Data.Database._Shared;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Infra.Data.Database.Repositories;

public class DeckImportsRepository : Repository<DeckImport>, IDeckImportsRepository
{
    public DeckImportsRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
