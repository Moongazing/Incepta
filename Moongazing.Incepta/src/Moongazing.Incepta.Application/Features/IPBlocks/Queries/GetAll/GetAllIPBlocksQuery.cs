using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Dtos;

namespace Moongazing.Incepta.Application.Features.IPBlocks.Queries.GetAll;

public record GetAllIPBlocksQuery() : IRequest<List<IPBlockDto>>;