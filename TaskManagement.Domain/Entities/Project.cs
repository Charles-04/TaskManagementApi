using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public string OwnerId { get; set; }
        public UserProfile Owner { get; set; }
    }
}
