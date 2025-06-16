using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moongazing.Incepta.Application.Features.Routes.Commands.Create;
using Moongazing.Incepta.Application.Features.Routes.Commands.Delete;
using Moongazing.Incepta.Application.Features.Routes.Commands.Update;
using Moongazing.Incepta.Application.Features.Routes.Queries.GetAll;
using Moongazing.Incepta.Application.Features.Routes.Queries.GetById;

namespace Moongazing.Incepta.AdminApi.Controllers;

[ApiController]
[Route("api/routes")]
public class RoutesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoutesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRouteCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? tenantId)
    {
        var result = await _mediator.Send(new GetAllRoutesQuery(tenantId));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRouteByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRouteCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteRouteCommand(id));
        return success ? NoContent() : NotFound();
    }
}