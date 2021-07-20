using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;
using Orion.DataContracts.Queries;
using Orion.DataContracts.Results;

namespace Orion.Repository.Repository
{
    public interface IReadOnlyRepository<TEntity>
    {
        Task<TEntity> FirstOrDefaultAsync(Guid id);
        Task<bool> CheckIfExistsAsync(Guid id);
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> FirstOrDefaultAsync<TSpecification>(TSpecification specification) 
            where TSpecification: ISpecification<TEntity>;
        Task<IList<TEntity>> QueryAsync<TSpecification>(TSpecification specification) 
            where TSpecification: ISpecification<TEntity>;
        
        Task<PaginatedResult<TEntity>> QueryPagedAndCountAsync<TSpecification, TQuery>(
            TSpecification specification,
            FilterQuery<TQuery> query
        ) where TSpecification : ISpecification<TEntity>;
        
        Task<bool> CheckIfExistsAsync<TSpecification>(TSpecification specification) 
            where TSpecification: ISpecification<TEntity>;
        
        Task<TEntity> ReloadAsync(TEntity instance);
    }
}