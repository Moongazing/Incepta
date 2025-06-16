using MediatR;
using Moongazing.Incepta.Application.Features.Policy.Dtos;

namespace Moongazing.Incepta.Application.Features.Policy.Queries;

public record GetAllPoliciesQuery() : IRequest<List<PolicyDto>>;