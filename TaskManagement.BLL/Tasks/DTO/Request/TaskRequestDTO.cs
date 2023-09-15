# nullable disable

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Shared;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.BLL.Tasks.DTO.Request
{
    public record CreateTaskRequest
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public DateTime StartTime { get; init; }
        [Required]
        public DateTime EndTime { get; init; }
        [Required]
        public Priority Priority { get; init; }
        [Required]
        public TaskStatus TaskStatus { get; init; } 
        public string AssigneeId { get; init; }
    }
    public record UpdateTaskRequest
    {
       [ Required]
        public string Id { get; init; }
        [Required]
        public string Title { get; init; }
        [Required]
        public string Description { get; init; }
    }
    public record DeleteTaskRequest
    {
        [Required]
        public string Id { get; init; }
    }
    public record GetTaskRequest
    {
        [Required]
        public string Id { get; init; }
    }
    public record GetTasksRequest : RequestParameter
    {
        
    }
    public record AddTaskRequest
    {
        [Required]
        public string ProjectId { get; init; }
        [Required]
        public string TaskId { get; init; }
    }
    public record RemoveTaskRequest
    {
        [Required]
        public string ProjectId { get; init; }
        [Required]
        public string TaskId { get; init; }
    }
    public record UpdateTaskStatusRequest
    {
        [Required]
        public string TaskId { get; init; }
        [Required]
        public TaskStatus Status { get; init; }
    }
    public record UpdateTaskAssignmentRequest
    {
        [Required]
        public string TaskId { get; init; }
        [Required]
        public string AssigneeId { get; init ; }
    }
}
