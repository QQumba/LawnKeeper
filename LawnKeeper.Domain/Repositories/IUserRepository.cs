using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;

namespace LawnKeeper.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}