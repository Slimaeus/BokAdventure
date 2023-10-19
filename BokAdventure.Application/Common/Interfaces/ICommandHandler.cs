using MediatR;

namespace BokAdventure.Application.Common.Interfaces;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
}
