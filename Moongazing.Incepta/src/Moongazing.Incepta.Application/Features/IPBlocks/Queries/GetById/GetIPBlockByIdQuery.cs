using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Dtos;

namespace Moongazing.Incepta.Application.Features.IPBlocks.Queries.GetById;

public record GetIPBlockByIdQuery(Guid Id) : IRequest<IPBlockDto?>;