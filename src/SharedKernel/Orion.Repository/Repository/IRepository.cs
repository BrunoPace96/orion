using System.Threading.Tasks;
using Orion.Core.Domain.Contracts;

namespace Orion.Repository.Repository
{
    public interface IRepository<TEntity> where TEntity: IAggregateRoot
    {
        Task SaveAsync(TEntity entity);
        void Delete(TEntity entity);
    }
}