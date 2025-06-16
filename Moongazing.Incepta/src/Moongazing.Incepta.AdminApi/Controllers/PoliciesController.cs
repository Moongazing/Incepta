using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moongazing.Incepta.Application.Features.Policy.Commands.Create;
using Moongazing.Incepta.Application.Features.Policy.Commands.Delete;
using Moongazing.Incepta.Application.Features.Policy.Commands.Update;
using Moongazing.Incepta.Application.Features.Policy.Queries;
using Moongazing.Incepta.Application.Features.Policy.Queries.GetById;

namespace Moongazing.Incepta.AdminApi.Controllers;

[ApiController]
[Route("api/policies")]
public class PoliciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PoliciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePolicyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPoliciesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPolicyByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePolicyCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeletePolicyCommand(id));
        return success ? NoContent() : NotFound();
    }
}