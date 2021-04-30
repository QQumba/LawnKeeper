using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawnKeeper.DAL.Repositories
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(LawnKeeperDbContext context) : base(context)
        {
        }

        public override async Task<Schedule> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await Set.Include(s => s.ScheduleIntervals).FirstOrDefaultAsync(e => e.Id == id, cancellationToken: ct);
        }

        public override Task<List<Schedule>> GetAllAsync(CancellationToken ct = default)
        {
            return Set.Include(s => s.ScheduleIntervals).Select(e => e).ToListAsync(cancellationToken: ct);
        }

        public override async Task<List<Schedule>> PageAsync(int skip, int take, CancellationToken ct = default)
        {
            return await Set.Include(s => s.ScheduleIntervals).Skip(skip).Take(take).ToListAsync(cancellationToken: ct);
        }

        public async Task<Schedule> GetLawnScheduleAsync(int lawnId)
        {
            var lawns = GetEntitySet<Lawn>();
            var lawn = await lawns.Include(l => l.Schedule).ThenInclude(s=>s.ScheduleIntervals).FirstOrDefaultAsync(l => l.Id == lawnId);
            return lawn.Schedule;
        }
    }
}