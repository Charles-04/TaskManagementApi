using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Notification.DTO.Request;
using TaskManager.BLL.Notification.DTO.Response;
using TaskManager.Domain.Shared;

namespace TaskManager.BLL.Notification.Interface
{
    public interface INotificationService
    {
        Task<Response<SendNotificationResponse>> SendNotification(SendNotificationRequest request);
        Task<Response<ReadNotificationResponse>> ReadNotification(string userId, ReadNotificationRequest request);
        Task<PagedResponse<IEnumerable<GetNotificationResponse>>> GetNotifications(string userId, GetNotificationRequest request);
        Task<PagedResponse<IEnumerable<GetUnreadNotificationsResponse>>> GetUnreadNotifications(string userId, GetUnreadNotificationsRequest request);
        Task<Response<DeleteNotificationResponse>> DeleteNotification(string userId, DeleteNotificationRequest request);

    }
}
