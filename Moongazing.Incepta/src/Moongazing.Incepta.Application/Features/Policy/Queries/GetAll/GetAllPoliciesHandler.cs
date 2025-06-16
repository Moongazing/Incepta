using MediatR;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Application.Features.Policy.Dtos;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Policy.Queries;

public class GetAllPoliciesHandler : IRequestHandler<GetAllPoliciesQuery, List<PolicyDto>>
{
    private readonly GatewayDbContext _db;

    public GetAllPoliciesHandler(GatewayDbContext db)
    {
        _db = db;
    }

    public async Task<List<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
    {
        return await _db.Policies
            .Select(p => new PolicyDto
            {
                Id = p.Id,
                Name = p.Name,
                RequiredClaims = p.RequiredClaims
            })
            .ToListAsync(cancellationToken);
    }
}