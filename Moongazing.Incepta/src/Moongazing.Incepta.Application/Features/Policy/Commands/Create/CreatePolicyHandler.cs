using MediatR;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Policy.Commands.Create;

public class CreatePolicyHandler : IRequestHandler<CreatePolicyCommand, Guid>
{
    private readonly GatewayDbContext _db;

    public CreatePolicyHandler(GatewayDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = new PolicyDefinition
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            RequiredClaims = request.RequiredClaims
        };

        _db.Policies.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}