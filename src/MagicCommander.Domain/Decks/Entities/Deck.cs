using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Decks.Entities
{
    public class Deck : Entity, IHasAlternateKey
    {
        private List<Card> _cards = new();
        
        public Guid Key { get; set; }
        public string Name { get; set; }
        public Audit Audit { get; set; } = new();
        public Card Commander { get; set; }
        public IReadOnlyList<Card> Cards { get { return _cards; } set { _cards = value.ToList(); } }

        public Deck(string name, Card commander)
        {
            Name = name;
            Commander = commander;
        }

        public bool AddCard(Card card)
        {
            if (!IsCardColorsValid(card.Colors))
                return false;

            if (IsCardDuplicatedInDeck(card))
                return false;

            _cards.Add(card);
            return true;
        }

        private bool IsCardColorsValid(IEnumerable<TypeColor> colors)
            => Commander.Colors.Except(colors).Any();

        private bool IsCardDuplicatedInDeck(Card card)
            => _cards.Exists(card => card.ExternalId == card.ExternalId);

        public bool RemoveCard(Card card) =>
            _cards.Remove(card);
    }
}
