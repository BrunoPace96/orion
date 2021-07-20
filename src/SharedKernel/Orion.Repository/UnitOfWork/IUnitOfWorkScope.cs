using System.Threading.Tasks;

namespace Orion.Repository.UnitOfWork
{
    public interface IUnitOfWorkScope
    {
        bool Committed { get; }
        void Rollback();
        Task CommitAsync();
    }
}