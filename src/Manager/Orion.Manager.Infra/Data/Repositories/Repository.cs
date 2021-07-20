using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orion.Core.Domain;
using Orion.Manager.Infra.Data.Context;
using Orion.Repository.Repository;

namespace Orion.Manager.Infra.Data.Repositories
{
    public sealed class Repository<TEntity> : 
        ReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(ApplicationDbContext context): base(context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task InsertAsync(TEntity entity) =>
            await _set.AddAsync(entity);

        public void Update(TEntity entity) =>
            _context.Entry(entity).State = EntityState.Modified;

        public void Delete(TEntity entity) => 
            entity.Deleted();
    }
}