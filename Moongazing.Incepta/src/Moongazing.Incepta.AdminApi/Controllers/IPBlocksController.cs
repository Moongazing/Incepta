using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moongazing.Incepta.Application.Features.IPBlocks.Commands.Create;
using Moongazing.Incepta.Application.Features.IPBlocks.Commands.Delete;
using Moongazing.Incepta.Application.Features.IPBlocks.Commands.Update;
using Moongazing.Incepta.Application.Features.IPBlocks.Queries.GetAll;
using Moongazing.Incepta.Application.Features.IPBlocks.Queries.GetById;

namespace Moongazing.Incepta.AdminApi.Controllers;

[ApiController]
[Route("api/ipblocks")]
public class IPBlocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public IPBlocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIPBlockCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllIPBlocksQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetIPBlockByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIPBlockCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteIPBlockCommand(id));
        return success ? NoContent() : NotFound();
    }
}