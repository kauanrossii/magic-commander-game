using System.Net;
using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Application.Decks.CreateDeck;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MagicCommander.Api.Controllers
{
    [Route("api/decks")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeckController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [SwaggerOperation("Deck creation", "Create a deck specifying the commander")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Deck created successfully", typeof(EntityKeyDto), "application/json")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The request is invalid", null, "application/json")]
        public async Task<IActionResult> PostDeck([FromBody] CreateDeckRequest request, CancellationToken cancellationToken) {
            var result = await _mediator.Send(request, cancellationToken);
            if (result is null)
                return StatusCode(400);
                
            return CreatedAtAction(nameof(PostDeck), result);
        }   
    }
}
