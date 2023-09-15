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
        public NotificationType NotificationType { get; init; }
      
    }
    public record GetNotificationResponse
    {
        public string NotificationType { get; init; }
        public string Receiver { get; init; }
        public string Message { get; init; }
        
    }
    public record GetUnreadNotificationsResponse : GetNotificationResponse
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
