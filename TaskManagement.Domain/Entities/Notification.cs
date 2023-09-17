using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Notification
    {
        public Notification()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; } 
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public UserProfile User { get; set; }
        public DateTime SentAt { get; set; }

    }
}
