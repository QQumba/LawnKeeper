using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawnKeeper.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LawnKeeperDbContext context) : base(context)
        {
        }
        
        public async Task<User> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await Set.FirstOrDefaultAsync(u => u.Email.Equals(email), ct);
        }
    }
} 