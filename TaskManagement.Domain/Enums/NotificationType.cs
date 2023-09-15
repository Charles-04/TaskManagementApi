using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Enums
{
    public enum NotificationType
    {
        CreateTaskReminder,
        ProjectUpdateReminder,
        TaskStatusUpdate,
        DueDateReminder,

    }
    public static class NotificationTypeExtension
    {
        public static string GetStringValue(this NotificationType type)
        {
            return type switch
            {
                NotificationType.DueDateReminder => "DueDateReminder",
                NotificationType.TaskStatusUpdate => "TaskStatusUpdate",
                _ => null
            };

        }
    }
}
