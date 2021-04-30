using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;

namespace LawnKeeper.Domain.Repositories
{
    public interface ILawnRepository : IRepository<Lawn>
    {
        Task<List<Lawn>> GetUserLawnsAsync(string email, CancellationToken ct = default);
    }
}