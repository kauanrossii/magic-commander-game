using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Domain._Shared.Notifications;
using MagicCommander.Domain.Decks.Entities;
using MagicCommander.Domain.Decks;
using MediatR;
using MagicCommander.Domain._Shared.Entities;

namespace MagicCommander.Application.Decks.CreateDeck
{
	public class CreateDeckRequestHandler : IRequestHandler<CreateDeckRequest, EntityKeyDto?>
	{
		private readonly INotificationContext _notificationContext;
		private readonly IDecksRepository _decksRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateDeckRequestHandler(INotificationContext notificationContext, IDecksRepository decksRepository, IUnitOfWork unitOfWork)
		{
			_notificationContext = notificationContext;
			_decksRepository = decksRepository;
			_unitOfWork = unitOfWork;
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

			var commanderTypeIdentifiers = request.Commander.Type.Split('-');

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
				request.Commander
			);

			await _decksRepository.InsertAsync(deck);
			await _unitOfWork.CommitAsync(cancellationToken);

			return new EntityKeyDto(deck.Key);
		}
	}
}
