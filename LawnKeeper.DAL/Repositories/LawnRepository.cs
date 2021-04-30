using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawnKeeper.DAL.Repositories
{
    public class LawnRepository : Repository<Lawn>, ILawnRepository
    {
        public LawnRepository(LawnKeeperDbContext context) : base(context)
        {
        }
        
        public async Task<List<Lawn>> GetUserLawnsAsync(string email, CancellationToken ct = default)
        {
            var users = GetEntitySet<User>();
            var user = await users.Include(u=>u.Lawns).FirstOrDefaultAsync(u => u.Email.Equals(email), ct);
            
            return user is null ? new List<Lawn>() : user.Lawns;
        }
    }
}