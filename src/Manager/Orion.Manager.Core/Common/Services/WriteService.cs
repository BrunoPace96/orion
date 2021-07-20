using System;
using System.Threading.Tasks;
using Orion.Core.Domain;
using Orion.Core.Services;
using Orion.DomainValidation.Domain;
using Orion.Repository.Repository;

namespace Orion.Manager.Core.Common.Services
{
    public sealed class WriteService<TEntity> : IWriteService<TEntity>
        where TEntity : EntityBase
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IDomainValidationProvider _domainValidationProvider;

        public WriteService(
            IRepository<TEntity> repository,
            IDomainValidationProvider domainValidationProvider
        )
        {
            _repository = repository;
            _domainValidationProvider = domainValidationProvider;
        }
        
        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (_domainValidationProvider.HasErrors()) 
                return null;

            bool IsNewEntity()
            {
                return entity.Id.Equals(Guid.Empty);
            }
            
            if (IsNewEntity())
            {
                entity.Created();
                await _repository.InsertAsync(entity);
            }
            else
            {
                entity.Updated();
                _repository.Update(entity);
            }

            return entity;
        }

        public void Delete(TEntity entity) => 
            _repository.Delete(entity);
    }
}