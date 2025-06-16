using MediatR;

namespace Moongazing.Incepta.Application.Features.Routes.Commands.Delete;

public record DeleteRouteCommand(Guid Id) : IRequest<bool>;
