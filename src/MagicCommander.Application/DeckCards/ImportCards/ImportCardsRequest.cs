using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Domain.Cards.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace MagicCommander.Application.DeckCards.ImportCards
{
	public class ImportCardsRequest : IRequest<EntityKeyDto?>
	{
		[JsonIgnore]
		public int UserId { get; set; }
        public required Guid DeckKey { get; init; }
		public required List<Card> Cards { get; init; }
    }
}
