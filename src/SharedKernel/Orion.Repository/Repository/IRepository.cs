using System.Threading.Tasks;

namespace Orion.Repository.Repository
{
    public interface IRepository<TEntity>: IReadOnlyRepository<TEntity>
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}