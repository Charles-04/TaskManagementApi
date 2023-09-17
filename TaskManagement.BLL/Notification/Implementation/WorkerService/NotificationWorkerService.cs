using AutoMapper;
using Hangfire;
using TaskManager.BLL.Interface.WorkerServices;
using TaskManager.Domain.Entities;
using TaskManager.Persistence.Implementation;
using TaskManager.Persistence.Interface;
using Notifications = TaskManager.Domain.Entities.Notification;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.BLL.Notification.WorkerService
{

    public class NotificationWorkerService : INotificationWorkerService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<Notifications> _notificationRepository;
        
        public NotificationWorkerService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _taskRepository = _unitOfWork.GetRepository<Task>();
            _notificationRepository = _unitOfWork.GetRepository<Notifications>();
        }
        public void SendReminders()
        {
            var tasks = _taskRepository.GetAll();
            List<Notifications> notifications = new ();
            foreach (var task in tasks)
            {
                if(task.EndDate > DateTime.Now)
                {
                    var notification = new Notifications
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Task Overdue",
                        NotificationType = Domain.Enums.NotificationType.DueDateReminder,
                        Message = "Your task is over due",
                        UserId = task.AssigneeId ?? task.AuthorId,

                    };
                    notifications.Add(notification);
                }

            }
            _notificationRepository.AddRange(notifications);
           
        }
    }
}
