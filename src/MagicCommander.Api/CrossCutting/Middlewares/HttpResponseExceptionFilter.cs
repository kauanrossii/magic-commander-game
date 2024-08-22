using MagicCommander.Application._Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MagicCommander.Api.CrossCutting.Middlewares
{
	public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
	{
		public int Order => int.MaxValue - 10;

		public void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Exception is not null)
			{

				if (context.Exception is EntityNotFoundException)
				{
					context.Result = new ObjectResult(new ErrorResponse("resource-not-found", "The requested resource was not found."))
					{
						StatusCode = (int)HttpStatusCode.NotFound
					};
					context.ExceptionHandled = true;
				}
				else
				{
					context.Result = new ObjectResult(new ErrorResponse("internal-error", "An internal server error ocurred."))
					{
						StatusCode = (int)HttpStatusCode.NotFound
					};
					context.ExceptionHandled = true;
				}
			}
		}

		public void OnActionExecuting(ActionExecutingContext context) { }
	}
}
