using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain._Shared.Notifications;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Domain.Decks.Entities
{
    public class Deck : Entity, IHasAlternateKey
    {
        private List<Card> _cards = new();

        public Guid Key { get; init; }
        public string Name { get; protected set; }
        public int UserId { get; protected set; }
        public Audit Audit { get; protected set; } = new();
        public Card Commander { get; protected set; }
        public IReadOnlyList<Card> Cards { get { return _cards; } set { _cards = value.ToList(); } }

        protected Deck() { }

        public Deck(string name, Card commander)
        {
            Key = Guid.NewGuid();
            Name = name;
            Commander = commander;
        }

        public (bool success, List<Notification> notifications) AddCard(Card card)
        {
            var notifications = new List<Notification>();

            if (!IsCardColorsValid(card.Colors))
            {
                notifications.Add(new Notification(card.Name, "InvalidCard", "The color of card isn't the same of deck commander"));
            }

            if (IsCardDuplicatedInDeck(card))
            {
				notifications.Add(new Notification(card.Name, "DuplicatedCard", "The card was already added to deck before."));
			}

            if (notifications.Count > 0) return (false, notifications);

            _cards.Add(card);

            return (true, notifications);
        }

        private bool IsCardColorsValid(IEnumerable<TypeColor> colors)
            => Commander.Colors.Except(colors).Any();

        private bool IsCardDuplicatedInDeck(Card card)
            => _cards.Exists(c => c.ExternalId == card.ExternalId);

        public bool RemoveCard(Card card) =>
            _cards.Remove(card);
    }
}
