namespace MagicCommander.Domain.Cards.Entities
{
    public class Rulling
    {
        public int Id { get; protected set; }
        public Guid Key { get; protected set; } = Guid.Empty;
        public string Text { get; protected set; } = string.Empty;
        public DateTimeOffset Date { get; protected set; }

        protected Rulling() { }

        public Rulling(DateTimeOffset date, string text)
        {
            Key = Guid.NewGuid();
            Text = text;
            Date = date;
        }
    }
}
