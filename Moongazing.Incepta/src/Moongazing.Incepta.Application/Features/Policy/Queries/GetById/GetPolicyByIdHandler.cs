using MediatR;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Application.Features.Policy.Dtos;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Policy.Queries.GetById;

public class GetPolicyByIdHandler : IRequestHandler<GetPolicyByIdQuery, PolicyDto?>
{
    private readonly GatewayDbContext _db;

    public GetPolicyByIdHandler(GatewayDbContext db)
    {
        _db = db;
    }

    public async Task<PolicyDto?> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Policies
            .Where(p => p.Id == request.Id)
            .Select(p => new PolicyDto
            {
                Id = p.Id,
                Name = p.Name,
                RequiredClaims = p.RequiredClaims
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}