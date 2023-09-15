using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Extensions;
using TaskManager.BLL.Notification.DTO.Request;
using TaskManager.BLL.Notification.DTO.Response;
using TaskManager.BLL.Notification.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Shared;
using TaskManager.Persistence.Extensions;
using TaskManager.Persistence.Interface;
using Notifications = TaskManager.Domain.Entities.Notification;

namespace TaskManager.BLL.Notification.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserProfile> _userRepository;
        private readonly IRepository<Notifications> _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _mapper = _serviceFactory.GetService<IMapper>();
            _userRepository = _unitOfWork.GetRepository<UserProfile>();
            _notificationRepository = _unitOfWork.GetRepository<Notifications>();
        }
        public async Task<Response<DeleteNotificationResponse>> DeleteNotification(string userId, DeleteNotificationRequest request)
        {
            var notification = await _notificationRepository.GetSingleByAsync(x => x.UserId == userId && x.Id == request.Id);
            notification.CheckNull("Notification not found");
            await _notificationRepository.DeleteAsync(notification);
            var response = new DeleteNotificationResponse
            {
                Id = notification.Id
            };
            return new Response<DeleteNotificationResponse>
            {
                Success = true,
                Message = "Notification deleted",
                Result = response

            };
            
        }

        public async Task<PagedResponse<IEnumerable<GetNotificationResponse>>> GetNotifications(string userId,GetNotificationRequest request)
        {
            var notifications = _notificationRepository.GetQueryable(n => n.UserId == userId);
            PagedList<Notifications> pagedNotifications = await notifications.GetPagedItems(request);
            return  _mapper.Map<PagedResponse<IEnumerable<GetNotificationResponse>>>(pagedNotifications);

        }

        public async Task<PagedResponse<IEnumerable<GetUnreadNotificationsResponse>>> GetUnreadNotifications(string userId, GetUnreadNotificationsRequest request)
        {
            var notifications = _notificationRepository.GetQueryable(n => n.UserId == userId && n.IsRead == false);
            PagedList<Notifications> pagedNotifications = await notifications.GetPagedItems(request);
            return _mapper.Map<PagedResponse<IEnumerable<GetUnreadNotificationsResponse>>>(pagedNotifications);
        }

        public async Task<Response<ReadNotificationResponse>> ReadNotification(string userId,ReadNotificationRequest request)
        {
            Notifications notification = _notificationRepository.GetById(request.Id);
            notification.CheckNull("Notification not found");
            var user = await _userRepository.GetByIdAsync(userId);
            user.CheckNull("Profile doesn't exist");
            if (notification.UserId != user.Id)
                throw new InvalidOperationException("Notification doesn't belong to  you");
            notification.IsRead = true;
            var response = await _notificationRepository.UpdateAsync(notification);
            var result = _mapper.Map<ReadNotificationResponse>(response);
            return new Response<ReadNotificationResponse> 
            { 
                Success = true,
                Message = "Marked as read",
                Result = result,

            };
           
        }

        public async Task<Response<SendNotificationResponse>> SendNotification(SendNotificationRequest request)
        {
            var notification = _mapper.Map<Notifications>(request);
            var result = await _notificationRepository.AddAsync(notification);
            SendNotificationResponse response = _mapper.Map<SendNotificationResponse>(result);
            return new Response<SendNotificationResponse>
            {
                Success = true,
                Message = "Notification sent",
                Result = response
            };
        }
    }
}
