using MagicCommander.Domain.Cards;
using MagicCommander.Domain.Cards.Entities;
using MagicCommander.Infra.Data.Database._Shared;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Infra.Data.Database.Repositories;

public class CardsRepository : Repository<Card>, ICardsRepository
{
    public CardsRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
