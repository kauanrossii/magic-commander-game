using MagicCommander.Application._Shared.Dtos;
using MediatR;
using System.Text.Json.Serialization;

namespace MagicCommander.Application.Decks.CreateDeck
{
    public class CreateDeckRequest : IRequest<EntityKeyDto?>
	{
		[JsonIgnore]
		public int UserId { get; set; }
		public required string CommanderExternalId { get; init; }
		public required string Name { get; init; }
	}
}
