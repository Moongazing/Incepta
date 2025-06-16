using MediatR;

namespace Moongazing.Incepta.Application.Features.Routes.Commands.Create;

public record CreateRouteCommand(Guid TenantId, string Path, string TargetUrl, bool Enabled) : IRequest<Guid>;
