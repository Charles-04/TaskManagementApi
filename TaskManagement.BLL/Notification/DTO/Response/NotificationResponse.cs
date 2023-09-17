using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.BLL.Notification.DTO.Response
{
    public record SendNotificationResponse
    {
        public string ReceiverId { get; init; }
        public string NotificationType { get; init; }
      
    }
    public class GetNotificationResponse
    {
        public string NotificationType { get; init; }
        public string Receiver { get; init; }
        public string Message { get; init; }
        public string Id { get; init; }
        
    }
    public class GetUnreadNotificationsResponse : GetNotificationResponse
    {
      
    }
    public record DeleteNotificationResponse
    {
        public string Id { get; init; }
    }
    public record ReadNotificationResponse
    {
        public string Id { get; init; }
    }
}
