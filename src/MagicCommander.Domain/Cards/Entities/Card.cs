using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Cards.Services;

namespace MagicCommander.Domain.Cards.Entities
{
	public class Card : Entity, IHasAlternateKey
	{
		private List<SupertypeCard> _supertypes = new();
		private List<TypeCard> _types = new();
		private List<Ruling> _rullings = new();
		private List<string> _colors = new();
		private List<string> _subtypes = new();

		public Guid Key { get; set; }
		public int MultiverseId { get; set; }
		public double Cmc { get; set; }
		public string Name { get; set; } = string.Empty;
		public string ManaCost { get; set; } = string.Empty;
		public string Set { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public string Type { get; set; } = string.Empty;
		public string Artist { get; set; } = string.Empty;
		public string Number { get; set; } = string.Empty;
		public string Power { get; set; } = string.Empty;
		public string Toughness { get; set; } = string.Empty;
		public string Layout { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public string ExternalId { get; set; } = string.Empty;
		public TypeRarity Rarity { get; set; }
		public IReadOnlyList<SupertypeCard> Supertypes { get { return _supertypes; } set { _supertypes = value.ToList(); } }
		public IReadOnlyList<TypeCard> Types { get { return _types; } set { _types = value.ToList(); } }
		public IReadOnlyList<Ruling> Rulings { get { return _rullings; } set { _rullings = value.ToList(); } }
		public IReadOnlyList<string> Subtypes { get { return _subtypes; } set { _subtypes = value.ToList(); } }
		public IReadOnlyList<string> Colors { get { return _colors; } set { _colors = value.ToList(); } }
		public Audit Audit { get; set; } = new();

		protected Card() { }

		public static Card FromDto(MagicApiCardDto magicApiCardDto)
		{
			return new Card
			{
				MultiverseId = magicApiCardDto.MultiverseId,
				Cmc = magicApiCardDto.Cmc,
				Name = magicApiCardDto.Name,
				ManaCost = magicApiCardDto.ManaCost,
				Text = magicApiCardDto.Text,
				Artist = magicApiCardDto.Artist,
				Number = magicApiCardDto.Number,
				Layout = magicApiCardDto.Layout,
				ImageUrl = magicApiCardDto.ImageUrl,
				Type = magicApiCardDto.Type,
				Rarity = magicApiCardDto.Rarity,
				Colors = magicApiCardDto.Colors,
				Types = magicApiCardDto.Types,
				Subtypes = magicApiCardDto.Subtypes
			};
		}

		public Card(int multiverseId, int cmc, string name, string manaCost, string set, string text, string artist, string number, string power, string toughness, string layout, string imageUrl, string externalId, TypeRarity rarity, List<string> colors, List<SupertypeCard> supertypes, List<TypeCard> types, List<string> subtypes)
		{
			MultiverseId = multiverseId;
			Cmc = cmc;
			Name = name;
			ManaCost = manaCost;
			Set = set;
			Text = text;
			Artist = artist;
			Number = number;
			Power = power;
			Toughness = toughness;
			Layout = layout;
			ImageUrl = imageUrl;
			ExternalId = externalId;
			Rarity = rarity;
			Colors = colors;
			Supertypes = supertypes;
			Types = types;
			Subtypes = subtypes;
		}
	}
}
