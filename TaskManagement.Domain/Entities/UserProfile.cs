using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public AppUser User { get; set; }
        public ICollection<Task> AssignedTasks { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Project> Projects { get; set; }
        public DateTime Birthday { get; set; }
    }
}
