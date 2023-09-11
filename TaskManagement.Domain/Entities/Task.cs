using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string AuthorId { get; set; }
        public UserProfile Author { get; set; }
    }
}
