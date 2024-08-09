namespace MagicCommander.Domain.Deck.Entities
{
    public class Rulling
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }

        protected Rulling() { }

        public Rulling(DateTimeOffset date, string text)
        {
            Key = Guid.NewGuid();
            Text = text;
            Date = date;
        }
    }
}
