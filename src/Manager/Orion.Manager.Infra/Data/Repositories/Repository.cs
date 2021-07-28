using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orion.Core.Domain;
using Orion.Core.Domain.Contracts;
using Orion.DomainValidation.Domain;
using Orion.Manager.Infra.Data.Context;
using Orion.Repository.Repository;

namespace Orion.Manager.Infra.Data.Repositories
{
    public sealed class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase, IAggregateRoot
    {
        private readonly ApplicationDbContext _context;
        private readonly IDomainValidationProvider _validator;
        private readonly DbSet<TEntity> _set;

        public Repository(
            ApplicationDbContext context,
            IDomainValidationProvider validator
        )
        {
            _context = context;
            _validator = validator;
            _set = _context.Set<TEntity>();
        }

        public async Task SaveAsync(TEntity entity)
        {
            if (_validator.HasErrors()) 
                return;

            bool IsNewEntity() => entity.Id.Equals(Guid.Empty);
            
            if (IsNewEntity())
            {
                entity.Created();
                await _set.AddAsync(entity);
            }
            else
            {
                entity.Updated();
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity) => 
            entity.Deleted();
    }
}