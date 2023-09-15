using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Tasks.DTO.Request;
using TaskManager.BLL.Tasks.DTO.Response;
using Task = TaskManager.Domain.Entities.Task;
namespace TaskManager.Infrastructure.Configurations
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<CreateTaskRequest, Task>();
            CreateMap<AddTaskRequest, Task>();
            CreateMap<RemoveTaskRequest, Task>();
            CreateMap<UpdateTaskRequest, Task>();
            CreateMap<UpdateTaskAssignmentRequest, Task>();
            CreateMap<DeleteTaskRequest, Task>();
            CreateMap<GetTaskRequest, Task>();
            CreateMap<Task,GetTaskResponse>();
            CreateMap<Task,CreateTaskResponse>();
            CreateMap<Task,UpdateTaskResponse>();
            CreateMap<Task,UpdateTaskAssignmentResponse>();
            CreateMap<Task,AddTaskResponse>();
            CreateMap<Task,RemoveTaskResponse>();


        }
    }
}
