using System;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Decks.Entities;

namespace MagicCommander.Domain.Decks;

public interface IDecksRepository : IRepository<Deck>
{
}
