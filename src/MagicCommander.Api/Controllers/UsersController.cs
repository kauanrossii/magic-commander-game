using System.Net;
using MagicCommander.Application._Shared;
using MagicCommander.Application.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MagicCommander.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous()]
        [HttpPost()]
        [SwaggerOperation(
            Summary = "Creation of user",
            Description = "..."
        )]
        [SwaggerResponse((int)HttpStatusCode.OK, "User created successfully", typeof(EntityKeyDto), "application/json")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error when creating user", null, "application/json")]
        public async Task<IActionResult> PostUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result is null)
                return StatusCode(500);

            return CreatedAtAction(nameof(PostUser), new { key = result.Key }, result);
        }
    }
}
