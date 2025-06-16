using MediatR;
using Moongazing.Incepta.Application.Features.Policy.Dtos;

namespace Moongazing.Incepta.Application.Features.Policy.Queries.GetById;

public record GetPolicyByIdQuery(Guid Id) : IRequest<PolicyDto?>;