using AutoMapper;
using TaskManager.BLL.Notification.DTO.Request;
using TaskManager.BLL.Notification.DTO.Response;
using TaskManager.Domain.Entities;

namespace TaskManager.Api.Configurations
{
    public class NotificationsMappingProfile : Profile
    {
        public NotificationsMappingProfile()
        {
                CreateMap<Notification,GetNotificationResponse>();
                CreateMap<Notification,GetUnreadNotificationsResponse>();
                CreateMap<Notification,ReadNotificationResponse>();
                CreateMap<Notification,DeleteNotificationResponse>();
                CreateMap<Notification,SendNotificationResponse>();
            CreateMap<GetNotificationRequest, Notification>();
            CreateMap<GetUnreadNotificationsRequest, Notification>();
            CreateMap<ReadNotificationRequest, Notification>();
            CreateMap<DeleteNotificationRequest, Notification>();
            CreateMap<SendNotificationRequest, Notification>();
        }
    }
}
