using System.Collections.Generic;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Domain.Repositories;
using Mapster;

namespace LawnKeeper.Services.DataAccess
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository; 
        
        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<NotificationViewModel> GetNotificationByIdAsync(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            return notification.Adapt<NotificationViewModel>();
        }

        public async Task<List<NotificationViewModel>> GetUserNotificationsAsync(string email){
            var notification = await _notificationRepository.GetUserNotifications(email);
            return notification.Adapt<List<NotificationViewModel>>();            
        }

        public async Task<List<NotificationViewModel>> GetAllNotifications()
        {
            var notifications = await _notificationRepository.GetAllAsync();
            return notifications.Adapt<List<NotificationViewModel>>();
        }

        public async Task DeleteNotificationByIdAsync(int id)
        {
            await _notificationRepository.DeleteByIdAsync(id);
        }
    }
}