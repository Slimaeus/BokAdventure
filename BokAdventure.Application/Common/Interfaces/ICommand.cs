using MediatR;

namespace BokAdventure.Application.Common.Interfaces;

public interface ICommand<TResponse> : IRequest<TResponse> { }
