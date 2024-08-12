using MagicCommander.Domain._Shared;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Decks.Entities
{
    public class Deck
    {
        private List<Card> _cards = new();
        
        public int Id { get; set; }
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
            if (Commander.Colors.Except(card.Colors).Any())
                return false;

            var duplicatedCard = _cards.Exists(card => card.ExternalId == card.ExternalId);

            if (duplicatedCard)
                return false;

            _cards.Add(card);
            return true;
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }
    }
}
