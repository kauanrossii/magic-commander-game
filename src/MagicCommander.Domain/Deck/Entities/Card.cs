namespace MagicCommander.Domain.Deck.Entities
{
	public class Card
    {
        private List<TypeColor> _colors = new();
        private List<TypeCard> _types = new();
        private List<SubtypeCard> _subtypes = new();
        private List<Rulling> _rullings = new();

		public int Id { get; set; }
        public Guid Key { get; set; }
		public int MultiverseId { get; set; }
		public int Cmc { get; set; }
		public string Name { get; set; }
        public string ManaCost { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }
        public string Text { get; set; }
        public string Artist { get; set; }
        public string Number { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public string ImageUrl { get; set; }
        public string ExternalId { get; set; }
        public IReadOnlyList<TypeColor> Colors { get { return _colors; } set { _colors = value.ToList(); } }
        public IReadOnlyList<TypeCard> Types { get { return _types; } set { _types = value.ToList(); } }
        public IReadOnlyList<SubtypeCard> Subtypes { get { return _subtypes; } set { _subtypes = value.ToList(); } }
        public IReadOnlyList<Rulling> Rullings { get { return _rullings; } set { _rullings = value.ToList(); } }

		public Card(int multiverseId, int cmc, string name, string manaCost, string rarity, string set, string text, string artist, string number, string power, string toughness, string layout, string imageUrl, string externalId)
		{
			MultiverseId = multiverseId;
			Cmc = cmc;
			Name = name;
			ManaCost = manaCost;
			Rarity = rarity;
			Set = set;
			Text = text;
			Artist = artist;
			Number = number;
			Power = power;
			Toughness = toughness;
			Layout = layout;
			ImageUrl = imageUrl;
			ExternalId = externalId;
		}
	}
}
