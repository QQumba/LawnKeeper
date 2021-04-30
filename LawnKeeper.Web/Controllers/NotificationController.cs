using System.Threading.Tasks;
using LawnKeeper.Services.DataAccess;
using LawnKeeper.Web.JwtUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LawnKeeper.Web.Controllers
{
    [ApiController]
    [Route("api/notification")]
    [Authorize]
    public class NotificationController : JwtController
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger _logger;
        
        public NotificationController(UserService userService, NotificationService notificationService, ILogger<NotificationController> logger) : base(userService)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(Email);
            return Ok(notifications);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification([FromQuery] int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (!notification.UserEmail.Equals(Email))
            {
                return BadRequest();
            }

            await _notificationService.DeleteNotificationByIdAsync(notification.Id);
            return Ok();
        }
    }
}