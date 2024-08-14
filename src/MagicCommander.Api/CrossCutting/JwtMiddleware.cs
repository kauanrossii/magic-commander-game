
using MagicCommander.Api.Helpers;
using System.Security.Claims;

namespace MagicCommander.Api.CrossCutting
{
	public class JwtMiddleware : IMiddleware
	{
		private readonly JwtTokenHelper _jwtTokenHelper;

		public JwtMiddleware(JwtTokenHelper jwtTokenHelper)
		{
			_jwtTokenHelper = jwtTokenHelper;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var token = context.Request.Headers.Authorization
				.ToString()
				.Replace("Bearer ", "");

			if (!string.IsNullOrEmpty(token))
			{
				var claimsPrincipal = _jwtTokenHelper.ValidateJwtToken(token);

				var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				context.Items["UserId"] = userId;
			}

			await next(context);
		}
	}
}
