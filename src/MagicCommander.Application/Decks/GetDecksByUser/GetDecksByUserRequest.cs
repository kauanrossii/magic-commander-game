using MagicCommander.Application._Shared.Dtos.Decks;
using MediatR;
using System.Text.Json.Serialization;

namespace MagicCommander.Application.Decks.GetDecksByUser
{
	public class GetDecksByUserRequest : IRequest<List<DeckDto>>
	{
		[JsonIgnore]
		public int UserId { get; set; }
	}
}
