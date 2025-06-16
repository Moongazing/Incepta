using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moongazing.Incepta.Application.Features.Tenants.Commands.Create;
using Moongazing.Incepta.Application.Features.Tenants.Commands.Delete;
using Moongazing.Incepta.Application.Features.Tenants.Commands.Update;
using Moongazing.Incepta.Application.Features.Tenants.Queries.GetAll;
using Moongazing.Incepta.Application.Features.Tenants.Queries.GetById;

namespace Moongazing.Incepta.AdminApi.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTenantsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTenantByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTenantCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteTenantCommand(id));
        return success ? NoContent() : NotFound();
    }
}