using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.BLL.Projects.DTO.Response;
using TaskManager.Domain.Shared;

namespace TaskManager.BLL.Projects.Interface
{
    public interface IProjectService
    {
        Task<Response<CreateProjectResponse>> CreateProject(string userId,CreateProjectRequest request);
        Task<Response<UpdateProjectResponse>> UpdateProject(string userId,UpdateProjectRequest request);
        Task<Response<string>> DeleteProject(string userId, string projectId);
        Task<Response<ViewProjectResponse>> ViewProject (string userId, ViewProjectRequest request);
        Task<Response<UserProjectResponse>> GetUserProjects();
    }
}
