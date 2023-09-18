using AutoMapper;
using TaskManager.BLL.Notification.DTO.Request;
using TaskManager.BLL.Notification.DTO.Response;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Shared;

namespace TaskManager.Api.Configurations
{
    public class NotificationsMappingProfile : Profile
    {
        public NotificationsMappingProfile()
        {
            CreateMap<Notification, GetNotificationResponse>()
            .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType.ToString()))
            .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<Notification,GetUnreadNotificationsResponse>()
                .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType.ToString()))
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<Notification,ReadNotificationResponse>();
            CreateMap<Notification,DeleteNotificationResponse>();
            CreateMap<Notification,SendNotificationResponse>()
                .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType.ToString()));
            CreateMap<GetNotificationRequest, Notification>();
            CreateMap<GetUnreadNotificationsRequest, Notification>();
            CreateMap<ReadNotificationRequest, Notification>();
            CreateMap<DeleteNotificationRequest, Notification>();
            CreateMap<SendNotificationRequest, Notification>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ReceiverId));
        }
    }
}
