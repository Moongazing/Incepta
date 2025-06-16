using MediatR;

namespace Moongazing.Incepta.Application.Features.Routes.Commands.Update;

public record UpdateRouteCommand(Guid Id, string Path, string TargetUrl, bool Enabled) : IRequest<bool>;
