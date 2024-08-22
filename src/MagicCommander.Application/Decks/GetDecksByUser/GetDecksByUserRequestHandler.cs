using MagicCommander.Application._Shared.Dtos.Decks;
using MagicCommander.Domain.Decks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCommander.Application.Decks.GetDecksByUser
{
	public class GetDecksByUserRequestHandler : IRequestHandler<GetDecksByUserRequest, List<DeckDto>>
	{
		private readonly IDecksRepository _decksRepository;

		public GetDecksByUserRequestHandler(IDecksRepository decksRepository)
		{
			_decksRepository = decksRepository;
		}

		public async Task<List<DeckDto>> Handle(GetDecksByUserRequest request, CancellationToken cancellationToken)
		{
			return [];
		}
	}
}
