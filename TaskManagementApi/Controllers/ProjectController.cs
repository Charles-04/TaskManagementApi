using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Api.Controllers.Shared;
using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.BLL.Projects.Interface;
using TaskManager.Persistence.Interface;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private IServiceFactory _serviceFactory;
        private IProjectService _projectService;
        public ProjectController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _projectService = _serviceFactory.GetService<IProjectService>();
        }

        [HttpPost("create-project")]
        [SwaggerOperation("Creates a new project")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            string userId = GetUserId();
            var response = await _projectService.CreateProject(userId, request);
            return Ok(response);

        }

        [HttpPut("update-project")]
        [SwaggerOperation("updates an existing project")]
        public async Task<IActionResult> UpdateProject(UpdateProjectRequest request)
        {
            string userId = GetUserId();
            var response = await _projectService.UpdateProject(userId, request);
            return Ok(response);

        } 
        [HttpGet("view-project")]
        [SwaggerOperation("Gets an existing project")]
        public async Task<IActionResult> GetProject([FromQuery]ViewProjectRequest request)
        {
            string userId = GetUserId();
            var response = await _projectService.ViewProject(userId, request);
            return Ok(response);

        }  
        [HttpDelete("delete-project/{Id}")]
        [SwaggerOperation("Deletes an existing project")]
        public async Task<IActionResult> DeleteProject([FromRoute]string Id)
        {
            string userId = GetUserId();
            var response = await _projectService.DeleteProject(userId, Id);
            return Ok(response);

        }
    }
}
