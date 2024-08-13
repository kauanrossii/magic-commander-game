using MagicCommander.Domain._Shared;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Decks.Entities;

public class DeckCard
{
    public int Id { get; protected set; }
    public Deck Deck { get; protected set; }
    public int DeckId { get; protected set; }
    public Card Card { get; protected set; }
    public int CardId { get; protected set; }
    public Guid Key { get; protected set; }
    public Audit Audit { get; protected set; } = new();

    protected DeckCard() { }

    public DeckCard(int deckId, int cardId)
    {
        Key =  Guid.NewGuid();
        DeckId = deckId;
        CardId = cardId;
    }
}
