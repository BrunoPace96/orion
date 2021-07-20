using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orion.Core.Domain;
using Orion.Core.Services;
using Orion.DomainValidation.Domain;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Repository.Repository;
using Orion.Repository.UnitOfWork.Factories;
using Xunit;

namespace Orion.Manager.Core.Tests.Common.Tests
{
    public class BaseTest<TEntity>: IClassFixture<ApplicationFixture> 
        where TEntity : EntityBase
    {
        protected readonly IMediator Mediator;
        protected readonly IDomainValidationProvider Validator;
        protected readonly IRepository<TEntity> Repository;
        
        private readonly IWriteService<TEntity> _service;
        private readonly IUnitOfWorkScopeFactory _unitOfWork;
        
        protected BaseTest(ApplicationFixture fixture)
        {
            Mediator = fixture.ServiceProvider.GetService<IMediator>();
            Validator = fixture.ServiceProvider.GetService<IDomainValidationProvider>();
            Repository = fixture.ServiceProvider.GetService<IRepository<TEntity>>();
            _service = fixture.ServiceProvider.GetService<IWriteService<TEntity>>();
            _unitOfWork = fixture.ServiceProvider.GetService<IUnitOfWorkScopeFactory>();
        }

        protected async Task<Guid> GenerateAndAsync(TEntity entity)
        {
            Validator.GetErrors().Clear();
            
            var unitOfWork = _unitOfWork.Get();
            await _service.SaveAsync(entity);
            await unitOfWork.CommitAsync();
            
            entity = await Repository.ReloadAsync(entity);
            return entity.Id;
        }
    }
}