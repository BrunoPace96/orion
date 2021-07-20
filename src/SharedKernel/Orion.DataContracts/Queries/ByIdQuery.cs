using System;
using MediatR;

namespace Orion.DataContracts.Queries
{
    public record ByIdQuery<TResult>(Guid Id) : IRequest<TResult>;
}