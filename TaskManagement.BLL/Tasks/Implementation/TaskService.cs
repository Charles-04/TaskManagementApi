using AutoMapper;
using TaskManager.BLL.Extensions;
using TaskManager.BLL.Tasks.DTO.Request;
using TaskManager.BLL.Tasks.DTO.Response;
using TaskManager.BLL.Tasks.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Shared;
using TaskManager.Persistence.Interface;
using Todo = TaskManager.Domain.Entities.Task;

namespace TaskManager.BLL.Tasks.Implementation
{
    public class TaskService : ITaskService
    {
        private IServiceFactory _serviceFactory;
        private IUnitOfWork _unitOfWork;
        private IRepository<Todo> _taskRepository;
        private IRepository<UserProfile> _userRepository;
        private IMapper _mapper;
        public TaskService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _mapper = _serviceFactory.GetService<IMapper>();
            _taskRepository = _unitOfWork.GetRepository<Todo>();
            _userRepository = _unitOfWork.GetRepository<UserProfile>();

        }

        public async Task<Response<CreateTaskResponse>> CreateTask(string userId, CreateTaskRequest request)
        {
            request.CheckNull("Request can't be empty");
            UserProfile author = await _userRepository.GetSingleByAsync(x => x.UserId == userId);
            author.CheckNull("User Profile doesn't exist for this user!! Create one");

            if (!author.Active)
                throw new InvalidOperationException("Account Inactive!!");
            Todo task = _mapper.Map<Todo>(request);
            task.AuthorId = author.Id;
            var newTask = await _taskRepository.AddAsync(task);
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

        public Task<Response<GetTaskResponse>> GetTaskById(GetTaskRequest request)
        {

            throw new NotImplementedException();
        }

        public Task<PagedResponse<GetTaskResponse>> GetTasks(GetTasksRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateTaskResponse>> UpdateTask(UpdateTaskRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
