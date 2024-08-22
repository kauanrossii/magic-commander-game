using FluentValidation.Results;

namespace MagicCommander.Domain._Shared.Notifications;

public interface INotificationContext
{
	IReadOnlyCollection<Notification> Notifications { get; }
	bool HasNotifications { get; }

	void AddNotification(Notification notification);
	void AddNotifications(ValidationResult validationResult);
	void AddNotifications(IEnumerable<Notification> notifications);
	void ClearNotifications();
}

public class NotificationContext : INotificationContext
{
	private readonly List<Notification> _notifications = new();

	public IReadOnlyCollection<Notification> Notifications => _notifications;
	public bool HasNotifications => _notifications.Count > 0;

	public void AddNotification(Notification notification) => _notifications.Add(notification);

	public void AddNotifications(IEnumerable<Notification> notifications)
	{
		_notifications.AddRange(notifications);
	}

	public void AddNotifications(ValidationResult validationResult)
	{
		foreach (var error in validationResult.Errors)
		{
			_notifications.Add(
				new Notification(
					error.PropertyName,
					error.ErrorCode,
					error.ErrorMessage
			   )
			);
		}
	}

	public void ClearNotifications() => _notifications.Clear();
}
