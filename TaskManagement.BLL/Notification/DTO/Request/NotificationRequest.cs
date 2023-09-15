using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Shared;

namespace TaskManager.BLL.Notification.DTO.Request
{
    public record SendNotificationRequest
    {
        public string Title { get; init; }
        public string ReceiverId { get; init; }
        public NotificationType NotificationType { get; init; }
        public string Message { get; init; }
    }
    public record GetNotificationRequest : RequestParameter
    {
       
    }
    public record GetUnreadNotificationsRequest :RequestParameter
    {
       
    }
    public record DeleteNotificationRequest
    {
        public string Id { get; init; }
    }
    public record ReadNotificationRequest
    {
        public string Id { get; init; }
    }
}
