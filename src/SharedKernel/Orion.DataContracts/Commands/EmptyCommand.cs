using MediatR;

namespace Orion.DataContracts.Commands
{
    public record EmptyCommand<TResult> : IRequest<TResult>;
}