using System;

namespace Orion.Core.Domain.Contracts
{
    public interface IAuditableEntity
    {
        bool IsDeleted { get; }
        DateTime CreatedAt { get; }
        DateTime LastUpdatedAt { get; }
        DateTime? DeletedAt { get; }
        
        void Created();
        void Updated();
        void Deleted();
    }
}