using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.Extensions;
using TaskManager.BLL.Notification.Interface;
using TaskManager.BLL.Tasks.DTO.Request;
using TaskManager.BLL.Tasks.DTO.Response;
using TaskManager.BLL.Tasks.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Shared;
using TaskManager.Persistence.Extensions;
using TaskManager.Persistence.Interface;
using Todo = TaskManager.Domain.Entities.Task;
using TaskManager.BLL.Notification.DTO.Request;
using TaskManager.Domain.Enums;

namespace TaskManager.BLL.Tasks.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IRepository<Todo> _taskRepository;
        private readonly IRepository<UserProfile> _userRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper _mapper;
         
        public TaskService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _mapper = _serviceFactory.GetService<IMapper>();
            _taskRepository = _unitOfWork.GetRepository<Todo>();
            _projectRepository = _unitOfWork.GetRepository<Project>();
            _userRepository = _unitOfWork.GetRepository<UserProfile>();
            _notificationService = _serviceFactory.GetService<INotificationService>();
        }

        public async Task<Response<CreateTaskResponse>> CreateTask(string userId, CreateTaskRequest request)
        {
            request.CheckNull("Request can't be empty");
            UserProfile author = await _userRepository.GetSingleByAsync(x => x.UserId == userId);
            author.CheckNull("User Profile doesn't exist for this user!! Create one");

            if (!author.Active)
                throw new InvalidOperationException("Account Inactive!!");

            if (request.StartTime < DateTime.Now)
                throw new InvalidOperationException("A task can't start on a previous date or time");
            Todo task = _mapper.Map<Todo>(request);
            task.AuthorId = author.Id;
            task.AssigneeId = request.AssigneeId == null  ? author.Id :  request.AssigneeId;
            var newTask = await _taskRepository.AddAsync(task);
            SendNotificationRequest notification = new SendNotificationRequest
            {
                  Title = "Task Created",
                  Message = $"Your task has been created, it due on {request.EndTime}",
                  NotificationType = NotificationType.CreateTaskReminder,
                  ReceiverId = author.Id
                  
            };
            await _notificationService.SendNotification(notification);
            var result = _mapper.Map<CreateTaskResponse>(newTask);
            return new Response<CreateTaskResponse>
            {
                Success = true,
                Message = "Task created successfully",
                Result = result

            };

        }

        public async Task<string> DeleteTask(string userId, DeleteTaskRequest request)
        {
            UserProfile user = await _userRepository.GetSingleByAsync(x => x.UserId == userId);
            user.CheckNull("User Profile doesn't exist for this user!! Create one");

            if (!user.Active)
                throw new InvalidOperationException("Account Inactive!!");
            Todo task = await _taskRepository.GetSingleByAsync(x => x.Id == request.Id && x.AuthorId == user.Id);
            task.CheckNull($"Task with id {request.Id} doesn't exist for user");
            await _taskRepository.DeleteByIdAsync(task.Id);
            return $"Task Deleted";
           
        }

        public async Task<Response<GetTaskResponse>> GetTaskById(GetTaskRequest request)
        {
            Todo task = await _taskRepository.GetByIdAsync(request.Id);
            task.CheckNull("Task doesn't Exist");
            GetTaskResponse response = _mapper.Map<GetTaskResponse>(task);

            return new Response<GetTaskResponse>
            {
                Success = true,
                Message = $"Task retrieved",
                Result = response
            };
            
        }

        public async Task<PagedResponse<GetTaskResponse>> GetTasks(string userId,GetTasksRequest request)
        {
            var user = await _userRepository.GetSingleByAsync(u => u.UserId == userId);
            user.CheckNull("User doesn't exist");
            var tasks = _taskRepository.GetQueryable(t => t.AssigneeId == user.Id || t.AuthorId == user.Id);
            var pagedtasks = tasks.GetPagedItems(request);
            var response = _mapper.Map<PagedResponse<GetTaskResponse>>(pagedtasks);
            return response;
        }

        public async Task<Response<UpdateTaskResponse>> UpdateTask(string userId, UpdateTaskRequest request)
        {
            UserProfile userProfile = await _userRepository.GetByIdAsync(userId);
            userProfile.CheckNull("Profile doesn't exist create one!!!");
            Todo task = await _taskRepository.GetByIdAsync(request.Id);
            task.CheckNull("Task doesn't Exist");
            if (task.AuthorId != userProfile.Id)
                throw new InvalidOperationException("You can't edit a task you don't own");
            var updatedTask = _mapper.Map(request, task);
            var result = _taskRepository.UpdateAsync(updatedTask);
            SendNotificationRequest notification = new SendNotificationRequest
            {
                Title = "Task Status Update",
                Message = $"Your task status has been updated",
                NotificationType = NotificationType.TaskStatusUpdate,
                ReceiverId = task.AuthorId

            };
            await _notificationService.SendNotification(notification);
            var response = _mapper.Map<UpdateTaskResponse>(result);
            return new Response<UpdateTaskResponse>
            {
                Success = true,
                Result = response,
                Message = "Task updated"
            };
           
        }
        public async Task<Response<AddTaskResponse>> AddToProject(AddTaskRequest request)
        {
            Todo task = await _taskRepository.GetByIdAsync(request.TaskId);
            task.CheckNull("Task Doesn't Exist");
            if (task.ProjectId != null)
                throw new InvalidOperationException("Task already belongs to a project");

            Project project = await _projectRepository.GetByIdAsync(request.ProjectId);
            project.CheckNull("Project doesn't exist");
            task.ProjectId = project.Id;

            Todo result = await _taskRepository.UpdateAsync(task);
            SendNotificationRequest notification = new SendNotificationRequest
            {
                Title = "Task Added",
                Message = $"A task has been added to your project",
                NotificationType = NotificationType.ProjectUpdateReminder,
                ReceiverId = project.OwnerId

            };
            await _notificationService.SendNotification(notification);
            AddTaskResponse response = _mapper.Map<AddTaskResponse>(result);
            return new Response<AddTaskResponse>
            {
                Success = true,
                Result = response,
                Message = $"Task {task.Title} has been added to project {project.Name}"

            };
        }

        public async Task<Response<RemoveTaskResponse>> RemoveTaskFromProject(RemoveTaskRequest request)
        {
            Todo task = _taskRepository.GetById(request.TaskId);
            task.CheckNull("Task doesn't exist");
            var project = await _projectRepository.GetSingleByAsync(x => x.Id == request.ProjectId, include: x => x.Include(t => t.Tasks), tracking: true);
            if (!project.Tasks.Any(x => x.Id == request.TaskId))
            {
                throw new InvalidOperationException("Task doesn't exist in project");
            }

            task.ProjectId = null;
            Todo updatedTask = await _taskRepository.UpdateAsync(task);
            var result = _mapper.Map<RemoveTaskResponse>(updatedTask);
            SendNotificationRequest notification = new SendNotificationRequest
            {
                Title = "Task Removed",
                Message = $"A task has been removed your project",
                NotificationType = NotificationType.ProjectUpdateReminder,
                ReceiverId = project.OwnerId

            };
            await _notificationService.SendNotification(notification);
            return new Response<RemoveTaskResponse>
            {
                Success = true,
                Result = result,
                Message = "Task removed from project"
            };
           
        }

        public async Task<Response<UpdateTaskAssignmentResponse>> AssignTask(UpdateTaskAssignmentRequest request)
        {
            UserProfile userProfile = await _userRepository.GetByIdAsync(request.AssigneeId);
            userProfile.CheckNull("User doesn't exist");
            if (!userProfile.Active)
                throw new InvalidOperationException("You can't assign task to an inactive user");
            Todo task = await _taskRepository.GetByIdAsync(request.TaskId);
            task.CheckNull("Task Doesn't Exist");
            var assignedTask = _mapper.Map<Todo>(request);
            var response = await _taskRepository.UpdateAsync(assignedTask);

            SendNotificationRequest notification = new SendNotificationRequest
            {
                Title = "Task Assigned",
                Message = $"A task has been assigned to you",
                NotificationType = NotificationType.TaskAssignmentReminder,
                ReceiverId = assignedTask.AssigneeId!

            };
            await _notificationService.SendNotification(notification);
            var result = _mapper.Map<UpdateTaskAssignmentResponse>(response);

            return new Response<UpdateTaskAssignmentResponse>
            {
                Success = true,
                Message = "Task assigned",
                Result = result,
            };

             throw new NotImplementedException();
        }
    }
}
