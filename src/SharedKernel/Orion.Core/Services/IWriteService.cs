using System.Threading.Tasks;
using Orion.Core.Domain;

namespace Orion.Core.Services
{
    public interface IWriteService<TEntity>
        where TEntity : EntityBase
    {
        Task<TEntity> SaveAsync(TEntity entity);
        void Delete(TEntity id);
    }
}