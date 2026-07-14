using InfosoftGlobalTestApi.Application.Litters.Commands;
using InfosoftGlobalTestApi.Application.Litters.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfosoftGlobalTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LittersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LittersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id:guid}/publish")]
        public async Task<IActionResult> Publish([FromRoute] Guid id,[FromHeader(Name = "X-Breeder-Id")] Guid breederId, CancellationToken cancellationToken = default)
        {
            var command = new LitterPublishCommand(id, breederId);

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetLitters(
            [FromHeader(Name = "X-Breeder-Id")] Guid breederId,
            [FromQuery] string? status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetLittersQuery(breederId, status, pageNumber, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}

