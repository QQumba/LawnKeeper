using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateAsync(TEntity e, CancellationToken ct = default);
        Task CreateRangeAsync(List<TEntity> e, CancellationToken ct = default);
        Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<List<TEntity>> PageAsync(int skip, int take, CancellationToken ct = default);
        Task<TEntity> UpdateAsync(TEntity e, CancellationToken ct = default);
        Task DeleteByIdAsync(int id, CancellationToken ct = default);
        Task DeleteAsync(TEntity e, CancellationToken ct = default);
        Task DeleteRangeAsync(IEnumerable<TEntity> e, CancellationToken ct = default);
    }
}