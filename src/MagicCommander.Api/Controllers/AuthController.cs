using System.Net;
using MagicCommander.Application._Shared.Dtos.Jwt;
using MagicCommander.Application.Auth.Sigin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MagicCommander.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous()]
        [HttpPost("signin")]
        [SwaggerOperation("User authentication", "Authentication of users using email and password")]
        [SwaggerResponse((int)HttpStatusCode.OK, "User authenticated successfully", typeof(JwtDto), "application/json")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The credentials are invalid", null, "application/json")]
        public async Task<IActionResult> PostAuthentication([FromBody] SigninRequest request, CancellationToken cancellationToken) {
            var result = await _mediator.Send(request, cancellationToken);
            if (result is null)
                return StatusCode(400);
                
            return CreatedAtAction(nameof(PostAuthentication), result);
        }   
    }
}
