using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities.NotMapped;
using LawnKeeper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LawnKeeper.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly LawnKeeperDbContext _db;
        private DbSet<TEntity> _set;

        protected Repository(LawnKeeperDbContext context)
        {
            _db = context;
        }

        protected DbSet<TEntity> Set => _set ??= _db.Set<TEntity>();

        protected DbSet<T> GetEntitySet<T>() where T : BaseEntity
        {
            return _db.Set<T>();
        }
        
        //TODO: test return object id change
        public async Task<TEntity> CreateAsync(TEntity e, CancellationToken ct = default)
        {
            var now = DateTime.Now;
            e.CreatedDate = now;
            e.UpdatedDate = now;
            await Set.AddAsync(e, ct);
            await SaveChangesAsync(ct);
            return e;
        }

        public async Task CreateRangeAsync(List<TEntity> e, CancellationToken ct = default)
        {
            var now = DateTime.Now;
            foreach (var entity in e)
            {
                entity.CreatedDate = now;
                entity.UpdatedDate = now;
            }
            await Set.AddRangeAsync(e, ct);
            await SaveChangesAsync(ct);
        }

        //TODO: test if the method return correct type of option 
        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await Set.FirstOrDefaultAsync(e => e.Id == id, cancellationToken: ct);
        }

        public virtual Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return Set.Select(e => e).ToListAsync(cancellationToken: ct);
        }

        public virtual Task<List<TEntity>> PageAsync(int skip, int take, CancellationToken ct = default)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationToken: ct);
        }

        public async Task<TEntity> UpdateAsync(TEntity e, CancellationToken ct = default)
        {
            e.UpdatedDate = DateTime.Now;
            Set.Update(e);
            await SaveChangesAsync(ct);
            return e;
        }

        public async Task DeleteByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = Set.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                Set.Remove(entity);
                await SaveChangesAsync(ct);
            }
        }

        public async Task DeleteAsync(TEntity e, CancellationToken ct = default)
        {
            Set.Remove(e);
            await SaveChangesAsync(ct);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> e, CancellationToken ct = default)
        {
            Set.RemoveRange(e);
            await SaveChangesAsync(ct);
        }
        
        protected async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _db.SaveChangesAsync(ct);
        }
    }
}
