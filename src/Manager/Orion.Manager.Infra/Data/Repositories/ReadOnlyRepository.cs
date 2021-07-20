using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orion.Core.Domain;
using Orion.DataContracts.Queries;
using Orion.DataContracts.Results;
using Orion.Manager.Infra.Data.Context;
using Orion.Repository.Repository;

namespace Orion.Manager.Infra.Data.Repositories
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _set;
        private readonly ISpecificationEvaluator _specificationEvaluator;

        public ReadOnlyRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
            _specificationEvaluator = SpecificationEvaluator.Default;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Guid id) =>
            await _set.AsQueryable().FirstOrDefaultAsync(e => e.Id == id);
        
        public async Task<bool> CheckIfExistsAsync(Guid id) =>
            await _set.AsQueryable().AnyAsync(e => e.Id == id);
        
        public async Task<IList<TEntity>> GetAllAsync() =>
            await _set.AsQueryable().ToListAsync();

        public async Task<TEntity> FirstOrDefaultAsync<TSpecification>(TSpecification specification)
            where TSpecification : ISpecification<TEntity> =>
            await ApplySpecification(specification).FirstOrDefaultAsync();
        
        public async Task<PaginatedResult<TEntity>> QueryPagedAndCountAsync<TSpecification, TQuery>(
            TSpecification specification, 
            FilterQuery<TQuery> query
        ) where TSpecification : ISpecification<TEntity>
        {
            var items = await ApplySpecification(specification).ToListAsync();
            var count = await ApplySpecification(specification).CountAsync();

            return new PaginatedResult<TEntity>
            {
                CurrentPage = query.Page,
                TotalPages = (int) Math.Ceiling(count / (decimal) query.PageSize),
                TotalItems = count,
                Items = items
            };
        }
        
        public async Task<IList<TEntity>> QueryAsync<TSpecification>(TSpecification specification)
            where TSpecification : ISpecification<TEntity> =>
            await ApplySpecification(specification).ToListAsync();
        
        public async Task<bool> CheckIfExistsAsync<TSpecification>(TSpecification specification)
            where TSpecification : ISpecification<TEntity> =>
            await ApplySpecification(specification).AnyAsync();
        
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification) =>
            _specificationEvaluator.GetQuery(_set.AsQueryable(), specification);

        public async Task<TEntity> ReloadAsync(TEntity instance)
        {
            await _context.Entry(instance).ReloadAsync();
            return instance;
        }
    }
}