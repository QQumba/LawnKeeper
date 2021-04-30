using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawnKeeper.DAL.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(LawnKeeperDbContext context) : base(context)
        {
        }

        public override Task<Notification> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return Set.Include(n => n.User).FirstOrDefaultAsync(n => n.Id == id, ct);
        }

        public async Task<List<Notification>> GetUserNotifications(string email)
        {
            var users = GetEntitySet<User>();
            var user = await users.Include(u => u.Notifications).FirstOrDefaultAsync(u => u.Email.Equals(email));
            return user is null ? new List<Notification>() : user.Notifications;
        }
    }
}