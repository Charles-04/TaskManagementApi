
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Tasks.DTO.Request;

namespace TaskManager.BLL.Tasks.Interface
{
    public interface ITaskService
    {
        Task GetTasks();
        Task CreateTask(CreateTaskRequest request);
        Task DeleteTask();
        Task UpdateTask();
        Task GetTaskById();
    }
}
