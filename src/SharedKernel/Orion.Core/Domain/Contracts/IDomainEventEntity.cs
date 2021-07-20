using System.Collections.Generic;
using MediatR;

namespace Orion.Core.Domain.Contracts
{
    public interface IDomainEventEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        bool HasDomainEvents();
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void ClearDomainEvents();
    }
}