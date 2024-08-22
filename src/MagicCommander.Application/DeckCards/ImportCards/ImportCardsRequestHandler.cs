using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain._Shared.Notifications;
using MagicCommander.Domain.Cards;
using MagicCommander.Domain.Decks;
using MediatR;

namespace MagicCommander.Application.DeckCards.ImportCards
{
	public class ImportCardsRequestHandler : IRequestHandler<ImportCardsRequest, EntityKeyDto?>
	{
		private readonly INotificationContext _notificationContext;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IDecksRepository _decksRepository;
		private readonly ICardsRepository _cardsRepository;

		public ImportCardsRequestHandler(INotificationContext notificationContext, IUnitOfWork unitOfWork, IDecksRepository decksRepository, ICardsRepository cardsRepository)
		{
			_notificationContext = notificationContext;
			_unitOfWork = unitOfWork;
			_decksRepository = decksRepository;
			_cardsRepository = cardsRepository;
		}

		public async Task<EntityKeyDto?> Handle(ImportCardsRequest request, CancellationToken cancellationToken)
		{
			var deck = await _decksRepository.FindAsync(deck => 
				deck.Key == request.DeckKey && 
				deck.UserId == request.UserId
			);

			if (deck is null)
			{
				_notificationContext.AddNotification(new Notification("Deck", "InexistentDeck", "The selected deck doesn't exists."));
				return null;
			}

			foreach (var card in request.Cards)
			{
				var operation = deck.AddCard(card);

				_notificationContext.AddNotifications(operation.notifications);
			}

			if (_notificationContext.HasNotifications)
				return null;

			await _cardsRepository.InsertAsync(deck.Cards);
			await _decksRepository.InsertAsync(deck);
			await _unitOfWork.CommitAsync(cancellationToken);

			return new EntityKeyDto(deck.Key);
		}
	}
}
