using AutoMapper;
using TaskManager.BLL.Extensions;
using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.BLL.Projects.DTO.Response;
using TaskManager.BLL.Projects.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Shared;
using TaskManager.Persistence.Interface;
using Todo = TaskManager.Domain.Entities.Task;

namespace TaskManager.BLL.Projects.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Todo> _taskRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IMapper _mapper;
        public ProjectService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = serviceFactory.GetService<IUnitOfWork>();
            _mapper = serviceFactory.GetService<IMapper>();
            _taskRepository = _unitOfWork.GetRepository<Todo>();
            _projectRepository = _unitOfWork.GetRepository<Project>();
            _userProfileRepository = _unitOfWork.GetRepository<UserProfile>();

        }


        public async Task<Response<CreateProjectResponse>> CreateProject(string userId, CreateProjectRequest request)
        {
            UserProfile userProfile = await _userProfileRepository.GetSingleByAsync(u => u.UserId == userId);
            userProfile.CheckNull("Profile doesn't exist");
            if (!userProfile.Active)
                throw new InvalidOperationException("User is deactivated");
            Project newProject = _mapper.Map<Project>(request);
            newProject.OwnerId = userProfile.Id;
            Project project = await _projectRepository.AddAsync(newProject);
            var response = _mapper.Map<CreateProjectResponse>(project);
            return new Response<CreateProjectResponse>
            {
                Success = true,
                Message = "Project created",
                Result = response
            };

        }


        public async Task<Response<UpdateProjectResponse>> UpdateProject(string userId, UpdateProjectRequest request)
        {
            UserProfile userProfile = await _userProfileRepository.GetSingleByAsync(u => u.UserId == userId);
            userProfile.CheckNull("Profile doesn't exist");
            Project project = await _projectRepository.GetByIdAsync(request.Id);
            project.CheckNull("Invalid project id");
            if (project.OwnerId != userProfile.Id)
                throw new InvalidOperationException("You can't update a project you don't own");
            Project updatedProject = _mapper.Map(request, project);
            var result = await _projectRepository.UpdateAsync(updatedProject);
            var response = _mapper.Map<UpdateProjectResponse>(result);
            return new Response<UpdateProjectResponse>
            {
                Success = true,
                Result = response,
                Message = " Project successfully updated"
            };


        }
        public async Task<Response<ViewProjectResponse>> ViewProject(ViewProjectRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
