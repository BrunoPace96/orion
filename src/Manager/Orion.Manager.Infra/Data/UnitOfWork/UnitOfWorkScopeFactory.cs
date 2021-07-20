using MediatR;
using Orion.DomainValidation.Domain;
using Orion.Manager.Infra.Data.Context;
using Orion.Repository.UnitOfWork;
using Orion.Repository.UnitOfWork.Factories;

namespace Orion.Manager.Infra.Data.UnitOfWork
{
    public class UnitOfWorkScopeFactory : UnitOfWorkScopeFactoryBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDomainValidationProvider _domainValidationProvider;
        private readonly IMediator _mediator;

        public UnitOfWorkScopeFactory(
            ApplicationDbContext context,
            IDomainValidationProvider domainValidationProvider,
            IMediator mediator
        )
        {
            _context = context;
            _domainValidationProvider = domainValidationProvider;
            _mediator = mediator;
        }

        protected override IUnitOfWorkScope CreateNew() => 
            new UnitOfWorkScope(_context, _domainValidationProvider, _mediator);
    }
}