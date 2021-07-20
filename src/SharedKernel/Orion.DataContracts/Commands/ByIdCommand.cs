using System;
using MediatR;

namespace Orion.DataContracts.Commands
{
    public record ByIdCommand<TResult>(Guid Id) : IRequest<TResult>;
}