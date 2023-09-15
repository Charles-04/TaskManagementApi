using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Domain.Entities
{
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public string? AssigneeId { get; set; }
        public UserProfile Assignee { get; set; }
        public string? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
