using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Api.Controllers.Shared;
using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.BLL.Tasks.DTO.Request;
using TaskManager.BLL.Tasks.Interface;
using TaskManager.BLL.UserAuth.Interface;
using TaskManager.Persistence.Interface;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController
    {
        private IServiceFactory _serviceFactory;
        private ITaskService _taskService;
        public TaskController(IServiceFactory serviceFactory)
        {
             _serviceFactory = serviceFactory;
            _taskService = _serviceFactory.GetService<ITaskService>();
        }

        [HttpPost("create-task")]
        [SwaggerOperation("Creates a new task")]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            string userId = GetUserId();
            var response = await _taskService.CreateTask(userId, request);
            return Ok(response);

        }
        [HttpPut("update-task")]
        [SwaggerOperation("Updates a existing task")]
        public async Task<IActionResult> UpdateTask(UpdateTaskRequest request)
        {
            string userId = GetUserId();
            var response = await _taskService.UpdateTask(userId, request);
            return Ok(response);

        }

        [HttpPut("add-task-to-project")]
        [SwaggerOperation("Adds a task to a project")]
        public async Task<IActionResult> AddToProject(AddTaskRequest request)
        {
            
            var response = await _taskService.AddToProject(request);
            return Ok(response);

        }

    }
}
