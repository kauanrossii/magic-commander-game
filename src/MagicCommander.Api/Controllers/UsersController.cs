using System.Net;
using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Application._Shared.Dtos.Users;
using MagicCommander.Application.Users.CreateUser;
using MagicCommander.Application.Users.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace MagicCommander.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all the registered users",
            Description = "Get all the registered users (the request user must be an Administrator)"
        )]
        [SwaggerResponse((int)HttpStatusCode.OK, "Users requested successfully", typeof(PaginatedResult<UserDto>), "application/json")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error when requesting users", null, "application/json")]
        public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersRequest request,  CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}
