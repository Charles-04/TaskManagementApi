using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Api.Controllers.Shared;
using TaskManager.BLL.Notification.DTO.Request;
using TaskManager.BLL.Notification.Interface;
using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.Persistence.Interface;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly INotificationService _notificationService;
        public NotificationController(IServiceFactory serviceFactory)
        {
             _serviceFactory = serviceFactory;
            _notificationService = _serviceFactory.GetService<INotificationService>();
        }

        [HttpGet("view-unread-notifications")]
        [SwaggerOperation("Gets unread notifications")]
        public async Task<IActionResult> ViewUnread([FromQuery]GetUnreadNotificationsRequest request)
        {
            string userId = GetUserId();
            var response = await _notificationService.GetUnreadNotifications(userId, request);
            return Ok(response);

        }
        [HttpGet("view-all-notifications")]
        [SwaggerOperation("Gets all notifications")]
        public async Task<IActionResult> ViewAllNotifications([FromQuery]GetNotificationRequest request)
        {
            string userId = GetUserId();
            var response = await _notificationService.GetNotifications(userId, request);
            return Ok(response);

        }
        [HttpPut("read-notification")]
        [SwaggerOperation("Marks notification as read")]
        public async Task<IActionResult> ReadNotification(ReadNotificationRequest request)
        {
            string userId = GetUserId();
            var response = await _notificationService.ReadNotification(userId, request);
            return Ok(response);

        }
        [HttpDelete("delete-notification")]
        [SwaggerOperation("deletes notification")]
        public async Task<IActionResult> DeleteNotification(DeleteNotificationRequest request)
        {
            string userId = GetUserId();
            var response = await _notificationService.DeleteNotification(userId, request);
            return Ok(response);

        }
    }
}
