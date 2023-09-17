
using TaskManager.BLL.Tasks.DTO.Request;
using TaskManager.BLL.Tasks.DTO.Response;
using TaskManager.Domain.Shared;

namespace TaskManager.BLL.Tasks.Interface
{
    public interface ITaskService
    {
        Task<PagedResponse<GetTaskResponse>> GetTasks(GetTasksRequest request);
        Task<Response<CreateTaskResponse>> CreateTask(string userId, CreateTaskRequest request);
        Task<string> DeleteTask(string userId, DeleteTaskRequest request);
        Task<Response<UpdateTaskResponse>> UpdateTask(string userId, UpdateTaskRequest request);
        Task<Response<GetTaskResponse>> GetTaskById(GetTaskRequest request);
        Task<Response<RemoveTaskResponse>> RemoveTaskFromProject(RemoveTaskRequest request);
        Task<Response<AddTaskResponse>> AddToProject(AddTaskRequest request);
        Task<Response<UpdateTaskAssignmentResponse>> AssignTask(UpdateTaskAssignmentRequest request);
    }
}
