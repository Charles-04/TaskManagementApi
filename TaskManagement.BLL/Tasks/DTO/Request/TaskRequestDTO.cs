using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Shared;

namespace TaskManager.BLL.Tasks.DTO.Request
{
    public record CreateTaskRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
    }
    public record UpdateTaskRequest
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
    public record DeleteTaskRequest
    {
        public string Id { get; init; }
    }
    public record GetTaskRequest
    {
        public string Id { get; init; }
    }
    public record GetTasksRequest : RequestParameter
    {
        
    }
}
