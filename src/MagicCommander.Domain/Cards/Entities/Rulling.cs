using MagicCommander.Domain._Shared.Entities;

namespace MagicCommander.Domain.Cards.Entities
{
    public class Ruling : Entity, IHasAlternateKey
    {
        public Guid Key { get; protected set; } = Guid.Empty;
        public string Text { get; protected set; } = string.Empty;
        public DateTimeOffset Date { get; protected set; }

        protected Ruling() { }

        public Ruling(DateTimeOffset date, string text)
        {
            Key = Guid.NewGuid();
            Text = text;
            Date = date;
        }
    }
}
