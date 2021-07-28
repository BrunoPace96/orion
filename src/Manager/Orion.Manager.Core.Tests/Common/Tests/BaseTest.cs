using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orion.Core.Domain;
using Orion.Core.Domain.Contracts;
using Orion.DomainValidation.Domain;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Repository.Repository;
using Orion.Repository.UnitOfWork.Factories;
using Xunit;

namespace Orion.Manager.Core.Tests.Common.Tests
{
    public class BaseTest<TEntity>: IClassFixture<ApplicationFixture> 
        where TEntity : EntityBase, IAggregateRoot
    {
        protected readonly IMediator Mediator;
        protected readonly IDomainValidationProvider Validator;
        private readonly IRepository<TEntity> _repository;
        protected readonly IReadOnlyRepository<TEntity> ReadOnlyRepository;
        private readonly IUnitOfWorkScopeFactory _unitOfWork;
        
        protected BaseTest(ApplicationFixture fixture)
        {
            Mediator = fixture.ServiceProvider.GetService<IMediator>();
            Validator = fixture.ServiceProvider.GetService<IDomainValidationProvider>();
            _repository = fixture.ServiceProvider.GetService<IRepository<TEntity>>();
            ReadOnlyRepository = fixture.ServiceProvider.GetService<IReadOnlyRepository<TEntity>>();
            _unitOfWork = fixture.ServiceProvider.GetService<IUnitOfWorkScopeFactory>();
        }

        protected async Task<Guid> GenerateAndAsync(TEntity entity)
        {
            Validator.GetErrors().Clear();
            
            var unitOfWork = _unitOfWork.Get();
            await _repository.SaveAsync(entity);
            await unitOfWork.CommitAsync();
            
            entity = await ReadOnlyRepository.ReloadAsync(entity);
            return entity.Id;
        }
    }
}