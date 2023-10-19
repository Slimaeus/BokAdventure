using MediatR;

namespace BokAdventure.Application.Common.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}

