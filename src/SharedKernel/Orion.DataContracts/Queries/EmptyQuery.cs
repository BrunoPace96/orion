using MediatR;

namespace Orion.DataContracts.Queries
{
    public record EmptyQuery<TResult> : IRequest<TResult>;
}