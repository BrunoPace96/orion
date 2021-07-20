using System;
using Orion.Core.Domain.Contracts;

namespace Orion.Core.Domain
{
    public abstract partial class EntityBase: IEntityBase
    {
        public Guid Id { get; }
        
        public override bool Equals(object obj)
        {
            if (obj is not EntityBase item) 
                return false;

            if (GetType() != item.GetType()) 
                return false;
            
            return ReferenceEquals(this, obj) || item.Id.Equals(Id);
        }
        
        public override int GetHashCode() => 
            Id.GetHashCode() ^ 31;

        public static bool operator ==(EntityBase left, EntityBase right) => 
            left?.Equals(right) ?? right is null;

        public static bool operator !=(EntityBase left, EntityBase right) => 
            !(left == right);
    }
}