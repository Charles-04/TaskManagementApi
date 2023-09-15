using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Tasks.DTO.Response
{
    public record GetTaskResponse
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Priority { get; init; }
        public string StartDate { get; init; }
        public string EndDate { get; init; }

    }
    public record UpdateTaskResponse
    {
        public string Id { get; init; }
        public string Title { get; init; }
    }  
    public record CreateTaskResponse
    {
        public string Id { get; init; }
        public string Title { get; init; }
    }
    public record UpdateTaskStatusResponse
    {
        [Required]
        public string TaskId { get; init; }
        
    }
    public record UpdateTaskAssignmentResponse
    {
        [Required]
        public string TaskId { get; init; }
      
    }

    public record AddTaskResponse
    {
        public string TaskId { get; init; }
    }
    public record RemoveTaskResponse
    {
        public string TaskId { get; init; }
    }
}
