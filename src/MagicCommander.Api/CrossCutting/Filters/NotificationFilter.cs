using MagicCommander.Domain._Shared.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace MagicCommander.Api.CrossCutting.Filters
{
	public class NotificationFilter : IAsyncResultFilter
	{
		private readonly INotificationContext _notificationContext;

		public NotificationFilter(INotificationContext notificationContext)
		{
			_notificationContext = notificationContext;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (_notificationContext.HasNotifications)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.HttpContext.Response.ContentType = "application/json";

				var errors = new { errors = _notificationContext.Notifications };

				var notifications = JsonSerializer.Serialize(errors);
				await context.HttpContext.Response.WriteAsync(notifications);

				return;
			}

			await next();
		}
	}
}
