using System.Collections.Generic;
using System.Threading.Tasks;
using LawnKeeper.Domain.Entities;

namespace LawnKeeper.Domain.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetUserNotifications(string email);
    }
}