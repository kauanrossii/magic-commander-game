using System;
using MagicCommander.Domain._Shared;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Decks.Entities;

public class DeckCard
{
    public int Id { get; protected set; }
    public int CardId { get; protected set; }
    public Guid Key { get; protected set; }
    public Audit Audit { get; protected set; } = new();

    protected DeckCard() { }

    public DeckCard(int cardId)
    {
        Key =  Guid.NewGuid();
        CardId = cardId;
    }
}
