using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.BLL.Projects.DTO.Response;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Configurations
{
    public class ProjectMappingProfile :Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<CreateProjectRequest, Project>();
            CreateMap<UpdateProjectRequest, Project>();
            CreateMap<Project,CreateProjectResponse>();
            CreateMap<Project,UpdateProjectResponse>();
            CreateMap<Project,ViewProjectResponse>();
            
        }
        
    }
    
}
