using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Domain._Shared.Notifications;
using MagicCommander.Domain.Decks.Entities;
using MagicCommander.Domain.Decks;
using MediatR;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Cards.Services;
using MagicCommander.Domain.Cards;
using MediatR.Wrappers;
using MagicCommander.Domain._Shared.Exceptions;
using MagicCommander.Domain.Cards.Entities;

namespace MagicCommander.Application.Decks.CreateDeck
{
	public class CreateDeckRequestHandler : IRequestHandler<CreateDeckRequest, EntityKeyDto?>
	{
		private readonly INotificationContext _notificationContext;
		private readonly IDecksRepository _decksRepository;
		private readonly ICardsRepository _cardsRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IApiMagicService _apiMagicService;

		public CreateDeckRequestHandler(INotificationContext notificationContext, IDecksRepository decksRepository, IUnitOfWork unitOfWork, IApiMagicService apiMagicService, ICardsRepository cardsRepository)
		{
			_notificationContext = notificationContext;
			_decksRepository = decksRepository;
			_unitOfWork = unitOfWork;
			_apiMagicService = apiMagicService;
			_cardsRepository = cardsRepository;
		}

		public async Task<EntityKeyDto?> Handle(CreateDeckRequest request, CancellationToken cancellationToken)
		{
			var duplicatedDeck = await _decksRepository
				.ExistsAsync(d => d.Name == request.Name && d.UserId == request.UserId);

			if (duplicatedDeck)
			{
				_notificationContext.AddNotification(new Notification("Name", "DuplicatedDeck", "The user already has a deck with this name"));
				return null;
			}

			var commanderCard = await SaveCommanderCard(request.CommanderExternalId);

			if (commanderCard is null)
				return null;

			var commanderTypeIdentifiers = commanderCard.Type.Split('-');

			if (commanderTypeIdentifiers.Length == 0)
			{
				_notificationContext.AddNotification(new Notification("Commander", "InvalidType", "The selected commander isn't a valid commander"));
				return null;
			}

			var commanderType = commanderTypeIdentifiers[0].Trim();


			if (commanderType is not "Legendary Creature")
			{
				_notificationContext.AddNotification(new Notification("Commander", "InvalidType", "The selected commander isn't a valid commander"));
				return null;
			}

			var deck = new Deck(
				request.Name,
				commanderCard
			);

			await _decksRepository.InsertAsync(deck);
			await _unitOfWork.CommitAsync(cancellationToken);

			return new EntityKeyDto(deck.Key);
		}

		private async Task<Card?> SaveCommanderCard(string externalId)
		{
			var existentCard = await _cardsRepository
				.FindAsync(card => card.ExternalId == externalId);

			if (existentCard is not null)
				return existentCard;

			try
			{
				var card = await _apiMagicService.GetCardByExternalId(externalId);
				await _cardsRepository.InsertAsync(card);
				return card;
			}
			catch (MagicApiResourceNotFoundException e)
			{
				_notificationContext.AddNotification(new Notification("CommanderId", "InexistentCommander", e.Message));
				return null;
			}
		}
	}
}
