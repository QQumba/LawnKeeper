using System.Collections.Generic;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;

namespace LawnKeeper.Domain.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<Schedule> GetLawnScheduleAsync(int lawnId);
    }
}